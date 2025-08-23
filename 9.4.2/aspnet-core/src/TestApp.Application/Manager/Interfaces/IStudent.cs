using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using TestApp.DTOs;
using TestApp.Models;

namespace TestApp.Manager.interfaces;

public interface IStudent : IApplicationService
{
    Task<StudentCreateDto> CreateStudent(StudentCreateDto StudentModel);
    Task<List<GetStudentDto>> GetAllStudents();
    Task<StudentUpdateDto> UpdateStudent(StudentUpdateDto dto);
    Task<string> DeleteStudent(long id);
    Task<GetStudentDto> GetStudentByEmain(string Email);

}
