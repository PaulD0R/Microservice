namespace HotelService.Application.Interfaces.Helpers;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}