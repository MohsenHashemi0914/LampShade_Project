namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void Signin(AuthViewModel account);
        void Signout();
        bool IsAuthenticated();
        long CurrentAccountId();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<int> GetCurrentAccountPermissions();
    }
}