using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.Menu;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IMenuQuery _menuQuery;

        public MenuViewComponent(IServiceProvider serviceProvider)
        {
            _menuQuery = serviceProvider.GetService<IMenuQuery>();
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var categories = _menuQuery.GetDataForMenu();
            return View("Default", categories);
        }
    }
}