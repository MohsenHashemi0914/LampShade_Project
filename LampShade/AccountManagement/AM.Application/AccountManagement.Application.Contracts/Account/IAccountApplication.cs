using _0_Framework.Application;
using _0_Framework.Application.Events;

namespace AccountManagement.Application.Contracts.Account
{
    public delegate void AccountRegisteredEventHandler(object sender, UserDataEventArgs args);

    public interface IAccountApplication
    {
        public event AccountRegisteredEventHandler AccountRegistered;
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