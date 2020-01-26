using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.LessonAggregate
{
    public class Schedule : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
    }
}