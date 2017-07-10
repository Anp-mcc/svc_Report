using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using Microsoft.AspNetCore.Hosting;

namespace SimpleAPI.Controllers
{
    //[Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : Controller
    {
		ReportContext db;
		IHostingEnvironment _env;
		public ReportController(ReportContext context, IHostingEnvironment env)
		{
			db = context;
			_env = env;
		}
        // GET: api/Report/5
        [HttpGet("{id}", Name = "Get")]
        public FileResult Get(int id)
        {
			byte[] doc = System.IO.File.ReadAllBytes(_env.ContentRootPath + @"\tmp\Osi_kr.pdf");
			string file_type = "application/pdf";
			string file_name = "Test.pdf";
			return File(doc, file_type, file_name);
		}
        // POST: api/Report
        [HttpPost]
        public IActionResult Post([FromBody]Report rp)
        {
			if (rp == null)
				return BadRequest();
			db.Reports.Add(rp);
			db.SaveChanges();
			return Ok(rp);
        }
        
        // POST: api/Report/5
        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody] Report rp)
        {
			if (rp == null)
				return BadRequest();
			return Ok(rp);
		}
    }
}
