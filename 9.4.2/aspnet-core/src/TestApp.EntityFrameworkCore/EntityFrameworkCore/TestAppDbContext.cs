using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TestApp.Authorization.Roles;
using TestApp.Authorization.Users;
using TestApp.MultiTenancy;

namespace TestApp.EntityFrameworkCore
{
    public class TestAppDbContext : AbpZeroDbContext<Tenant, Role, User, TestAppDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TestAppDbContext(DbContextOptions<TestAppDbContext> options)
            : base(options)
        {
        }
    }
}
