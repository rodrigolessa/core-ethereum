using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace demo.ethm.Dominio.Entities.Enums
{
    public enum TipoDeUsuario
    {
        [Description("Advogado")]
        Advogado = 1,
        [Description("Escritor")]
        Escritor = 2,
        [Description("Escritório")]
        Escritario = 3
    }
}