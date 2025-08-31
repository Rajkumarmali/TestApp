using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Authorization.Roles;
using TestApp.Authorization.Users;
using TestApp.DTOs;
using TestApp.Manager.interfaces;
using TestApp.Models;
using TestApp.MultiTenancy;
namespace TestApp.Manager.Services;

public class StudentServices : ApplicationService, IStudent
{

    private readonly IRepository<Student, long> _studentRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly TenantManager _tenantManager;




    public StudentServices(IRepository<Student, long> studentRepository, UserManager userManager,
        RoleManager roleManager, IEmailSender emailSender, TenantManager tenantManager)
    {
        _studentRepository = studentRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _tenantManager = tenantManager;

    }
    public async Task<StudentCreateDto> CreateStudent([FromBody] StudentCreateDto dto)
    {
        try
        {

            string fromMail = "rajkumarmali2121@gmail.com";
            string fromPassword = "mhvy yezv dddv yyro";

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


            var year = DateTime.UtcNow.Year.ToString().Substring(2);
            int tenantId = AbpSession.TenantId ?? 1;
            var tenant = await _tenantManager.GetByIdAsync(tenantId);

            var prefix = tenant.Name.Length >= 3
                  ? tenant.Name.Substring(0, 3).ToUpper()
                  : tenant.Name.ToUpper();

            // Find last student with same Year + Prefix
            var lastStudent = await _studentRepository
                .GetAll()
                .Where(s => s.StudentId.StartsWith(year + prefix))
                .OrderByDescending(s => s.StudentId)
                .FirstOrDefaultAsync();

            int nextSeq = 1;
            if (lastStudent != null)
            {
                var lastSeqStr = lastStudent.StudentId.Substring((year + prefix).Length);
                if (int.TryParse(lastSeqStr, out int lastSeq))
                {
                    nextSeq = lastSeq + 1;
                }
            }

            var sequence = nextSeq.ToString("D2"); // 01, 02, ...
            string studentId = $"{year}{prefix}{sequence}";


            var Student = new Student
            {
                TenantId = AbpSession.TenantId ?? 1,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                StudentId = studentId
            };

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add("2021pcecrrajkumar013@poornima.org");
            message.Body = $"Hello {dto.FirstName}, your student account has been created successfully.\n" +
                            $"Your Student ID is: {studentId}";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };
            smtpClient.Send(message);

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
                PhoneNumber = s.PhoneNumber,
                StudentId = s.StudentId
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
            var studentEmail = student.Email;
            var user = await _userManager.FindByEmailAsync(student.Email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
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
                PhoneNumber = student.PhoneNumber,
                StudentId = student.StudentId
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

