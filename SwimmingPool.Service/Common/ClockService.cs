using NodaTime;

namespace SwimmingPool.Service.Common
{
    public class ClockService : IClockService
    {
        public Instant Now => SystemClock.Instance.GetCurrentInstant();
    }
}