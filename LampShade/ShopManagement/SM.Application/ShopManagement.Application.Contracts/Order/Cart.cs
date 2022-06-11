namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new();
        }

        public void Add(CartItem cartItem)
        {
            CartItems.Add(cartItem);
            CalculateCart(cartItem);
        }

        #region Utilities

        private void CalculateCart(CartItem cartItem)
        {
            TotalAmount += cartItem.TotalItemPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmount;
        }

        #endregion
    }
}
