using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace test2.Models
{
    public static class Cleaner
    {
        public static void Clear(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DBReportContext>();
            context.Reports.RemoveRange(context.Reports);
            context.SaveChanges();
        }
    }
}
