using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test2.Models;
using test2.Modules;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace test2.Controllers
{
	[Route("api/Report")]
	public class ReportController : Controller
	{
        DBReportContext db;
		IHostingEnvironment _env;
		public ReportController(IHostingEnvironment env, DBReportContext context)
		{
			_env = env;
            db = context;
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
            byte[] doc;
            if (System.IO.File.Exists(path + @"\Temp\" + id.ToString() + ".pdf"))
            {
                doc = System.IO.File.ReadAllBytes(path + @"\Temp\" + id.ToString() + ".pdf");
            }
            else
            {
                doc = Encoding.UTF8.GetBytes("File doesn't exist");
            }
            string file_type = "application/json";
            string file_name = "Report";
            return File(doc, file_type, file_name);

		}

		// POST: api/Report
		[HttpPost]
		public IActionResult Post([FromBody]Report rp)
		{
			string path = _env.ContentRootPath;
            string temp = rp.GetHash();
            var rep = db.Reports.Find(temp);
            DBReport dbr = new DBReport() { Hash = temp, Path = path + @"\Temp\" + Convert.ToString(db.Reports.Count() + 1) + ".pdf", Id = db.Reports.Count() + 1 };
            if ((rep == null) || (!db.Reports.Any()))
            {
                db.Reports.Add(dbr);
                db.SaveChanges();
                Task.Run(() => new ReportMaker().CreateReport(rp, path, dbr.Id));
                return Ok(dbr);
            }
            else if ((!System.IO.File.Exists(rep.Path)) && (rep != null))
            {
                Task.Run(() => new ReportMaker().CreateReport(rp, path, rep.Id));
                return Ok(rep);
            }
            else
                return Ok(rep);
        }

		// POST: api/Report/5
		[HttpPost("{id}")]
		public IActionResult Post(int id, [FromBody] Report rp)
		{
			if (rp == null)
				return BadRequest();
			return Ok(rp.PieChartData["pie1"]);
		}
	}
}
