using NodaTime;
using System;

namespace SwimmingPool.Domain.Infrastucture
{
    public interface IAuditable
    {
        Guid CreatedById { get; }
        Instant CreatedOn { get; }
        Guid? UpdatedById { get; }
        Instant? UpdatedOn { get; }
    }
}