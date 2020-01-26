using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SwimmingPool.Data;
using SwimmingPool.Domain.UserAggregate;
using SwimmingPool.Service.Authentication.Models;
using SwimmingPool.Service.Common;
using SwimmingPool.Service.Common.EmailTemplates;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SwimmingPool.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
        private const int Pbkdf2IterCount = 1000;
        private const int Pbkdf2SubkeyLength = 256 / 8;
        private const int SaltSize = 128 / 8;

        private readonly IUserRepository userRepository;
        private readonly ISmtpService smtpService;
        private readonly IConfiguration configuration;
        private readonly IClockService clock;

        public AuthenticationService(IUserRepository userRepository, ISmtpService smtpService, IConfiguration configuration, IClockService clock)
        {
            this.userRepository = userRepository;
            this.smtpService = smtpService;
            this.configuration = configuration;
            this.clock = clock;
        }

        public SecurityToken Authenticate(LoginModel model)
        {
            var user = userRepository.Query().SingleOrDefault(u => u.Email.Equals(model.Email));
            if (user == null || !user.AccountVerification.IsVerified)
                return default;

            var hashedPassword = GetPasswordHash(model.Password, user.PasswordSalt);
            if (!user.PasswordHash.Equals(hashedPassword))
                return default;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SwimmingPoolSecurityKey", EnvironmentVariableTarget.Machine))), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "https://swimmingpool.com",
                Audience = "https://swimmingpool.com",
                Expires = DateTime.Now.AddSeconds(10)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }

        private string GetPasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength));
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public void Register(RegistrationModel model)
        {
            var salt = GenerateSalt();
            var hashedPassword = GetPasswordHash(model.Password, salt);
            var user = User.Create(model.Email, model.GivenName, model.FamilyName, hashedPassword, salt, model.AccountVerificationExpiry);
            userRepository.Add(user);

            var accountVerificationLink = string.Concat(configuration.GetSection("Endpoints")["AccountVerification"], $"/{user.AccountVerification.AccountVerificationToken}");
            smtpService.SendEmail(model.Email, new AccountVerificationEmail(model.GivenName, accountVerificationLink, clock.Now));
        }
    }
}