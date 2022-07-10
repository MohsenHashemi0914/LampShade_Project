using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        #region Constructor

        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor httpContext)
        {
            _contextAccessor = httpContext;
        }

        #endregion

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("UserName", account.UserName),
                new Claim("Mobile", account.Mobile),
                new Claim("Permissions", permissions)
            };

            var claimIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            var principal = new ClaimsPrincipal(claimIdentity);

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);
        }

        public void Signout()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public long CurrentAccountId()
        {
            return IsAuthenticated() ? long.Parse(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId")?.Value) : 0;
        }

        public string CurrentAccountRole()
        {
            return IsAuthenticated() ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type is ClaimTypes.Role).Value : null;
        }

        public string GetCurrentAccountMobile()
        {
            return IsAuthenticated() ? _contextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x?.Type is "Mobile")?.Value : null;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var result = new AuthViewModel();
            if (!IsAuthenticated())
                return result;

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.Role = Roles.GetRoleBy(result.RoleId);
            result.UserName = claims.FirstOrDefault(x => x.Type == "UserName").Value;
            result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            return result;
        }

        public List<int> GetCurrentAccountPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();

            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions").Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }
    }
}