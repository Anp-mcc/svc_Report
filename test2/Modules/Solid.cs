using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test2.Models;

namespace test2.Modules
{
    public class Solid
    {
        /*public IHostingEnvironment Env { get; set; }
        public DBReportContext Context { get; set; }*/
        /*public Solid()
        {

        }*/
        public string Generate(IHostingEnvironment _env, Report rp, DBRepository db)
        {
            string path = _env.ContentRootPath;
            string temp = rp.GetHash();
            var rep = db.GetReport(temp);
            DBReport dbr = new DBReport() { Hash = temp, Path = path + @"\Temp\" + Convert.ToString(db.GetCount() + 1) + ".pdf", Id = db.GetCount() + 1 };
            if ((rep == null) || (db.IsEmpty()))
            {
                db.Create(dbr);
                db.Save();
                Task.Run(() => new ReportMaker().CreateReport(rp, path, dbr.Id));
                return "Отчет будет доступен по адресу /api/report/" + dbr.Id.ToString();
            }
            else if ((!System.IO.File.Exists(rep.Path)) && (rep != null))
            {
                Task.Run(() => new ReportMaker().CreateReport(rp, path, rep.Id));
                return "Отчет будет доступен по адресу /api/report/" + rep.Id.ToString();
            }
            else
                return "Отчет доступен по адресу /api/report/" + rep.Id.ToString();
        }
    }
}
