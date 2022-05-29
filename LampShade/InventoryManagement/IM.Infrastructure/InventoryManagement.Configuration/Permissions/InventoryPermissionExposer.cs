using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Invertory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventory, nameof(InventoryPermissions.ListInventory)),
                        new PermissionDto(InventoryPermissions.SearchInventory, nameof(InventoryPermissions.SearchInventory)),
                        new PermissionDto(InventoryPermissions.CreateInventory, nameof(InventoryPermissions.CreateInventory)),
                        new PermissionDto(InventoryPermissions.EditInventory, nameof(InventoryPermissions.EditInventory)),
                        new PermissionDto(InventoryPermissions.Increase, nameof(InventoryPermissions.Increase)),
                        new PermissionDto(InventoryPermissions.Reduce, nameof(InventoryPermissions.Reduce)),
                        new PermissionDto(InventoryPermissions.OperationLog, nameof(InventoryPermissions.OperationLog))
                    }
                }
            };
        }
    }
}
