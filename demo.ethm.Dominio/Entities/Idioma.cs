using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    /// <summary>
    /// https://pt.wikipedia.org/wiki/ISO_639#A
    /// eng en  English inglês  English
    /// por pt Portuguese  português português
    /// </summary>
    public class Idioma : EntityBase
    {
        public Idioma()
        {
            TipoDocumentoDescricoes = new HashSet<TipoDocumentoDescricao>();
            GeneroObraDescricoes = new HashSet<GeneroObraDescricao>();
            PaisDescricoes = new HashSet<PaisDescricao>();
            Usuarios = new HashSet<Usuario>();
        }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        [StringLength(2)]
        public string ISO639Alfa2 { get; set; }

        [StringLength(3)]
        public string ISO639Alfa3 { get; set; }

        [StringLength(100)]
        public string HtmlCode { get; set; }

        //public bool Ativo { get; set; }
        public DateTime? DataDeDesativacao { get; set; }

        public virtual ICollection<TipoDocumentoDescricao> TipoDocumentoDescricoes { get; set; }

        public virtual ICollection<GeneroObraDescricao> GeneroObraDescricoes { get; set; }

        public virtual ICollection<PaisDescricao> PaisDescricoes { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
