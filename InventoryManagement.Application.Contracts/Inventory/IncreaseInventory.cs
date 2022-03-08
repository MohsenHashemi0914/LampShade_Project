using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long Count { get; set; }

        public string Description { get; set; }
    }
}