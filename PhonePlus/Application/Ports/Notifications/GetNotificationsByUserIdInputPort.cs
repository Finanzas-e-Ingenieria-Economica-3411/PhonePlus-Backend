using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Application.Ports.Notifications;

public sealed class GetNotificationsByUserIdInputPort : IInputPort<int,IEnumerable<NotificationResponseDto>>
{
    public int RequestData { get; }
    public IOutputPort<IEnumerable<NotificationResponseDto>> OutputPort { get; }
    
    public GetNotificationsByUserIdInputPort(int requestData, IOutputPort<IEnumerable<NotificationResponseDto>> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}