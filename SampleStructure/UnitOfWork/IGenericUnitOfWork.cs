using GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IGenericUnitOfWork : IDisposable
    {
        IGenericRepository<Consultation> Consultation { get; }
        

        void Save();
        void Commit();
        IEnumerable<T> ExecuteSQLScript<T>(string sqlScript, SqlParameter[] param = null);
        IEnumerable<T> ExecuteSQLScript<T>(string sqlScript, SqlParameter param);
        int ExecuteSqlCommand<T>(string sqlScript);
        int ExecuteSqlCommand(string sqlScript, SqlParameter[] param);
        bool AutoDetectChanges { get; set; }
        IEnumerable<T> ExecuteSQLScriptWithTotal<T>(string sqlScript, out int TotalCount, SqlParameter[] param = null);
    }
}
