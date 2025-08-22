using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using TestApp.DTOs;

namespace TestApp.Manager.interfaces;

public interface ICourse : IApplicationService
{
    Task<CreateCouresDto> CreateCourse(CreateCouresDto CourseModel);
    Task<List<GetCourseDto>> GetAllCourses();
    Task<UpdateCourseDto> UpdateCourse(UpdateCourseDto dto);
    Task<string> deleteCourse(long id);
}