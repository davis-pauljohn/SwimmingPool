using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.FacilityAggregate
{
    public class Facility : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
    }
}