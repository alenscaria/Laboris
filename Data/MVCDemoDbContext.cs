using ASPNETMVCCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Data;

public class MVCDemoDbContext : DbContext
{
    public MVCDemoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> EmployeesDetails { get; set; }
}
