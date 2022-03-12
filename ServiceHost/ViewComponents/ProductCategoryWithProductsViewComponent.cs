using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryQuery.GetProductCategoriesWithProducts();
            return View("Default", categories);
        }
    }
}