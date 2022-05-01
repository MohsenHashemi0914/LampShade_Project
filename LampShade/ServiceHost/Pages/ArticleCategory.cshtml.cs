using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleCategoryQueryModel> LatestArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }

        #region Constructor

        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IServiceProvider serviceProvider)
        {
            _articleCategoryQuery = serviceProvider.GetService<IArticleCategoryQuery>();
            _articleQuery = serviceProvider.GetService<IArticleQuery>();
        }

        #endregion

        public void OnGet(string id)
        {
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryWithArticles(id);
            LatestArticleCategories = _articleCategoryQuery.GetLatestArticleCategories();
            LatestArticles = _articleQuery.GetLatestArticles();
        }
    }
}