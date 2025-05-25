using PhonePlus.Common.Repository;
using PhonePlus.Domain.Models;
using PhonePlus.Interface.DTO.Notification;

namespace PhonePlus.Domain.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserId(int userId);
}