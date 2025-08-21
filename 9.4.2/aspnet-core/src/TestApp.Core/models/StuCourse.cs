using Abp.Domain.Entities.Auditing;

namespace TestApp.Models;

public class StuCourse : FullAuditedEntity<long>
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}