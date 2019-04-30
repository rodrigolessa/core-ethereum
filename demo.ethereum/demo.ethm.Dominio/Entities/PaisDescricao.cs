using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class PaisDescricao : EntityBase
    {
        public int IdIdioma { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }

        public virtual Idioma Idioma { get; set; }

        public virtual Pais Pais { get; set; }
    }
}
