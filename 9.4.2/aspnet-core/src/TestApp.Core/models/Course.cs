using System.Collections.Generic;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace TestApp.Models;

public class Course : FullAuditedEntity<long>, IMustHaveTenant
{
    public int TenantId { get; set; }
    public string Name { get; set; }

    public ICollection<StuCourse> StuCourses { get; set; }
}