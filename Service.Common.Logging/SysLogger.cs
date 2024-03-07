using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common.Logging
{
    public class SysLoggerProvider : ILoggerProvider
    {
        private string _host;
        private string _port;
        private readonly Func<string, LogLevel, bool> _filter;

        public SysLoggerProvider(string host, string port, Func<string, LogLevel, bool> filter)
        {
            _host = host;
            _port = port;
            _filter = filter;
        }


        public ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
