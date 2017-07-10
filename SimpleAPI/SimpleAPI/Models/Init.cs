using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class Init
    {
		public static void Initialize(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetService<ReportContext>();
			foreach (Report rp in context.Reports)
			{
				if (rp.Id < 0)
					context.Reports.Remove(rp);
			}
			if (!context.Reports.Any())
			{
				context.Reports.AddRange(
					new Report
					{
						BarChartData = "bar", ImagePath = "img",
						PieChartData = "pie", Name = "name", Text = "txt"
					},
					new Report
					{
						BarChartData = "bar", ImagePath = "img",
						PieChartData = "pie", Name = "name", Text = "txt"
					});
				context.SaveChanges();
			}
		}

	}
}
