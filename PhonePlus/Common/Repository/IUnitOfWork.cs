namespace PhonePlus.Common.Repository;

public interface IUnitOfWork
{
    Task CompleteAsync();
}