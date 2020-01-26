using NodaTime;
using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string GivenName { get; private set; }
        public string FamilyName { get; private set; }
        public string PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public AccountVerification AccountVerification { get; private set; }

        private User()
        {
        }

        public static User Create(string email, string givenName, string familyName, string passwordHash, byte[] passwordSalt, Instant accountVerificationExpiry)
        {
            var userId = Guid.NewGuid();
            var accountVerificationToken = HashCode.Combine(userId, email, givenName, familyName, passwordHash);
            var accountVerification = UserAggregate.AccountVerification.Create(userId, accountVerificationToken.ToString(), accountVerificationExpiry);
            return new User
            {
                Id = userId,
                Email = email,
                GivenName = givenName,
                FamilyName = familyName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                AccountVerification = accountVerification
            };
        }
    }
}