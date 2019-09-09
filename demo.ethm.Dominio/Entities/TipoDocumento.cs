using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class TipoDocumento : EntityBase
    {
        public TipoDocumento()
        {
            Documentos = new HashSet<Documento>();
            Descricoes = new HashSet<TipoDocumentoDescricao>();
        }

        public int Codigo {get;set;}

        public virtual ICollection<Documento> Documentos { get; set; }

        public virtual ICollection<TipoDocumentoDescricao> Descricoes { get; set; }
    }
}
