using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class GeneroObraDescricao : EntityBase
    {
        public int IdIdioma { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        public virtual GeneroObra GeneroObra { get; set; }

        public virtual Idioma Idioma { get; set; }
    }
}
