using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.DTOs;

namespace TestApp.Manager.interfaces;

public interface IStuCourse : IApplicationService
{
    Task<string> EnrollInCourse(string email, long courseId);
    Task<List<GetCourseDto>> GetEnrolledCourses(string email);
    Task<List<GetStudentDto>> GetAllEnrolledStudents(long courseId);

}