using System;

namespace SwimmingPool.Domain.Infrastucture
{
    public interface IGloballyIdentifiable
    {
        Guid Id { get; }
    }
}