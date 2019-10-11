using Base.Settings.Email.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Base.Settings.Email
{
    public class EmailService: IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
 
        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
 
        
        public IEmailConfiguration GetConfiguration()
        {
            return _emailConfiguration; 
        }
     
        public void Send(MimeMessage message)
        {                       

            message.From.Add (new MailboxAddress ("Administrador", _emailConfiguration.EmailAccount));

            try
            {

                using (var client = new SmtpClient())
                {
                    client.SslProtocols = System.Security.Authentication.SslProtocols.Tls |
                                          System.Security.Authentication.SslProtocols.Tls12 |
                                          System.Security.Authentication.SslProtocols.Tls11;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_emailConfiguration.EmailAccount, _emailConfiguration.EmailPassword);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}