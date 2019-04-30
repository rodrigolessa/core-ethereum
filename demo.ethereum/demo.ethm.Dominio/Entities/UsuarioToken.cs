using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class UsuarioToken : EntityBase
    {
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(128)]
        public string Token { get; set; }

        // TODO: Resolver conversão para boleano!
        public int Ativo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
