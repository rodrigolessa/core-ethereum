using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace demo.ethm.Infraestrutura.ExtensionLogger
{
    public class AppLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filtro;
        private readonly string _connectionString;

        public AppLoggerProvider(Func<string, LogLevel, bool> filtro, string connectionString)
        {
            _filtro = filtro;
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string nomeCategoria)
        {
            return new AppLogger(nomeCategoria, _filtro, _connectionString);
        }

        public void Dispose()
        {

        }
    }
}