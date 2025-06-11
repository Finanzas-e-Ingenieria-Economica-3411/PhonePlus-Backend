using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Application.Ports.Notifications.Output;

public sealed class GetNotificationsByUserIdOutputPort :  IOutputPort<IEnumerable<NotificationResponseDto>>
{
    public IEnumerable<NotificationResponseDto> Content { get; private set; }
    public void Handle(IEnumerable<NotificationResponseDto> response)
    {
        Content = response;
    }
}