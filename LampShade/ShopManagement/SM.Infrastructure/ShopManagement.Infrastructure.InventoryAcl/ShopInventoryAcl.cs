using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.DomainServices;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInverntoryAcl
    {
        #region Constructor

        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        #endregion

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command =
                items.Select(x => new ReduceInventory(x.ProductId, x.Count, "خرید مشتری", x.OrderId)).ToList();

            return _inventoryApplication.Reduce(command).IsSucceeded;
        }
    }
}