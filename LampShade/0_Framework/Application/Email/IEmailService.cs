using _0_Framework.Application.Events;

namespace _0_Framework.Application.Email
{
    public interface IEmailService
    {
        void SendEmail(object sender, UserDataEventArgs args);
    }
}