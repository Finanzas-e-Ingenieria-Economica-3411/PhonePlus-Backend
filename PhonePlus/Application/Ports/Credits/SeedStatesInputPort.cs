using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Credits;

namespace PhonePlus.Application.Ports.Credits;

public sealed class SeedStatesInputPort : IInputPort<SeedStatesDto, bool>
{
    public SeedStatesDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public SeedStatesInputPort(SeedStatesDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}