using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using ParkyAPI.Repository;

namespace ParkyAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;

        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserDto userDto)
        {
            var data = mapper.Map<User>(userDto);
            var user = userRepo.Authenticate(data.UserName, data.Password);
            if(user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect" });
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("addUser")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var data = mapper.Map<User>(userDto);
            if (!userRepo.IsUniqueUser(data.UserName))
            {
                return BadRequest(new { message = "User already exists!" });
            }
            else
            {
                var user = userRepo.Register(data.UserName, data.Password);
                if (user == null)
                {
                    return BadRequest(new { message = "Failed to add new user!" });
                }
                else
                {
                    return Ok(user);
                }
            }
        }
    }
}
