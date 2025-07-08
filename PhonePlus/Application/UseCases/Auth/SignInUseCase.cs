using MediatR;
using PhonePlus.Application.Cross.SMTP;
using PhonePlus.Application.Ports.Auth.Input;
using PhonePlus.Common.Auth;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.User;
using hasher = BCrypt.Net.BCrypt;

namespace PhonePlus.Application.UseCases.Auth;

public sealed class SignInUseCase(IUserRepository userRepository, ITokenService tokenService) : IRequestHandler<SignInInputPort>
{
    public async Task Handle(SignInInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetUserByEmailAsync(request.RequestData.Email);
            if (user == null)
            {
                throw new BadHttpRequestException("User with the provided email does not exist.");
            }

            var isPasswordValid = hasher.Verify(request.RequestData.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new BadHttpRequestException("The provided password is incorrect.");
            }
            if (!user.IsEmailVerified)
            {
                throw new BadHttpRequestException("Email not verified.");
            }
            var credentials  = await tokenService.GenerateCredentials(user);
            var response = new UserAuthenticatedResponseDto(
                user.Id, 
                user.Email, 
                credentials.Item1,
                user.Name,
                user.Username,
                user.Dni, 
                credentials.Item2
            );
            request.OutputPort.Handle(response);
        } catch (BadHttpRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    
}