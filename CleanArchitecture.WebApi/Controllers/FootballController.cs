using CleanArchitecture.Application;
using CleanArchitecture.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FootballController : ControllerBase
    {
        private readonly IApplication<FootballTeam> _football;

        public FootballController(IApplication<FootballTeam> football)
        {
            _football = football;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_football.GetAll());
        }

        [HttpPost]
        public IActionResult Save(FootballTeam teamDto)
        {
            var team = new FootballTeam()
            {
                Name = teamDto.Name,
                Score = teamDto.Score
            };
            return Ok(_football.Save(team));
        }

    }
}
