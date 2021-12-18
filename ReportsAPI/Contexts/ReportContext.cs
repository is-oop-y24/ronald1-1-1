using Microsoft.EntityFrameworkCore;
using ReportsDAL;

namespace ReportsAPI.Contexts
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) :
            base(options){}
        
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
    }
}