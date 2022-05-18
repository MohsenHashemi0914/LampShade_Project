using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : BaseRepository<long, Inventory>, IInventoryRepository
    {
        #region Constructor

        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        #endregion

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Inventory
                .Select(x => new InventoryViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UnitPrice = x.UnitPrice,
                    IsInStock = x.IsInStock,
                    CurrentCount = x.CalculateCurrentCount(),
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.NotInStock)
                query = query.Where(x => !x.IsInStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            if (inventory.Any())
                inventory.ForEach(item =>
                    item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _context.Inventory.Find(inventoryId);
            return inventory.Operations
                .Select(x => new InventoryOperationViewModel
                {
                    Id = x.Id,
                    Operation = x.Operation,
                    OperationDate = x.OperationDate.ToFarsi(),
                    Count = x.Count,
                    CurrentCount = x.CurrentCount,
                    Description = x.Description,
                    OperatorId = x.OperatorId,
                    Operator = "مدیر سیستم",
                    OrderId = x.OrderId
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}