using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Log4NetLoggerManager : ILogger
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger("Log4NetLoggerManager");

        #region ILogger Interface Methods 
        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex.Message, ex);
            if (ex.InnerException != null)
                _logger.Error(ex.Message, ex.InnerException);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex.Message, ex);
        }

        #endregion ILogger Interface Methods

    }
}
