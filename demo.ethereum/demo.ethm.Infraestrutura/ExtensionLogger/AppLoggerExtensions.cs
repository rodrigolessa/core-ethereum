using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace demo.ethm.Infraestrutura.ExtensionLogger
{
    public static class AppLoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory,
                    Func<string, LogLevel, bool> filter = null, string connectionString = null)
        {
            factory.AddProvider(new AppLoggerProvider(filter, connectionString));
            return factory;
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, string connectionString)
        {
            return AddContext(factory, (_, logLevel) => logLevel >= minLevel, connectionString);
        }
    }
}
