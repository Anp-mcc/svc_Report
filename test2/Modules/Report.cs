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
        
	}
}
