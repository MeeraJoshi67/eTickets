using eTickets.DTO;
using eTickets.Models;
using eTickets.Services.UserService;
using eTickets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using eTickets.Data;
using System.Data;
using Serilog;
using static eTickets.Models.Login;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Serilog.Context;

namespace eTickets.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        public IUserService UserService { get; }
        //private readonly IUserService userService;

        public AuthController(IConfiguration configuration, IUserService userService, AppDbContext context)
        {
            _configuration = configuration;
            _userService = userService;
            _context = context;
        }

        [HttpGet, Authorize(Roles = "Actor")]
        public ActionResult<string> GetMe()
        {

            LogContext.PushProperty("Username", _userService.GetMyName());
            var userName = _userService.GetMyName();
            return Ok(userName);
            //var userName = User?.Identity?.Name;
            //var userName2 = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);
            //return Ok(new { userName, userName2, role } );
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            //LogContext.PushProperty("Username", "Producer");
            LogContext.PushProperty("Username", _userService.GetMyName());
            if (_context.Users.Any(x => x.Username == request.Username))
            {
                return BadRequest("User Already exist");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {

                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role
            };

           
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("wrong password");
            }
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
           
            Response.Cookies.Append("refreshToken", refreshToken.Token);
            var rft = new User
            {
                RefreshToken = refreshToken.Token,
                TokenCreated = refreshToken.Created,
                TokenExpires = refreshToken.Expires,
            };
            _context.Users.Add(rft);
            return Ok(token);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");

            }
            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
           

            return Ok(token);
        }

        [HttpGet,Authorize(Roles ="Actor")]
        public async Task<ActionResult> GetMyName()
        {
           
            return Ok(_userService.GetMyName());
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };
            return refreshToken;
        }
        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            
        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenExpires = newRefreshToken.Expires;
        //}
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;


            return string.Empty;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var CoumputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return CoumputedHash.SequenceEqual(passwordHash);

            }
        }

        
    }
}
    
