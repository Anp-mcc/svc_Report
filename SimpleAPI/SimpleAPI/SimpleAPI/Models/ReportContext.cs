using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleAPI.Models
{
    public class ReportContext : DbContext
    {
		public DbSet<Report> Reports { get; set; }
		public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
		}
	}
}
