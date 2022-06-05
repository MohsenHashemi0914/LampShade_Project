using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; }
        private const string _cookieName = "cart-items";

        #region Constructor

        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        #endregion

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[_cookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value) ?? new();
            cartItems.ForEach(x => x.TotalItemPrice = x.UnitPrice * x.Count);
            CartItems = _productQuery.CheckInventoryStatusFor(cartItems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[_cookieName];
            Response.Cookies.Delete(_cookieName);

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var indexForRemove = cartItems.FindIndex(x => x.Id == id);
            cartItems.RemoveAt(indexForRemove);

            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(_cookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("./Cart");
        }
    }
}