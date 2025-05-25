using MediatR;
using PhonePlus.Application.Ports.Auth;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using hasher = BCrypt.Net.BCrypt;


namespace PhonePlus.Application.UseCases.Auth;

public class SignUpUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository) : IRequestHandler<SignUpInputPort>
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