using GenericRepository;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public class GenericUnitOfWork : IDisposable, IGenericUnitOfWork
    {
        #region Class Fields
        private bool _disposed;
        private readonly IDbContext _dbContext;

        private Lazy<GenericRepository<Consultation>> _consultation;
        

        #endregion

        #region Constrcutors
        public GenericUnitOfWork(IDbContext dbContext)
        {
            _disposed = false;
            _dbContext = dbContext;
            _dbContext.Configuration.ProxyCreationEnabled = true;
            _dbContext.Configuration.LazyLoadingEnabled = false;
            CreateRepositories();
        }
        #endregion Constrcutors


        #region Properties
        public IGenericRepository<Consultation> Consultation { get { return _consultation.Value; } }

        #endregion
       


        private void CreateRepositories()
        {

            _consultation = CreateLazyRepository<Consultation>();

        }
        private Lazy<GenericRepository<T>> CreateLazyRepository<T>()
         where T : class
        {
            return new Lazy<GenericRepository<T>>(() => new GenericRepository<T>(_dbContext));
        }

        #region Interface IUnitOfWork
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                // additional log info to assist debugging
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //log.ErrorFormat("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                // still want to propagate this failure exception to the caller
                throw;
            }
        }
        public bool AutoDetectChanges
        {
            get
            {
                return _dbContext.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                _dbContext.Configuration.AutoDetectChangesEnabled = value;
            }
        }



        public IEnumerable<T> ExecuteSQLScript<T>(string sqlScript, SqlParameter[] param = null)
        {
            //var firstName = "John";
            //var id = 12;
            //var sql = @"Update [User] SET FirstName = {0} WHERE Id = {1}";
            //ctx.Database.ExecuteSqlCommand(sql, firstName, id);

            if (param == null)
            {
                return _dbContext.Database.SqlQuery<T>(sqlScript);
            }
            else
                return _dbContext.Database.SqlQuery<T>(sqlScript, param);
        }
        public int ExecuteSqlCommand<T>(string sqlScript)
        {
            return _dbContext.Database.ExecuteSqlCommand(sqlScript);
        }

        public int ExecuteSqlCommand(string sqlScript, SqlParameter[] param)
        {
            return _dbContext.Database.ExecuteSqlCommand(sqlScript, param);
        }
        public IEnumerable<T> ExecuteSQLScript<T>(string sqlScript, SqlParameter param = null)
        {
            //var firstName = "John";
            //var id = 12;
            //var sql = @"Update [User] SET FirstName = {0} WHERE Id = {1}";
            //ctx.Database.ExecuteSqlCommand(sql, firstName, id);

            if (param == null)
                return _dbContext.Database.SqlQuery<T>(sqlScript);
            else
                return _dbContext.Database.SqlQuery<T>(sqlScript, param);
        }

        public IEnumerable<T> ExecuteSQLScriptWithTotal<T>(string sqlScript, out int TotalCount, SqlParameter[] param = null)
        {
            TotalCount = 0;

            if (param == null)
            {
                return _dbContext.Database.SqlQuery<T>(sqlScript);
            }
            else
            {
                var result = _dbContext.Database.SqlQuery<T>(sqlScript, param);
                TotalCount = Convert.ToInt32(param[param.Length - 1].Value);
                return result;
            }

        }
        #endregion Interface IUnitOfWork

        #region Interface IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion Interface IDisposable
    }
}
