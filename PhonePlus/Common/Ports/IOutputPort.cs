namespace PhonePlus.Common.Ports;

public interface IOutputPort<InteractorResponseType>
{
    void Handle(InteractorResponseType response);
}