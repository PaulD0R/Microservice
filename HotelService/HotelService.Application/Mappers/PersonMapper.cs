using HotelService.Domain.Entities;
using HotelService.Domain.Events;

namespace HotelService.Application.Mappers;

public static class PersonMapper
{
    public static Person ToPerson(this PersonCreateEvent personEvent)
    {
        return new Person
        {
            Id = personEvent.PersonId,
            Name = personEvent.Name
        };
    }
}