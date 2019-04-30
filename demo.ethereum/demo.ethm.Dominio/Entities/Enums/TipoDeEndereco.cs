using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum TipoDeEndereco
    {
        [Display(Name = "Principal")]
        Principal = 1,
        [Display(Name = "Cobrança")]
        Cobranca = 2,
        [Display(Name = "Outros")]
        Outros = 3
    }
}
