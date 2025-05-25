namespace PhonePlus.Domain.Models;

public sealed class UserRole
{
    public int Id { get; }
    public string Type { get; private set; }

    public UserRole(string type)
    {
        Type = type;
    }
    
    public UserRole()
    {
        Type = string.Empty;
    }
    
}