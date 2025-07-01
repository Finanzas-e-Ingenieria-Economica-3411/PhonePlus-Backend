using PhonePlus.Interface.DTO.Auth;

namespace PhonePlus.Domain.Models;

public sealed class User
{
    public int Id { get;  }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Dni { get; private set; }
    public string Name { get; private set; }
    public string Username { get; private set; }
    
    public int RoleId { get; private set; }
    
    public bool IsEmailVerified { get; private set; }

    public User(SignUpDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
        Name = dto.Name;
        Username = dto.UserName;
        Dni = dto.Dni;
        IsEmailVerified = false;
        RoleId = dto.RoleId;
    }

    public User()
    {
        Email = string.Empty;
        Password = string.Empty;
        Name = string.Empty;
        Username = string.Empty;
        Dni = string.Empty;
        IsEmailVerified = false;
        RoleId = 0;
        
    }
    
    
    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }
    
}