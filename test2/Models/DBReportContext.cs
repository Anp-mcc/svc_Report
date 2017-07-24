using Microsoft.EntityFrameworkCore;

namespace test2.Models
{
    public class DBReportContext : DbContext
    {
        public DbSet<DBReport> Reports { get; set; }
        public DBReportContext(DbContextOptions<DBReportContext> options) : base(options)
        {
        }
    }
}
