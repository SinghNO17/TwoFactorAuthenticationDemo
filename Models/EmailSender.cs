using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TwoFactorAuthenticationDemo.Models
{
    public class CustomEmailSender : IEmailSender
    {
        readonly SMTPConfigModel _smtpConfig = null;
        public CustomEmailSender()
        {
            _smtpConfig = new SMTPConfigModel()
            {
                SenderAddress= "tes.test@gmail.com",
     SenderDisplayName= "My Application Team",
    UserName= "07xxxxxx2a3d3699",
     Password =  "xxxxxxxxxxxxxxxx",
     Host= "smtp.mailtrap.io",
    Port =587,
     EnableSSL= true,
     UseDefaultCredentials= true,
     IsBodyHTML= true
            };
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new MailMessage
            {
                Subject = subject,
                Body = htmlMessage,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            mail.To.Add(email);
            

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }
    }

    public class SMTPConfigModel
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }


    }
}
