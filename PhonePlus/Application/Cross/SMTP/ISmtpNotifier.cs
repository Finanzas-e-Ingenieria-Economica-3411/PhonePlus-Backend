namespace PhonePlus.Application.Cross.SMTP;

public interface ISmtpNotifier
{
    Task SendNotification(string message, string subject, string from, string to);
    
}