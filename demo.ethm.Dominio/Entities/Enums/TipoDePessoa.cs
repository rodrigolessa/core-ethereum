using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum TipoDePessoa
    {
        [Display(Name = "Pessoa Física")]
        PessoaFisica = 1,
        [Display(Name = "Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}
