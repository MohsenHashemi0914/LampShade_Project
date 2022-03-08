using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ProductViewModel = InventoryManagement.Application.Contracts.Inventory.ProductViewModel;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public SelectList Products;
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;

        #region Constructor

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        #endregion

        public void OnGet(InventorySearchModel searchModel)
        {
            Inventory = _inventoryApplication.Search(searchModel);
            Products = new SelectList(GetProductsForSelect(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = GetProductsForSelect()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = GetProductsForSelect();
            return Partial("./Edit", inventory);
        }

        public IActionResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Increase", command);
        }

        public IActionResult OnPostIncrease(IncreaseInventory command)
        {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }

        public IActionResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLog(long id)
        {
            var operationLog = _inventoryApplication.GetOperationLog(id);
            return Partial("./OperationLog", operationLog);
        }

        #region Utilities

        private List<ProductViewModel> GetProductsForSelect()
        {
            return _productApplication.GetProducts().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        #endregion
    }
}