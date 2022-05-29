using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts, nameof(ShopPermissions.ListProducts)),
                        new PermissionDto(ShopPermissions.SearchProducts, nameof(ShopPermissions.SearchProducts)),
                        new PermissionDto(ShopPermissions.CreateProduct, nameof(ShopPermissions.CreateProduct)),
                        new PermissionDto(ShopPermissions.EditProduct, nameof(ShopPermissions.EditProduct))
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories, nameof(ShopPermissions.ListProductCategories)),
                        new PermissionDto(ShopPermissions.SearchProductCategories, nameof(ShopPermissions.SearchProductCategories)),
                        new PermissionDto(ShopPermissions.CreateProductCategory, nameof(ShopPermissions.CreateProductCategory)),
                        new PermissionDto(ShopPermissions.EditProductCategory, nameof(ShopPermissions.EditProductCategory))
                    }
                }
            };
        }
    }
}
