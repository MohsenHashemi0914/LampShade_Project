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

        public (string name, string mobile) GetAccountBy(long id)
        {
            var account = _accountApplication.GetAccountBy(id);
            return (account.FullName, account.Mobile);
        }
    }
}