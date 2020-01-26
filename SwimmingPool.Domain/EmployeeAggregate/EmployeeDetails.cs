using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.EmployeeAggregate
{
    public class EmployeeDetails : Entity
    {
        internal string FirstName { get; private set; }
        internal string LastName { get; private set; }
        internal Address MailingAddress { get; private set; }
        internal string PhoneNumber { get; private set; }

        private EmployeeDetails()
        {
        }

        internal EmployeeDetails(Guid employeeId, string firstName, string lastName)
        {
            Key = $"{lastName}_{firstName}_{employeeId}";
            FirstName = firstName;
            LastName = lastName;
        }
    }
}