namespace ShopManagement.Application.Contracts
{
    public class PaymentMethod
    {
        public byte Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private PaymentMethod(byte id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1, "پرداخت آنلاین", "در این مدل شما به درگاه پرداخت اینترنتی هدایت شده و در لحظه پرداخت را انجام خواهید داد ."),
                new PaymentMethod(2, "پرداخت نقدی", "در این مدل ابتدا سفارش ثبت شده و سپس وجه بصورت نقدی در زمان تحویل کالا دریافت خواهد شد .")
            };
        }

        public static PaymentMethod GetBy(byte id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
    }
}
