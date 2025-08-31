using System.Collections;
using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace TestApp.Models
{
    public class Student : FullAuditedEntity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StudentId { get; set; }

        public ICollection<StuCourse> StuCourses { get; set; }
    }
}