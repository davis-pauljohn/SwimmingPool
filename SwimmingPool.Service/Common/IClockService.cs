using NodaTime;

namespace SwimmingPool.Service.Common
{
    public interface IClockService
    {
        Instant Now { get; }
    }
}