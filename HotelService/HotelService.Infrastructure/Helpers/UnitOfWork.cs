using HotelService.Application.Interfaces.Helpers;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore.Storage;

namespace HotelService.Infrastructure.Helpers;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    
    public async Task BeginTransactionAsync()
    {
        if (_transaction != null) return;
        
        _transaction = await  context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await context.SaveChangesAsync();
                await context.Database.CommitTransactionAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}