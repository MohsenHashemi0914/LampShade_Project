using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comment
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public CommentSearchModel SearchModel { get; set; }
        public List<CommentViewModel> Comments;

        #region Constructor

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        #endregion

        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            var result = _commentApplication.Cancel(id);

            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }
    }
}