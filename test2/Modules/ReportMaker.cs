using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;
using System.IO;
using System.Drawing;
using OxyPlot.Axes;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net.Http;
using System;

namespace test2.Modules
{
    public class ReportMaker
    {
        public string TFName { get; set; }
        public Bitmap DrawPie(Report rp)
        {
            var plPie = new PlotModel { Title = "Pie Chart" };
            var pie = new PieSeries() { AngleSpan = 360, StartAngle = 0, StrokeThickness = 5.0, InsideLabelPosition = 0.65, FontSize = 22 };
            foreach (KeyValuePair<string, double> tmp in rp.PieChartData)
            {
                pie.Slices.Add(new PieSlice(tmp.Key, tmp.Value));
            }
            plPie.Series.Add(pie);
            PngExporter png = new PngExporter();
            Bitmap pieBmp = png.ExportToBitmap(plPie);
            return pieBmp;
        }
        public Bitmap DrawBar(Report rp)
        {
            var plBar = new PlotModel() { Title = "Bar Chart" };
            var bar = new ColumnSeries();
            var cat = new CategoryAxis() { Position = AxisPosition.Bottom };
            foreach (KeyValuePair<string, double> tmp in rp.BarChartData)
            {
                bar.Items.Add(new ColumnItem() { Value = tmp.Value });
                cat.Labels.Add(tmp.Key);
            }
            plBar.Series.Add(bar);
            plBar.Axes.Add(cat);
            PngExporter png = new PngExporter();
            Bitmap barBmp = png.ExportToBitmap(plBar);
            return barBmp;
        }

        public async void DownloadPageAsync(Report rp, string path)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(rp.ImagePath);
            TFName = DateTime.Now.ToString("d-M-yyyy HH-mm-ss");
            HttpContent content = response.Content;
            var result = await content.ReadAsStreamAsync();
            var bmp = System.Drawing.Image.FromStream(result);
            bmp.Save(path + @"\Temp\" + TFName);
            //result.Flush();
            bmp.Dispose();
            result.Dispose();
            content.Dispose();
            response.Dispose();
            client.Dispose();
        }
        public void CreateReport(Report rp, string path, int id)
        {
            DownloadPageAsync(rp, path);

            var doc = new Document(iTextSharp.text.PageSize.A4);

            var pieImg = iTextSharp.text.Image.GetInstance(DrawPie(rp), BaseColor.White);
            var barImg = iTextSharp.text.Image.GetInstance(DrawBar(rp), BaseColor.White);
            pieImg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            barImg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            barImg.ScalePercent(75);
            pieImg.ScalePercent(75);

            System.Drawing.Image img = System.Drawing.Image.FromFile(path + @"\Temp\" + TFName);
            var repImg = iTextSharp.text.Image.GetInstance(img, BaseColor.White);
            repImg.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            repImg.ScalePercent(75);
            img.Dispose();

            var fs = new FileStream(path + @"\Temp\" + id.ToString() + ".pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();

            doc.Add(new Paragraph(rp.Name));
            doc.Add(repImg);
            doc.AddTitle(rp.Name);
            doc.Add(pieImg);
            doc.Add(barImg);
            doc.Add(new Paragraph(rp.Text));

            doc.Close();
            writer.Close();
            //fs.Flush();
            fs.Dispose();
            File.Delete(path + @"\Temp\" + TFName);
        }
    }
}
