using MediatR;
using PhonePlus.Application.Ports.Auth.Input;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Repositories;

namespace PhonePlus.Application.UseCases.Auth;

public sealed class EmailVerificationUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<EmailVerificationInputPort>
{
    public async Task Handle(EmailVerificationInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userRepository.GetUserByEmailAsync(request.RequestData);
            if (user == null)
            {
                throw new BadHttpRequestException("The user given does not exist.");
            }
            user.VerifyEmail();
            userRepository.Update(user);
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