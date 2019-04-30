using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Documento : EntityBase
    {
        public int IdTipoDocumento { get; set; }

        public int IdPais { get; set; }

        [Required]
        [StringLength(50)]
        public string Identificador { get; set; }

        [StringLength(50)]
        public string OrgaoExpedidor { get; set; }

        public DateTime? Validade { get; set; }

        public virtual Requerente Requerente { get; set; }

        public virtual Representante Representante { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}
