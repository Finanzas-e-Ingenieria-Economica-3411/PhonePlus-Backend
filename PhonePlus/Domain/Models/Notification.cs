using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Domain.Models;

public sealed class Notification
{
    public int Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int UserId { get; private set; }

    public Notification(CreateNotificationRequestDto dto)
    {
        Title = dto.Title;
        Description = dto.Description;
        UserId = dto.UserId;
    }

    public Notification()
    {
        Title = string.Empty;
        Description = string.Empty;
        UserId = 0;
    }
}