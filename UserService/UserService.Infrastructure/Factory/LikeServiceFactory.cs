using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces.Factory;
using UserService.Application.Interfaces.Services;

namespace UserService.Infrastructure.Factory;

public class LikeServiceFactory(IServiceScopeFactory serviceScopeFactory) : ILikeServiceFactory, IDisposable
{
    private readonly IServiceScope  _serviceScope = serviceScopeFactory.CreateScope();
    
    public ILikeService CreateLikeService() =>
        _serviceScope.ServiceProvider.GetRequiredService<ILikeService>();

    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}