using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Application.Ports.Notifications;

public sealed class CreateNotificationOutputPort : IOutputPort<bool>
{
    public bool Content { get; private set; }
    public void Handle(bool response)
    {
        Content = response;
    }
}