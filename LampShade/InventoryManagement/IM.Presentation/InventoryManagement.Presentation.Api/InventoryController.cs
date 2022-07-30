using _01_LampshadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        #region Constructor

        private readonly IInventoryQuery _inventoryQuery;
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication,
            IInventoryQuery inventoryQuery)
        {
            _inventoryQuery = inventoryQuery;
            _inventoryApplication = inventoryApplication;
        }

        #endregion

        [HttpGet("{id}")]
        public List<InventoryOperationViewModel> GetOperationsBy(long id)
        {
            return _inventoryApplication.GetOperationLog(id);
        }

        [HttpPost]
        public StockStatus CheckStock([FromBody] IsInStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }
    }
}