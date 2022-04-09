using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product { get; set; }

        #region Constructor

        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        #endregion

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            var result = _commentApplication.Add(command);
            return RedirectToPage("./Product", new { id = productSlug });
        }
    }
}
