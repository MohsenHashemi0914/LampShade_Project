using _0_Framework.Application.Email;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData] public string LoginMessage { get; set; }
        [TempData] public string RegisterMessage { get; set; }

        #region Constructor

        private readonly IEmailService _emailService;
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication, IEmailService emailService)
        {
            _emailService = emailService;
            _accountApplication = accountApplication;
        }

        #endregion

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            LoginMessage = result.Message;
            return RedirectToPage("./Account");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostRegister(RegisterAccount command)
        { 
            _accountApplication.AccountRegistered += _emailService.SendEmail;
            var result = _accountApplication.Register(command);
            if (!result.IsSucceeded)
                RegisterMessage = result.Message;

            return RedirectToPage("./Account");
        }
    }
}
