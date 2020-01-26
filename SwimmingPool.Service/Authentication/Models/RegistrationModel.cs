using NodaTime;

namespace SwimmingPool.Service.Authentication.Models
{
    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Instant AccountVerificationExpiry { get; set; }
    }
}