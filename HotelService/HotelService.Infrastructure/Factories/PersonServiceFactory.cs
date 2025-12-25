using HotelService.Application.Interfaces.Factories;
using HotelService.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelService.Infrastructure.Factories;

public class PersonServiceFactory(
    IServiceScopeFactory scopeFactory)
    : IPersonServiceFactory, IDisposable
{
    private readonly IServiceScope  _serviceScope = scopeFactory.CreateScope();
    
    public IPersonService CreatePersonService()
    {
        return  _serviceScope.ServiceProvider.GetRequiredService<IPersonService>();
    }

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}