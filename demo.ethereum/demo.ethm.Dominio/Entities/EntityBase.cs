using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public abstract class EntityBase
    {
        //[NonSerialized]
        //[NotMapped]
        //IEnumerable<ValidationResult> validationResults = new List<ValidationResult>();

        public int Id { get; protected set; }

        public DateTime DataDeInclusao { get; set; }

        public Nullable<DateTime> DataDeAlteracao { get; set; }
    }
}