using MediatR;
using PhonePlus.Application.Ports.Notifications;
using PhonePlus.Application.Ports.Notifications.Input;
using PhonePlus.Domain.Repositories;
using PhonePlus.Interface.DTO.Notification;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace PhonePlus.Application.UseCases.Notifications;

public sealed class GetNotificationsByUserIdUseCase(INotificationRepository notificationRepository) : IRequestHandler<GetNotificationsByUserIdInputPort>
{
    public async Task Handle(GetNotificationsByUserIdInputPort request, CancellationToken cancellationToken)
    {
        try
        {
            var notifications = await notificationRepository.GetByUserId(request.RequestData);
            if (notifications == null)
            {
                throw new BadHttpRequestException("This user does not have any notifications");
            }
            var notificationResponse =
                notifications.Select(x => new NotificationResponseDto(x.Id, x.Title, x.Description, x.UserId));
            request.OutputPort.Handle(notificationResponse.ToList());
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