using MediatR;
using PhonePlus.Application.Ports.Notifications;
using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace PhonePlus.Application.UseCases.Notifications;

public sealed class CreateNotificationUseCase(IUnitOfWork unitOfWork, INotificationRepository notificationRepository, IUserRepository userRepository) : IRequestHandler<CreateNotificationInputPort>
{
    public async Task Handle(CreateNotificationInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var isUserExists = await userRepository.FindByIdAsync(request.RequestData.UserId);
            if (isUserExists is null)
            {
                throw new BadHttpRequestException("User not found");
            }
            var notification = new Notification(request.RequestData);
            await notificationRepository.AddAsync(notification);
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