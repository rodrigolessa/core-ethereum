using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum Periodicidade : int
    {
        [Display(Name = "Mensal")]
        Mensal = 1,
        [Display(Name = "Trimestral")]
        Trimestral = 2,
        [Display(Name = "Anual")]
        Anual = 3,
        [Display(Name = "Bienal")]
        Bienal = 4
    }
}
