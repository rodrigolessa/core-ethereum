using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum SituacaoDaObra
    {
        [Display(Name = "Rascunho")]
        Rascunho = 1,
        [Display(Name = "Concluida")]
        Concluida = 2,
        [Display(Name = "Aprovação de Pagamento")]
        Aprovacao = 3,
        [Display(Name = "Certificado Emitido")]
        Certificado = 4
    }
}
