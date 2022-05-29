using _0_Framework.Application;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ServiceHost
{
    [HtmlTargetElement(Attributes = "Permission")]
    public class PermissionTagHelper : TagHelper
    {
        public int Permission { get; set; }

        #region Constructor

        private readonly IAuthHelper _authHelper;

        public PermissionTagHelper(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentAccountPermissions = _authHelper.GetCurrentAccountPermissions();
            if(!_authHelper.IsAuthenticated() || currentAccountPermissions.All(x => x != Permission))
            {
                output.SuppressOutput();
                return;
            }

            base.Process(context, output);
        }
    }
}