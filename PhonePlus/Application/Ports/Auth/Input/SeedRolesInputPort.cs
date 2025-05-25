using PhonePlus.Common.Ports;
using PhonePlus.Interface.DTO.Auth;

namespace PhonePlus.Application.Ports.Auth;

public sealed class SeedRolesInputPort : IInputPort<SeedRolesDto, bool>
{
    public SeedRolesDto RequestData { get; }
    public IOutputPort<bool> OutputPort { get; }
    
    public SeedRolesInputPort(SeedRolesDto requestData, IOutputPort<bool> outputPort)
    {
        RequestData = requestData;
        OutputPort = outputPort;
    }
}