using Microsoft.IdentityModel.Tokens;
using SwimmingPool.Service.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SwimmingPool.Service.Authentication
{
    public interface IAuthenticationService
    {
        SecurityToken Authenticate(LoginModel model);

        void Register(RegistrationModel model);
    }
}