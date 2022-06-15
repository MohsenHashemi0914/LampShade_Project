using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart { get; set; }
        private const string _cookieName = "cart-items";

        #region Constructor

        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculator,
            ICartService cartService, IProductQuery productQuery)
        {
            Cart = new();
            _cartService = cartService;
            _productQuery = productQuery;
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

        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
            var cartItems = _productQuery.CheckInventoryStatusFor(cart.CartItems);
            return RedirectToPage(cartItems.Any(x => !x.IsInStock) ? "./Cart" : "./Checkout");
        }
    }
}