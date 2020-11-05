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
        private business.Domains.Account _domain;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(business.Domains.Account domain, IOptions<AuthOptions> authOptions)
        {
            this._domain = domain;
            this._authOptions = authOptions;
        }

        [Route("sign_up")]
        [HttpPost]
        public IActionResult SignUp([FromBody] SignUp request)
        {
            if (this._domain.Exists(request.Email))
            {
                return Conflict();
            }

            this._domain.Create(request);
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

        [Route("login\admin")]
        [HttpPost]
        public IActionResult LoginAdmin([FromBody] Login request)
        {
            var user = AuthenticateAdminUser(request.Email, request.Password);
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

        [Route("reset_password/{code}")]
        [HttpPost]
        public IActionResult ResetPassword(int code, [FromBody] ResetPassword request)
        {
            return Unauthorized();
        }

        public Account AuthenticateUser(string email, string password) =>
            this._domain.Get().SingleOrDefault(user => user.Email == email && Crypto.VerifyHashedPassword(user.Password, password));

        public Account AuthenticateAdminUser(string email, string password) =>
            this._domain.Get().SingleOrDefault(user => user.Email == email /*&& user.Role == Admin*/ && Crypto.VerifyHashedPassword(user.Password, password));

        private string GenerateJWT(Account user)
        {
            var authParam = this._authOptions.Value;

            var securityKey = authParam.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                //new Claim("role", user.Role.ToString())
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
