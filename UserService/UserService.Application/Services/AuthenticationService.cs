using Microsoft.AspNetCore.Identity;
using UserService.Application.Interfaces.Messages;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Mappers;
using UserService.Application.Models.Person;
using UserService.Application.Models.Token;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class AuthenticationService(
    IPersonRepository personRepository,
    IJwtRepository jwtRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IMessageProducer<PersonDto>  messageProducer,
    UserManager<Person> userManager,
    SignInManager<Person> signInManager)
    : IAuthenticationService
{
    public async Task<TokensDto> SigninAsync(SigninRequest signinRequest)
    {
        var person = await personRepository.GetByNameAsync(signinRequest.Name!);
        if (person == null) throw new BadRequestException("Person with this name not found");
        
        var result = await signInManager.CheckPasswordSignInAsync(person, signinRequest.Password!, false);
        if (!result.Succeeded) throw new Exception("Incorrect password");

        var token = await jwtRepository.CreateJwtAsync(person);
        var refreshToken = await refreshTokenRepository.CreateNewRefreshTokenAsync(person);

        return new TokensDto
        {
            Jwt = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokensDto> SignupAsync(SignupRequest signupRequest)
    {
        var person = signupRequest.ToPerson();
        var createPerson = await userManager.CreateAsync(person, signupRequest.Password!);

        if (!createPerson.Succeeded)
            throw new UsernameAlreadyExistsException(person.UserName!);
            
        var roleResult = await userManager.AddToRoleAsync(person, "User");
        if (!roleResult.Succeeded)
            throw new Exception("failed to issue license");

        var token = await jwtRepository.CreateJwtAsync(person);
        var refreshToken = await refreshTokenRepository.CreateNewRefreshTokenAsync(person);

        await messageProducer.ProduceAsync(person.ToPersonDto());

        return new TokensDto 
        {
            Jwt = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<bool> LogoutAsync(string id)
    {
        return await refreshTokenRepository.DeleteRefreshToken(id) ?
            true : throw new NotFoundException($"Person {id} not found");
    }
}