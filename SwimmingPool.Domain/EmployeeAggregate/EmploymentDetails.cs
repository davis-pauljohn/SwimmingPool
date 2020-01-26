using SwimmingPool.Domain.EmployeeAggregate.Base;
using SwimmingPool.Domain.Infrastucture;
using System;

namespace SwimmingPool.Domain.EmployeeAggregate
{
    public class EmploymentDetails : Entity
    {
        public DateTime CommencementDate { get; private set; }

        public DateTime? TerminationDate { get; private set; }

        public EmploymentType EmploymentType { get; private set; }
        public RoleType RoleType { get; private set; }

        private EmploymentDetails()
        {
        }

        internal EmploymentDetails(Guid employeeId, RoleType roleType, EmploymentType employmentType, DateTime commencementDate)
        {
            Key = $"{roleType.ToString()}_{employmentType.ToString()}_{commencementDate.ToString("yyyyMMdd")}_{employeeId}";
            RoleType = roleType;
            EmploymentType = employmentType;
            CommencementDate = commencementDate;
        }
    }
}