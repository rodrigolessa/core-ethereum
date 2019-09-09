using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class RequerenteRepresentante : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Parentesco { get; set; }

        public virtual Requerente Requerente { get; set; }

        public virtual Representante Representante { get; set; }
    }
}
