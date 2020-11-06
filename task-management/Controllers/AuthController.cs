using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web.Helpers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using task_management.Common;
using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private business.Domains.AccountDomain _accountDomain;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(business.Domains.AccountDomain accountDomain, IOptions<AuthOptions> authOptions)
        {
            this._accountDomain = accountDomain;
            this._authOptions = authOptions;
        }

        [Route("sign_up")]
        [HttpPost]
        public IActionResult SignUp([FromBody] SignUp request)
        {
            if (this._accountDomain.EmailExists(request.Email))
            {
                return Conflict("Email already exist");
            }

            if (this._accountDomain.UsernameExists(request.Username))
            {
                return Conflict("Username already exist");
            }

            this._accountDomain.Create(request);
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        [Route("login/admin")]
        [HttpPost]
        public IActionResult LoginAdmin([FromBody] Login request)
        {
            var user = AuthenticateAdmin(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        [Route("logout")]
        [HttpPost]
        public IActionResult Logout([FromBody] Logout request)
        {
            return Unauthorized();
        }

        [Route("reset_password")]
        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPassword request)
        {
            return Unauthorized();
        }

        [Route("reset_password/{code}")]
        [HttpPost]
        public IActionResult ResetPassword(int code, [FromBody] ResetPassword request)
        {
            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password) =>
            this._accountDomain.Get().SingleOrDefault(user => user.Email == email && user.Role == Role.User && Crypto.VerifyHashedPassword(user.Password, password));

        private Account AuthenticateAdmin(string email, string password) =>
            this._accountDomain.Get().SingleOrDefault(user => user.Email == email && user.Role == Role.Admin && Crypto.VerifyHashedPassword(user.Password, password));

        private string GenerateJWT(Account user)
        {
            var authParam = this._authOptions.Value;

            var securityKey = authParam.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                authParam.Issuer,
                authParam.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParam.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
