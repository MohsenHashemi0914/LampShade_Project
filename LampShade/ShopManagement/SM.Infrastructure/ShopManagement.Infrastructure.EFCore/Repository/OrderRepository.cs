using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : BaseRepository<long, Order>, IOrderRepository
    {
        #region Constructor

        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        #endregion

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount })
                .FirstOrDefault(x => x.Id == id);

            return order is null ? 0 : order.PayAmount;
        }

        public List<OrderItemViewModel> GetItemsBy(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            List<OrderItemViewModel> items = new();
            var order = Get(orderId);
            if (order is null) return items;

            items = order.Items.Select(x => new OrderItemViewModel
            {
                OrderId = order.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                UnitPrice = x.UnitPrice,
                Count = x.Count
            }).ToList();

            items.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();

            var query = _context.Orders
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    PaymentMethodId = x.PaymentMethod,
                    PaymentMethod = PaymentMethod.GetBy(x.PaymentMethod).Name,
                    TotalAmount = x.TotalAmount,
                    PayAmount = x.PayAmount,
                    DiscountAmount = x.DiscountAmount,
                    IssueTrackingNo = x.IssueTrackingNo,
                    RefId = x.RefId,
                    IsPaid = x.IsPaid,
                    IsCanceled = x.IsCanceled,
                    OrderDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.IsCanceled)
                query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).ToList();
            orders.ForEach(x =>
            x.AccountFullName = accounts.FirstOrDefault(a => a.Id == x.AccountId)?.FullName);

            return orders;
        }
    }
}
