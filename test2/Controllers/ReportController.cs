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
        DBRepository db;
		IHostingEnvironment _env;
		public ReportController(IHostingEnvironment env, DBReportContext context)
		{
			_env = env;
            db = new DBRepository(context);
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
            if (rp.IsValid())
            {
                var response = new Solid().Generate(_env, rp, db);
                return Ok(response);
            }
            else
                return BadRequest();
        }

		// POST: api/Report/5
		/*[HttpPost("{id}")]
		public IActionResult Post(int id, [FromBody] Report rp)
		{
			if (rp == null)
				return BadRequest();
			return Ok(rp.PieChartData["pie1"]);
		}*/
	}
}
