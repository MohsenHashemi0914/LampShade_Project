using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public EditRole Command { get; set; }
        public List<SelectListItem> Permissions = new();

        #region Constructor

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        #endregion

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };
                    value.ForEach(permission =>
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        Permissions.Add(item);
                    });
                }
            }
        }

        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            if (!result.IsSucceeded)
                Message = result.Message;

            return RedirectToPage("./Index");
        }
    }
}
