using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Notifications.Output;

public sealed class CreateNotificationOutputPort : IOutputPort<bool>
{
    public bool Content { get; private set; }
    public void Handle(bool response)
    {
        Content = response;
    }
}