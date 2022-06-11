using _01_LampshadeQuery.Contracts;
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

        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculator)
        {
            _cartCalculatorService = cartCalculator;
            Cart = new();
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
        }
    }
}