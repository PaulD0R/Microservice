using HotelService.Application.Interfaces.Services;

namespace HotelService.Application.Interfaces.Factories;

public interface IPersonServiceFactory
{
    IPersonService CreatePersonService();
}