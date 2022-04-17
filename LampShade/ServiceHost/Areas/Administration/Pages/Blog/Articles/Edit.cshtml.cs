using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        public SelectList ArticleCategories { get; set; }
        public EditArticle Command { get; set; }

        #region Constructor

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        #endregion

        public void OnGet(long id)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetCategories(), "Id", "Name");
            Command = _articleApplication.GetDetails(id);
        }

        public IActionResult OnPost(EditArticle command)
        {
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
