using MediatR;

namespace PhonePlus.Common.Ports;

public interface IInputPort<IInteractorRequestType, InteractorResponseType> : IRequest
{
    public IInteractorRequestType RequestData { get; }
    public IOutputPort<InteractorResponseType> OutputPort { get; }
    
}