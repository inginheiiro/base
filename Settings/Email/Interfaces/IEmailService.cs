
using MimeKit;

namespace Base.Settings.Email.Interfaces
{
    public interface IEmailService
    {        
        void Send(MimeMessage message);        
        IEmailConfiguration GetConfiguration();
    }
}