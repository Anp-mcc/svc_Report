using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Models
{
    public class Report
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public string PieChartData { get; set; }
		public string BarChartData { get; set; }
		public string ImagePath { get; set; }
	}
}
