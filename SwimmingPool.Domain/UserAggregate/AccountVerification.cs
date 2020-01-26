using NodaTime;
using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.UserAggregate
{
    public class AccountVerification : Entity
    {
        public string AccountVerificationToken { get; private set; }
        public Instant AccountVerificationTokenExpiry { get; private set; }
        public bool IsVerified { get; private set; } = false;
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        private AccountVerification()
        {
        }

        internal static AccountVerification Create(Guid userId, string accountVerificationToken, Instant accountVerificationTokenExpiry)
        {
            return new AccountVerification { UserId = userId, AccountVerificationToken = accountVerificationToken, AccountVerificationTokenExpiry = accountVerificationTokenExpiry };
        }

        internal void VerifyAccount(string accountVerificationTokenChallenge, Instant accountVerificationTokenExpiry)
        {
            if (accountVerificationTokenChallenge != AccountVerificationToken || accountVerificationTokenExpiry > AccountVerificationTokenExpiry)
            {
                return;
            }

            IsVerified = true;
        }
    }
}