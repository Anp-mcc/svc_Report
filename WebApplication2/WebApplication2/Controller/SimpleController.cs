using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.DataVisualization; // C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0
using System.Web.UI.DataVisualization.Charting;
using System;

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
            var img = Image.GetInstance(Getchart());

            writer.Close();
            newdocstream.Dispose();

            string file_type = "application/pdf";
            string file_name = "book3.pdf";
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "result.pdf");
            return File(new FileStream(file_path, FileMode.Open), file_type, file_name);
        }
        
        Byte [] Getchart()
        {
            // Set 3D chart settings
             Chart chart1 = new Chart();
             Random random = new Random();
             for (int pointIndex = 0; pointIndex < 10; pointIndex++)
             {
                 chart1.Series["Series1"].Points.AddY(random.Next(45, 95));
                 chart1.Series["Series2"].Points.AddY(random.Next(5, 75));
             }

             // Set series chart type
             chart1.Series["Series1"].ChartType = SeriesChartType.Line;
             chart1.Series["Series2"].ChartType = SeriesChartType.Spline;

             // Set point labels
             chart1.Series["Series1"].IsValueShownAsLabel = true;
             chart1.Series["Series2"].IsValueShownAsLabel = true;


             using (MemoryStream memoryStream = new MemoryStream())
             {
                 chart1.SaveImage(memoryStream, ChartImageFormat.Png);
                 return memoryStream.ToArray();
             }          
        }
    }
}