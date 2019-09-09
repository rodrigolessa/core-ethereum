using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Infraestrutura.ExtensionLogger.Model
{
    public class LogEvento
    {
        public int Id { get; set; }
        public int? EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
