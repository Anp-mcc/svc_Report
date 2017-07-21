using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace test2.Modules
{
    public class Report
    {
		//public int Id { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public Dictionary<string, double> PieChartData { get; set; }
		public Dictionary<string, double> BarChartData { get; set; }
		public string ImagePath { get; set; }
        public Report()
        {
            Name = "";
            Text = "";
            PieChartData = new Dictionary<string, double>();
            BarChartData = new Dictionary<string, double>();
            ImagePath = "";
        }
        public string GetHash()
        {
            string json = JsonConvert.SerializeObject(this);
            string hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(json)));
            }
            return hash;
        }
        public bool IsValid()
        {
            if ((ImagePath.Length != 0) && (Name.Length != 0) && (Text.Length != 0) && (PieChartData.Count != 0) && (BarChartData.Count != 0))
                return true;
            else
                return false;
        }
        
	}
}
