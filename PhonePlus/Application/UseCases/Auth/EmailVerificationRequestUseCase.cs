using MediatR;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Application.Ports.Auth.Input;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Auth;

public sealed class EmailVerificationRequestUseCase(ISmtpNotifier smtpNotifier, IUserRepository userRepository) : IRequestHandler<EmailVerificationRequestInputPort>
{
    public async Task Handle(EmailVerificationRequestInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetUserByEmailAsync(request.RequestData);
            if (user == null)
            {
                throw new BadHttpRequestException("User with the provided email does not exist.");
            }
            
            var body = $"<h1>Hi {user.Name},</h1> <h1> Welcome to PhonePlus Platform! Please you need to verify your email before continue </h1> <p>Click <a href='http://localhost:5119/api/v1/auth/verify-email/{user.Email}'>here</a> to verify your email.</p>";
            var subject = "Email Verification";
            var to = user.Email;
            var from = "phoneplus@gmail.com";
            await smtpNotifier.SendNotification(body, subject, from,to);
            request.OutputPort.Handle(true);
        }
        catch (BadHttpRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}