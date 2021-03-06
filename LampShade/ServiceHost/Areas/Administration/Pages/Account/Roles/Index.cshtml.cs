using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<RoleViewModel> Roles;

        #region Constructor

        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        #endregion

        public void OnGet()
        {
            Roles = _roleApplication.GetList();
        }
    }
}