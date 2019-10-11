namespace Base.Settings.Email.Interfaces
{
    public interface  IEmailConfiguration
    {
        
        string PopServer { get; set; }
        int PopPort { get; set; }
        string SmtpServer { get; set; }
        int SmtpPort { get; set; }     
        string EmailAccount { get; set; }
        string EmailPassword{ get; set; }
        string  ValidationUrl { get; set; }
    }
}