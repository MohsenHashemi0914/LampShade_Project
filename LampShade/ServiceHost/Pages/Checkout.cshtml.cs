using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart { get; set; }
        private const string _cookieName = "cart-items";

        #region Constructor

        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinpalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculator,
            ICartService cartService, IProductQuery productQuery,
            IOrderApplication orderApplication, IZarinPalFactory zarinpalFactory, IAuthHelper authHelper)
        {
            Cart = new();
            _authHelper = authHelper;
            _cartService = cartService;
            _productQuery = productQuery;
            _zarinpalFactory = zarinpalFactory;
            _orderApplication = orderApplication;
            _cartCalculatorService = cartCalculator;
        }

        #endregion

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[_cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value) ?? new();

            cartItems.ForEach(cartItem =>
            {
                cartItem.CalculateTotalItemPrice();
                cartItem.CalculateItemPayAmount();
            });

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay(byte paymentMethod)
        {
            var cart = _cartService.Get();
            var cartItems = _productQuery.CheckInventoryStatusFor(cart.CartItems);
            if (cartItems.Any(x => !x.IsInStock)) return RedirectToPage("./Cart");

            cart.SetPaymentMethodId(paymentMethod);
            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == PaymentType.Online)
            {
                var paymentResponse = _zarinpalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "", "",
                    "جهت خرید اجناس دفتر مشاور املاک", orderId);

                var paymentUrl = ZarinpalApiUrls.CompletePayment(_zarinpalFactory.Prefix, paymentResponse.Authority);
                return Redirect(paymentUrl);
            }

            var result = new PaymentResult();
            return RedirectToPage("./PaymentResult", result.Succeeded(null, message: PaymentMessages.PaymentLater));
        }

        public IActionResult OnGetCallBack([FromQuery] long oId, [FromQuery] string authority,
            [FromQuery] string status)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse = _zarinpalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            var result = new PaymentResult();
            if (status is "OK" && verificationResponse.Status is 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete(_cookieName);
                result = result.Succeeded(issueTrackingNo);
            }
            else
            {
                result = result.Failed();
            }

            return RedirectToPage("./PaymentResult", result);
        }
    }
}