using System.Net;
using System.Net.Mail;
using PhonePlus.Application.Cross.SMTP;

namespace PhonePlus.Infrastructure.Cross.SMTP;

public class SmtpNotifier : ISmtpNotifier
{
    public async Task SendNotification(string message, string subject, string from, string to)
    {
       
        var mailMessage = new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };
        
        mailMessage.To.Add(to); 
        using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
        smtpClient.Credentials = new NetworkCredential("aljandro.jave@gmail.com", "ogwq gwhs fvdf gkkk");
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(mailMessage);
    }

    
}