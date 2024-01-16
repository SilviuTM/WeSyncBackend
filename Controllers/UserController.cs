using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WeSyncBackend.Dtos;
using WeSyncBackend.Models;

namespace WeSyncBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return _context.Users.Select(x => new UserDto(x)).ToList();
        }

        [HttpGet("getOne")]
        public async Task<ActionResult<UserDto>> GetOne(string email)
        {
            await Task.CompletedTask;
            var user =  _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return Conflict();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 11);
            await _context.Users.AddAsync(new Models.User()
            {
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Password = hashedPassword
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(CredentialsDto credentials)
        {
            await Task.CompletedTask;
            var user =  _context.Users.FirstOrDefault(el => el.Email == credentials.Email);
            if (user == null)
                return NotFound();
            bool ok = BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password);
            if (!ok)
                return NotFound();
            return Ok(user);
        }

    }
}
