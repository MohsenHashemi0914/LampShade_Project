using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Login(Login command);
        void Logout();
        EditAccount GetDetails(long id);
        AccountViewModel GetAccountBy(long id);
        List<AccountViewModel> GetAccouts();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}