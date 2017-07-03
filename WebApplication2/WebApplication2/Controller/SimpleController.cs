using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Controller
{
    
    public class SimpleController : ControllerBase
    {
        private readonly IHostingEnvironment _appEnvironment;

        public SimpleController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [Route("api/test")]
        public IActionResult Index(int Code)
        {
            return Ok("Hello world" + Code);
        }

        [Route("api/download")]
        public FileResult Download()
        {
            Document doc = new Document();
            FileStream newdocstream = new FileStream("result.pdf", FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, newdocstream);

            doc.Open();

            doc.Add(new Paragraph("Some Text"));

            doc.Close();
            writer.Close();
            newdocstream.Dispose();

            string file_type = "application/pdf";
            string file_name = "book3.pdf";
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "result.pdf");
            return File(new FileStream(file_path, FileMode.Open), file_type, file_name);
        }

    }
}