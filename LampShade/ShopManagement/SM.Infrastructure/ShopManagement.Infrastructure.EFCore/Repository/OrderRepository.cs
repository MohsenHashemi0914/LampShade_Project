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
    }
}
