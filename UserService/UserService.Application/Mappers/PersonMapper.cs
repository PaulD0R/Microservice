using UserService.Application.Models.Person;
using UserService.Domain.Entities;

namespace UserService.Application.Mappers;

public static class PersonMapper
    {
        public static Person ToPerson(this SignupRequest signupRequest)
        {
            return new Person {
                UserName = signupRequest.Name,
                Email = signupRequest.Email,
            };
        }

        public static PersonDto ToPersonDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.UserName ??  string.Empty
            };
        }

        public static PrivatePersonDto ToPrivatePersonDto(this Person person)
        {
            return new PrivatePersonDto
            {
                Id = person.Id,
                Name = person.UserName ??  string.Empty,
                Email = person.Email ??  string.Empty
            };
        }
    }