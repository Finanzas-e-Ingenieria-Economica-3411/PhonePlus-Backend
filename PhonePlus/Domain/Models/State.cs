namespace PhonePlus.Domain.Models;

public sealed class State
{
    public int Id { get;  }
    public string Type { get; private set; }

    public State(string type)
    {
        Type = type;
    }

    public State()
    {
        Type = string.Empty;
    }
}