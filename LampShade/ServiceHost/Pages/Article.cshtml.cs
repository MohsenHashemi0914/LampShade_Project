using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> LatestArticleCategories { get; set; }

        #region Constructor

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        #endregion

        public void OnGet(string id)
        {
            Article = _articleQuery.GetDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            LatestArticleCategories = _articleCategoryQuery.GetLatestArticleCategories();
        }
    }
}
