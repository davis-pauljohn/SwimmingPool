using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwimmingPool.Service.Authentication;
using SwimmingPool.Service.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;

namespace SwimmingPool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody]LoginModel model)
        {
            var token = authenticationService.Authenticate(model) as JwtSecurityToken;
            if (token == default)
                return Unauthorized("Please ensure that your account is verified and that your email address and password are correct");

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                Expires = DateTimeOffset.Now.AddSeconds(100)
            };

            Response.Cookies.Append("SwimmingPoolToken", token.RawData, cookieOptions);

            return Ok(token.RawData);
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody]RegistrationModel model)
        {
            authenticationService.Register(model);

            return Ok("An email has been sent to your nominated email address. Please verify your account by following the link in this email.");
        }
    }
}