using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum SituacaoDoUsuario
    {
        [Display(Name = "Aguardando confirmação")]
        Aguardando = 1,
        [Display(Name = "Liberado")]
        Liberado = 2,
        [Display(Name = "Teste")]
        Teste = 3,
        [Display(Name = "Bloqueado")]
        Bloqueado = 4
    }
}
