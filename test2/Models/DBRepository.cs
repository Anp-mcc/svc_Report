using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Models
{
    public class DBRepository : IRepository
    {
        public DBReportContext db;
        private bool disposed = false;

        public DBRepository(DBReportContext _db)
        {
            db = _db;
        }

        public void Create(DBReport item)
        {
            db.Reports.Add(item);
        }

        public void Delete(string hash)
        {
            DBReport del = db.Reports.Find(hash);
            if (del != null)
                db.Reports.Remove(del);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DBReport GetReport(string hash)
        {
            return db.Reports.Find(hash);
        }

        public IEnumerable<DBReport> GetReportsList()
        {
            return db.Reports;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(DBReport item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public bool IsEmpty()
        {
            if (db.Reports.Count() == 0)
                return true;
            else
                return false;
        }
        public int GetCount()
        {
            return db.Reports.Count();
        }
    }
}
