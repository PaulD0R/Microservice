using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Extensions;
using UserService.Application.Interfaces.Services;

namespace UserService.API.Controllers;

[ApiController]
    [Authorize]
    [Route("user-service/persons")]
    public class PersonController(IPersonService  personService) : ControllerBase
    {
        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetPersonById([FromRoute] string id)
        {
            return Ok(await personService.GetByIdAsync(id));
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult> GetByName([FromRoute] string name)
        {
            return Ok(await personService.GetByNameAsync(name));
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetPrivatePerson()
        {
            var personId = User.GetId();
            if (personId == null) return Unauthorized("Не авторизирован");

            return Ok(await personService.GetMeAsync(personId));
        }
    }