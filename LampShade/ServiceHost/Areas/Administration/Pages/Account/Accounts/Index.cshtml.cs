using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;

namespace ServiceHost.Areas.Administration.Pages.Account.Accounts
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }

        public SelectList Roles;
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts;

        #region Constructor

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        #endregion

        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.Search(searchModel);
            Roles = new SelectList(_roleApplication.GetList(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount { Roles = _roleApplication.GetList() };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetList();
            return Partial("./Edit", account);
        }

        public IActionResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            return Partial("./ChangePassword");
        }

        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }
    }
}