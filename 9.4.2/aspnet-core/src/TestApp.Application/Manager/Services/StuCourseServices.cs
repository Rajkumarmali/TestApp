using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using TestApp.DTOs;
using Microsoft.EntityFrameworkCore;
using TestApp.Manager.interfaces;
using TestApp.Models;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Manager.Services;

public class StuCourseServices : ApplicationService, IStuCourse
{
    private readonly IRepository<Student, long> _studentRepository;
    private readonly IRepository<Course, long> _courseRepository;
    private readonly IRepository<StuCourse, long> _stuCourseRepository;

    public StuCourseServices(IRepository<Student, long> studentRepository, IRepository<Course, long> courseRepository, IRepository<StuCourse, long> stuCourseRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _stuCourseRepository = stuCourseRepository;
    }

    public async Task<string> EnrollInCourse(string email, long courseId)
    {
        try
        {
            var student = await _studentRepository.FirstOrDefaultAsync(s => s.Email == email);
            var studentId = student.Id;
            await _stuCourseRepository.InsertAsync(new StuCourse
            {
                StudentId = studentId,
                CourseId = courseId
            });
            return $"Student with email {email} enrolled in course with ID {courseId} successfully.";
        }
        catch (Exception ex)
        {
            Logger.Error("Error while enrolling in course", ex);
            throw;
        }
    }

    public async Task<List<GetCourseDto>> GetEnrolledCourses(string email)
    {
        try
        {
            var enrolledCourses = await _stuCourseRepository
            .GetAll()
            .Include(sc => sc.Student)
            .Include(sc => sc.Course)
            .Where(sc => sc.Student.Email == email)
            .Select(sc => new GetCourseDto
            {
                Id = sc.Course.Id,
                Name = sc.Course.Name
            }).ToListAsync();
            return enrolledCourses;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while getting enrolled courses", ex);
            throw;
        }
    }
    public async Task<List<GetStudentDto>> GetAllEnrolledStudents(long courseId)
    {
        try
        {
            var enrolledStudent = await _stuCourseRepository
            .GetAll()
            .Include(sc => sc.Student)
            .Include(sc => sc.Course)
            .Where(sc => sc.Course.Id == courseId)
            .Select(sc => new GetStudentDto
            {
                Id = sc.Student.Id,
                FirstName = sc.Student.FirstName,
                LastName = sc.Student.LastName,
                Email = sc.Student.Email,
                PhoneNumber = sc.Student.PhoneNumber
            }).ToListAsync();
            return enrolledStudent;
        }
        catch (Exception ex)
        {
            Logger.Error("Error while getting all enrolled students", ex);
            throw;
        }
    }

}