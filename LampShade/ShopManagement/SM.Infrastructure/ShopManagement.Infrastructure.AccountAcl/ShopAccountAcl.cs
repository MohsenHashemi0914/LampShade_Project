using AccountManagement.Application.Contracts.Account;
using ShopManagement.Domain.DomainServices;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopAccountAcl : IShopAccountAcl
    {
        #region Constructor

        private readonly IAccountApplication _accountApplication;

        public ShopAccountAcl(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        #endregion

        public KeyValuePair<string, string> GetAccountBy(long id)
        {
            var account = _accountApplication.GetAccountBy(id);
            return new KeyValuePair<string, string>(account.FullName, account.Mobile);
        }
    }
}