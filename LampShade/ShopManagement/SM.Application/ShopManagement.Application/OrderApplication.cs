using _0_Framework.Application;
using _0_Framework.Application.Sms;
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
        private readonly ISmsService _smsService;
        private readonly IConfiguration _configuration;
        private readonly IShopAccountAcl _shopAccountAcl;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInverntoryAcl _shopInventoryAcl;

        public OrderApplication(IAuthHelper authHelper, ISmsService smsService, 
            IConfiguration configuration, IShopAccountAcl shopAccountAcl,
            IOrderRepository orderRepository, IShopInverntoryAcl shopInventoryAcl)
        {
            _authHelper = authHelper;
            _smsService = smsService;
            _configuration = configuration;
            _shopAccountAcl = shopAccountAcl;
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
            //var symbol = _configuration.GetValue<string>("Symbol");
            var symbol = _configuration["Symbol"];
            var issueTrackingNo = CodeGenerator.Generate(symbol);
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            order.SetIssueTrackingNo(issueTrackingNo);
            
            if(!_shopInventoryAcl.ReduceFromInventory(order.Items)) return "";

            _orderRepository.SaveChanges();

            var customer = _shopAccountAcl.GetAccountBy(order.AccountId);
            _smsService.Send(customer.mobile,
                $"{customer.name} عزیز سفارش شما با شماره پیگیری {issueTrackingNo} با موفقیت پرداخت شد و ارسال خواهد شد . با تشکر از اعتماد شما");

            return issueTrackingNo;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var order = _orderRepository.Get(id);

            if (order == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            order.Cancel();
            _orderRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<OrderItemViewModel> GetItemsBy(long orderId)
        {
            return _orderRepository.GetItemsBy(orderId);
        }
    }
}
