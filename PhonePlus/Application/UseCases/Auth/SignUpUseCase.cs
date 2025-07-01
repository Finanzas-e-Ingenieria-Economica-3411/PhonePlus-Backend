using MediatR;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Application.Ports.Auth;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using hasher = BCrypt.Net.BCrypt;


namespace PhonePlus.Application.UseCases.Auth;

public class SignUpUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository, ISmtpNotifier smtpNotifier) : IRequestHandler<SignUpInputPort>
{
    public async Task Handle(SignUpInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var isEmailAlreadyExists = await userRepository.AlreadyExists(request.RequestData.Email);
            if (isEmailAlreadyExists)
            {
                throw new BadHttpRequestException("Email already exists");
            }

            var isDniAndUserNameAlreadyExists =
                await userRepository.AlreadyExists(request.RequestData.Dni, request.RequestData.UserName);
            if (isDniAndUserNameAlreadyExists)
            {
                throw new BadHttpRequestException("DNI and UserName already exists");
            }

            var user = new User(request.RequestData);
            var passwordHash = hasher.HashPassword(user.Password);
            user.UpdatePassword(passwordHash);
            await userRepository.AddAsync(user);
            
            var body = $"<h1>Hi {user.Name},</h1> <h1> Welcome to PhonePlus Platform! Please you need to verify your email before continue </h1> <p>Click <a href='http://localhost:5119/api/v1/auth/verify-email/{user.Email}'>here</a> to verify your email.</p>";
            const string subject = "Email Verification";
            var to = user.Email;
            const string from = "phoneplus@gmail.com";
            await smtpNotifier.SendNotification(body, subject, from,to);
            
            
            await unitOfWork.CompleteAsync();
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