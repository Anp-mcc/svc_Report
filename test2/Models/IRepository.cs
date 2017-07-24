using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Models
{
    interface IRepository : IDisposable
    {
        IEnumerable<DBReport> GetReportsList();
        DBReport GetReport(string hash);
        void Create(DBReport item);
        void Update(DBReport item);
        void Delete(string hash);
        void Save();
        bool IsEmpty();
        int GetCount();
    }
}
