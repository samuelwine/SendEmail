using Microsoft.AspNetCore.Identity.UI.Services;

namespace EmailTryout
{
    public interface IAppEmailSender : IEmailSender
    {
        void SendEmail(Message message);
    }
}
