using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApp.Authorization.Roles;
using TestApp.Authorization.Users;
using TestApp.DTOs;
using TestApp.Manager.interfaces;
using TestApp.Models;

namespace TestApp.Manager.Services;

public class StudentServices : ApplicationService, IStudent
{

    private readonly IRepository<Student, long> _studentRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public StudentServices(IRepository<Student, long> studentRepository, UserManager userManager,
        RoleManager roleManager)
    {
        _studentRepository = studentRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<StudentCreateDto> CreateStudent([FromBody] StudentCreateDto dto)
    {
        try
        {
            var user = new User
            {
                TenantId = AbpSession.TenantId,
                Name = dto.FirstName,
                Surname = dto.LastName,
                EmailAddress = dto.Email,
                UserName = dto.Email,
                IsEmailConfirmed = true,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, "Test@123");
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(user, "Student");


            var Student = new Student
            {
                TenantId = AbpSession.TenantId ?? 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };
            await _studentRepository.InsertAsync(Student);
            return dto;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while creating student", ex);
            throw;
        }
    }
    public async Task<List<GetStudentDto>> GetAllStudents()
    {
        try
        {
            var student = await _studentRepository.GetAllListAsync();
            var result = student.Select(s => new GetStudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber
            }).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while getting all students", ex);
            throw;
        }
    }

    public async Task<StudentUpdateDto> UpdateStudent([FromBody] StudentUpdateDto dto)
    {
        try
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.Id == dto.Id);
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.PhoneNumber = dto.PhoneNumber;
            student.Email = dto.Email;
            await _studentRepository.UpdateAsync(student);
            return dto;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while updating student", ex);
            throw;
        }
    }

    public async Task<string> DeleteStudent(long id)
    {
        try
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.Id == id);
            await _studentRepository.DeleteAsync(student);
            return $"Student with ID {id} deleted successfully.";
        }
        catch (Exception ex)
        {
            Logger.Error("Error while deleting student", ex);
            throw;
        }
    }
    public async Task<GetStudentDto> GetStudentByEmain(string Email)
    {
        try
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.Email == Email);
            var resutl = new GetStudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber
            };
            return resutl;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while getting student by email", ex);
            throw;
        }
    }
}

