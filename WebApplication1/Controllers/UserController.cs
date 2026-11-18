using Application.Behaviour.Authorization;
using Application.Behaviour.Course;
using Domain.Entities;
using Domain.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto.Course;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<UserEntity> Register([FromBody] RegisterDto dto)
        {
            UserEntity result = await mediator.Send(dto.Adapt<RegisterCommand>());

            return result;
        }

        [HttpPost("login")]
        public async Task<AccessPair> Login([FromBody] LoginDto dto)
        {
            AccessPair result = await mediator.Send(dto.Adapt<LoginCommand>());

            return result;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public  bool GetMyRole()
        { 

            return HttpContext.User.IsInRole("Admin");
        }
    }

    public class  RegisterDto
    {
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
