using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Application.Ports.Notifications.Input;

public sealed class CreateNotificationInputPort : IInputPort<CreateNotificationRequestDto,bool>
{
    public CreateNotificationRequestDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public CreateNotificationInputPort(CreateNotificationRequestDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}