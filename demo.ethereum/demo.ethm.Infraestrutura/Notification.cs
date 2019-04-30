using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.ethm.Infraestrutura
{
    public class Notification
    {
        private List<String> errors = new List<string>();

        public void addError(String message) { errors.Add(message); }
        public Boolean hasErrors()
        {
            return errors.Any();
        }
    }
}
