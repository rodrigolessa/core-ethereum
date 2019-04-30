using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class GeneroObra : EntityBase
    {
        public GeneroObra()
        {
            Descricoes = new HashSet<GeneroObraDescricao>();
            Obras = new HashSet<Obra>();
        }

        public int Codigo { get; set; }

        public virtual ICollection<GeneroObraDescricao> Descricoes { get; set; }

        public virtual ICollection<Obra> Obras { get; set; }
    }
}
