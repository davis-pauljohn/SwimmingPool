using NodaTime;

namespace SwimmingPool.Service.Common.EmailTemplates
{
    public class AccountVerificationEmail : IEmailTemplate
    {
        public string RecipientName { get; set; }
        public string AccountVerificationLink { get; set; }
        public string AccountVerificationLinkExpirationDate { get; set; }

        public AccountVerificationEmail(string recipientName, string accountVerificationLink, Instant accountVerificationLinkExpiration)
        {
            RecipientName = recipientName;
            AccountVerificationLink = accountVerificationLink;
            AccountVerificationLinkExpirationDate = AccountVerificationLinkExpirationDate;
        }

        public string GetTemplate()
        {
            return $"Hello, {RecipientName}, <br />" +
                "Your registration request has been recieved and requires that your email address is verified before your account is finalised. <br />" +
                $"<a href=\"{AccountVerificationLink}\">Click here</a> to verify your email address. <br />" +
                $"This link will exire at {AccountVerificationLinkExpirationDate.ToString()}. <br /><br />" +
                "We hope you enjoy swimmingpool.com";
        }
    }
}