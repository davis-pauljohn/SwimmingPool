using SwimmingPool.Service.Common.EmailTemplates;

namespace SwimmingPool.Service
{
    public interface ISmtpService
    {
        void SendEmail(string recipient, IEmailTemplate template);
    }
}