using NodaTime;
using SwimmingPool.Domain.UserAggregate;
using System;

namespace SwimmingPool.Domain.Infrastucture
{
    public class Entity : IEntity
    {
        public string Key { get; set; }
        public Guid CreatedById { get; private set; }
        public Instant CreatedOn { get; private set; }
        public Guid? UpdatedById { get; private set; }
        public Instant? UpdatedOn { get; private set; }

        public void Update(Guid userId, Instant updatedOn)
        {
            UpdatedById = userId;
            UpdatedOn = updatedOn;
        }
    }
}