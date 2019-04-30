using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Representante : EntityBase
    {
        public Representante()
        {
            RequerenteRepresentantes = new HashSet<RequerenteRepresentante>();
            Obras = new HashSet<Obra>();
        }

        public int IdUsuario { get; set; }

        public int? IdDocumento { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Documento Documento { get; set; }

        public virtual ICollection<RequerenteRepresentante> RequerenteRepresentantes { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }
    }
}
