using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public CreateRole Command { get; set; }

        #region Constructor

        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        #endregion

        public void OnGet()
        {
        }

        public IActionResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }
    }
}
