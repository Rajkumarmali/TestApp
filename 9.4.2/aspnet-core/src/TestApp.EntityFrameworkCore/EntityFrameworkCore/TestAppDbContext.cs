using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TestApp.Authorization.Roles;
using TestApp.Authorization.Users;
using TestApp.MultiTenancy;
using TestApp.Models;

namespace TestApp.EntityFrameworkCore
{
    public class TestAppDbContext : AbpZeroDbContext<Tenant, Role, User, TestAppDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StuCourse> StuCourses { get; set; }


        public TestAppDbContext(DbContextOptions<TestAppDbContext> options)
            : base(options)
        {
        }
    }
}
