using Microsoft.EntityFrameworkCore;
using PhonePlus.Domain.Models;
using PhonePlus.Domain.Repositories;
using PhonePlus.Infrastructure.Context;
using PhonePlus.Infrastructure.Repositories.Common;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Infrastructure.Repositories.Notifications;

public sealed class NotificationRepository(AppDbContext context) : BaseRepository<Notification>(context), INotificationRepository
{
    public async Task<IEnumerable<Notification>> GetByUserId(int userId)
    {
        return await context.Set<Notification>()
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
}