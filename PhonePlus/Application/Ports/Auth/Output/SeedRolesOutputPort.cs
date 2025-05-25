using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Auth.Output;

public sealed class SeedRolesOutputPort : IOutputPort<bool>
{
    
    public bool Data { get; private set; }
    public void Handle(bool response)
    {
        Data = response;
    }
}