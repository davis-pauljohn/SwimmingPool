using SwimmingPool.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwimmingPool.Data
{
    public interface IUserRepository : IRepository<User>
    {
    }
}