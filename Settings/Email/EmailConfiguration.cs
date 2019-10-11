

using Base.Settings.Email.Interfaces;

namespace Base.Settings.Email
{
    public class EmailConfiguration:IEmailConfiguration
    {
        public string PopServer { get; set; }
        
        public int PopPort { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string EmailAccount { get; set; }
        public string EmailPassword{ get; set; }
        public string  ValidationUrl { get; set; }
                
    }
}