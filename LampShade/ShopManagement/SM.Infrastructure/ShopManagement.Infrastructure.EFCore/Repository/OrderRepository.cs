using _0_Framework.Infrastructure;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : BaseRepository<long, Order>, IOrderRepository
    {
        #region Constructor

        private readonly ShopContext _context;

        public OrderRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount })
                .FirstOrDefault(x => x.Id == id);

            return order is null ? 0 : order.PayAmount;
        }
    }
}
