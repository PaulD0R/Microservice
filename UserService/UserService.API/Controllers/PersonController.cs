using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Extensions;
using UserService.Application.Interfaces.Services;

namespace UserService.API.Controllers;

[ApiController]
    [Authorize]
    [Route("WPF/Persons")]
    public class PersonController(IPersonService  personService) : ControllerBase
    {
        [HttpGet("Id/{id}")]
        public async Task<IActionResult> GetPersonById([FromRoute] string id)
        {
            return Ok(await personService.GetByIdAsync(id));
        }

        [HttpGet("Name/{name}")]
        public async Task<ActionResult> GetByName([FromRoute] string name)
        {
            return Ok(await personService.GetByNameAsync(name));
        }

        [HttpGet("Me")]
        public async Task<IActionResult> GetPrivatePerson()
        {
            var personId = User.GetId();
            if (personId == null) return Unauthorized("Не авторизирован");

            return Ok(await personService.GetMeAsync(personId));
        }
    }