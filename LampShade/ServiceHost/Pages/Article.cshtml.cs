using _0_Framework.Presentation;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IServiceProvider serviceProvider)
        {
            _articleQuery = serviceProvider.GetService<IArticleQuery>();
            _articleCategoryQuery = serviceProvider.GetService<IArticleCategoryQuery>();
            _commentApplication = serviceProvider.GetService<ICommentApplication>();
        }

        #endregion

        public void OnGet(string id)
        {
            Article = _articleQuery.GetDetails(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            LatestArticleCategories = _articleCategoryQuery.GetLatestArticleCategories();
        }

        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("./Article", new { id = articleSlug });
        }
    }
}
