using Microsoft.EntityFrameworkCore;
using SwimmingPool.Domain.UserAggregate;
using System;
using System.Linq;

namespace SwimmingPool.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly SwimmingPoolContext dbContext;

        public UserRepository(SwimmingPoolContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(User aggregate)
        {
            dbContext.Add(aggregate);
            dbContext.SaveChanges();
        }

        public User Find(Guid id)
        {
            return dbContext.Set<User>()
                .Include(u => u.AccountVerification)
                .SingleOrDefault();
        }

        public IQueryable<User> Query()
        {
            return dbContext.Set<User>()
                .Include(u => u.AccountVerification);
        }

        public void Update(User aggregate)
        {
            dbContext.Update(aggregate);
            dbContext.SaveChanges();
        }
    }
}