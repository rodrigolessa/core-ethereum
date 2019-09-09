using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Pais : EntityBase
    {
        public Pais()
        {
            Requerentes = new HashSet<Requerente>();
            Documentos = new HashSet<Documento>();
            Descricoes = new HashSet<PaisDescricao>();
        }

        [StringLength(2)]
        public string ISO3166Alfa2 { get; set; }

        [StringLength(3)]
        public string ISO3166Alfa3 { get; set; }

        public int? CodigoTelefone { get; set; }

        //public bool Ativo { get; set; }
        public DateTime? DataDeDesativacao { get; set; }

        public virtual ICollection<Requerente> Requerentes { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }

        public virtual ICollection<PaisDescricao> Descricoes { get; set; }
    }
}
