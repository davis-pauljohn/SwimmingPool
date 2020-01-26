using SwimmingPool.Domain.Infrastucture;
using SwimmingPool.Domain.EmployeeAggregate.Base;
using System;
using System.Threading.Tasks;

namespace SwimmingPool.Domain.EmployeeAggregate
{
    public class Employee : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public EmployeeDetails EmployeeDetails { get; private set; }

        public EmploymentDetails EmploymentDetails { get; private set; }

        private Employee()
        {
        }

        public static Employee Create(string firstName, string lastName, RoleType role, EmploymentType employmentType, DateTime commencementDate)
        {
            var id = Guid.NewGuid();
            var employeeDetails = new EmployeeDetails(id, firstName, lastName);
            var employmentDetails = new EmploymentDetails(id, role, employmentType, commencementDate);
            return new Employee
            {
                Id = id,
                Key = $"{employeeDetails.LastName}_{employeeDetails.FirstName}_{employmentDetails.RoleType.ToString()}_{id}",
                EmployeeDetails = employeeDetails,
                EmploymentDetails = employmentDetails
            };
        }
    }
}