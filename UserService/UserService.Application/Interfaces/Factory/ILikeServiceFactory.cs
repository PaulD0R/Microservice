using UserService.Application.Interfaces.Services;

namespace UserService.Application.Interfaces.Factory;

public interface ILikeServiceFactory
{
    ILikeService CreateLikeService();
}