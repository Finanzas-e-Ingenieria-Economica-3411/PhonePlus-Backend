using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Credits.Output;

public sealed class SeedStatesOutputPort : IOutputPort<bool>
{
    public bool Data { get; private set; }
    public void Handle(bool response)
    {
        Data = response;
    }
}