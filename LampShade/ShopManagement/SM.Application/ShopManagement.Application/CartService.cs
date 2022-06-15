using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartService : ICartService
    {
        private Cart _cart;

        public Cart Get()
        {
            return _cart;
        }

        public void Set(Cart cart)
        {
            _cart = cart;
        }
    }
}
