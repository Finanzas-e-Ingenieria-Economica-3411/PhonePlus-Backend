using PhonePlus.Common.Ports;

namespace PhonePlus.Application.Ports.Auth.Output;

public sealed class SignUpOutputPort : IOutputPort<bool>
{
    public bool Content { get; private set; }
    public void Handle(bool response)
    {
        Content = response;
    }
}