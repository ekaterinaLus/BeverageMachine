using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class EmailService
    {
        public string EmailAddress { get; set; }
        public EmailService()
        {

        }
        public EmailService(string email)
        {
            EmailAddress = email;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("Test1234567.test12@yandex.ru", "Barny");
            MailAddress to = new MailAddress(email);
            MailMessage emailMessage = new MailMessage(from, to);
            emailMessage.Subject = subject;
            emailMessage.Body = message;

            SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("Test1234567.test12@yandex.ru", "1234567Qwerty");
            client.EnableSsl = true;
            await client.SendMailAsync(emailMessage);
        }
    }
}
