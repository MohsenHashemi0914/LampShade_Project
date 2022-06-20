using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        #region Constructor

        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IAuthHelper authHelper, IOrderRepository orderRepository,
            IConfiguration configuration)
        {
            _authHelper = authHelper;
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        #endregion

        public long PlaceOrder(Cart cart)
        {
            var order = new Order(_authHelper.CurrentAccountId(), cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

            cart.CartItems.ForEach(cartItem =>
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DiscountRate);
                order.Add(orderItem);
            });

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var symbol = _configuration.GetSection("Symbol").ToString();
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            order.SetIssueTrackingNo(issueTrackingNo);
            // ToDo : Reduce order items from Inventory 
            _orderRepository.SaveChanges();
            return issueTrackingNo;
        }
    }
}
