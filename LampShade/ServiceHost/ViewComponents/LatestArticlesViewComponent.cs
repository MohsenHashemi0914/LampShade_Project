using _01_LampshadeQuery.Contracts.Article;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        #region Constructor

        private readonly IArticleQuery _articleQuery;

        public LatestArticlesViewComponent(IArticleQuery articleQuery)
        {
            _articleQuery = articleQuery;
        }

        #endregion

        public IViewComponentResult Invoke()
        {
            var articles = _articleQuery.GetLatestArticles();
            return View("Default", articles);
        }
    }
}