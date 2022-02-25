using EmployeeCRUDApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUDApp.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
