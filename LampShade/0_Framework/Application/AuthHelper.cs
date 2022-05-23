﻿using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("UserName", account.UserName)
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
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            return claims.Count > 0;
        }

        public string CurrentAccountRole()
        {
            return IsAuthenticated() ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value : null;
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
    }
}