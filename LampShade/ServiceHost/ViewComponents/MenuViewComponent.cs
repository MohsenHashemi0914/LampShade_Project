using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, 
            IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var categories = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetLatestArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View("Default", categories);
        }
    }
}