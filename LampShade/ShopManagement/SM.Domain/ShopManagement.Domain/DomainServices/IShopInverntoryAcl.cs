using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.DomainServices
{
    public interface IShopInverntoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> items);
    }
}
