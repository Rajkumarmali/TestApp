using Abp.Application.Services;
using Abp.Net.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TestApp.Manager.interfaces;

namespace TestApp.Manager.Services;

public class TestEmail : ApplicationService, ITestEmail
{
    public async Task<string> SendEmail()
    {
        string fromMail = "rajkumarmali2121@gmail.com";
        string fromPassword = "mhvy yezv dddv yyro";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = "Test Subject";
        message.To.Add("2021pcecrrajkumar013@poornima.org");
        message.Body = "Test Body";

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true
        };
        smtpClient.Send(message);


        return "";
    }

}

