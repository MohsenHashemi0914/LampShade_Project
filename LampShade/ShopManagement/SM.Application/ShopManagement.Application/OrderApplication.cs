using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.DomainServices;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        #region Constructor

        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInverntoryAcl _shopInventoryAcl;

        public OrderApplication(IAuthHelper authHelper, IOrderRepository orderRepository,
            IConfiguration configuration, IShopInverntoryAcl shopInventoryAcl)
        {
            _authHelper = authHelper;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _shopInventoryAcl = shopInventoryAcl;
        }

        #endregion

        public long PlaceOrder(Cart cart)
        {
            var order = new Order(_authHelper.CurrentAccountId(), cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);

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

            if (!_shopInventoryAcl.ReduceFromInventory(order.Items)) return "";

            _orderRepository.SaveChanges();
            return issueTrackingNo;
        }
    }
}
