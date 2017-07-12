﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test2.Models;
using test2.Modules;
using Microsoft.AspNetCore.Hosting;

namespace test2.Controllers
{
	[Route("api/Report")]
	public class ReportController : Controller
	{
		IHostingEnvironment _env;
		public ReportController(IHostingEnvironment env)
		{
			_env = env;
		}
		// GET: api/Report
		[HttpGet]
		public IActionResult Get()
		{
			/**/
			return Ok("ok");
		}

		// GET: api/Report/5
		[HttpGet("{id}", Name = "Get")]
		public FileResult Get(int id)
		{
            string path = _env.ContentRootPath;
            byte[] doc = System.IO.File.ReadAllBytes(path + @"\Temp\" + id.ToString() + ".pdf");
            string file_type = "application/pdf";
            string file_name = "Report.pdf";
            return File(doc, file_type, file_name);
		}

		// POST: api/Report
		[HttpPost]
		public IActionResult Post([FromBody]Report rp)
		{
			string path = _env.ContentRootPath;
            ReportMaker rm = new ReportMaker();
            Task.Run(() => rm.CreateReport(rp, path));
            return Ok("Ваш отчет будет доступен по адресу api/report/" + rp.Id.ToString() + " в ближайшее время");
		}

		// POST: api/Report/5
		[HttpPost("{id}")]
		public IActionResult Post(int id, [FromBody] Report rp)
		{
			if (rp == null)
				return BadRequest();
			var r = new Report();
			r.Id = 1;
			r.ImagePath = "path1";
			r.Name = "reportName";
			r.PieChartData = new Dictionary<string, double>
			{
				{ "pie1", 0 },
				{ "pie2", 1 }
			};
			r.Text = "text1";
			r.BarChartData = new Dictionary<string, double>
			{
				{ "bar1", 0 },
				{ "bar2", 1 }
			};
			return Ok(rp.PieChartData["pie1"]);
		}
	}
}
