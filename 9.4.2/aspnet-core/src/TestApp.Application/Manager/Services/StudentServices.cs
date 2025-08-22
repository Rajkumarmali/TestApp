using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using TestApp.DTOs;
using TestApp.Manager.interfaces;
using TestApp.Models;

namespace TestApp.Manager.Services;

public class StudentServices : ApplicationService, IStudent
{

    private readonly IRepository<Student, long> _studentRepository;
    public StudentServices(IRepository<Student, long> studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<StudentCreateDto> CreateStudent([FromBody] StudentCreateDto dto)
    {
        try
        {
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
}