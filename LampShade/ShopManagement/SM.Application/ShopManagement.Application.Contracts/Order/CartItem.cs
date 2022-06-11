namespace ShopManagement.Application.Contracts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double UnitPrice { get; set; }
        public int Count { get; set; }
        public double TotalItemPrice { get; set; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }

        public CartItem()
        {
            CalculateTotalItemPrice();
            CalculateItemPayAmount();
        }

        public void CalculateTotalItemPrice()
        {
            TotalItemPrice = (UnitPrice * Count);
        }

        public void CalculateItemPayAmount()
        {
            ItemPayAmount = (TotalItemPrice - DiscountAmount);
        }

        public void CalculateItemDiscount(int discountRate)
        {
            DiscountRate = discountRate;
            DiscountAmount = ((TotalItemPrice * DiscountRate) / 100);
            CalculateItemPayAmount();
        }
    }
}