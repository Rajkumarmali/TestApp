using System.Threading.Tasks;
using Abp.Application.Services;
using TestApp.DTOs;
using TestApp.Models;

namespace TestApp.Manager.interfaces;

public interface IStudent : IApplicationService
{
    Task<StudentCreateDto> CreateStudent(StudentCreateDto StudentModel);
}
