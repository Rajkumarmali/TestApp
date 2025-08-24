using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;
using TestApp.DTOs;
using TestApp.Manager.interfaces;
using TestApp.Models;

namespace TestApp.Manager.Services;

public class CourseServices : ApplicationService, ICourse
{
    private readonly IRepository<Course, long> _courseRepository;
    public CourseServices(IRepository<Course, long> courseRepository)
    {
        _courseRepository = courseRepository;
    }
    public async Task<CreateCouresDto> CreateCourse(CreateCouresDto dto)
    {
        try
        {
            var course = new Course
            {
                TenantId = AbpSession.TenantId ?? 1,
                Name = dto.Name
            };
            await _courseRepository.InsertAsync(course);
            return dto;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while creating course", ex);
            throw;
        }
    }
    public async Task<List<GetCourseDto>> GetAllCourses()
    {
        try
        {
            var course = await _courseRepository.GetAllListAsync();
            var result = course.Select(c => new GetCourseDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while getting all courses", ex);
            throw;
        }
    }
    public async Task<UpdateCourseDto> UpdateCourse([FromBody] UpdateCourseDto dto)
    {
        try
        {
            var course = await _courseRepository.FirstOrDefaultAsync(c => c.Id == dto.Id);
            course.Name = dto.Name;
            await _courseRepository.UpdateAsync(course);
            return dto;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while updating course", ex);
            throw;
        }
    }

    public async Task<string> deleteCourse(long id)
    {
        try
        {
            var course = await _courseRepository.FirstOrDefaultAsync(c => c.Id == id);
            await _courseRepository.DeleteAsync(course);
            return $"Course with ID {id} deleted successfully.";
        }
        catch (Exception ex)
        {
            Logger.Error("Error while deleting course", ex);
            throw;
        }
    }

}