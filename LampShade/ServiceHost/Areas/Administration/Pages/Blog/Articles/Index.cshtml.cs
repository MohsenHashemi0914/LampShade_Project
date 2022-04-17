using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        public SelectList ArticleCategories { get; set; }
        public ArticleSearchModel SearchModel { get; set; }
        public List<ArticleViewModel> Articles { get; set; }

        #region Constructor

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        #endregion

        public void OnGet(ArticleSearchModel searchModel)
        {
            Articles = _articleApplication.Search(searchModel);
            ArticleCategories = new SelectList(_articleCategoryApplication.GetCategories(), "Id", "Name");
        }
    }
}
