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
                        new PermissionDto(50, "ListInvertory"),
                        new PermissionDto(51, "SearchInvertory"),
                        new PermissionDto(52, "CreateInvertory"),
                        new PermissionDto(53, "EditInvertory")
                    }
                }
            };
        }
    }
}
