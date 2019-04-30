using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class ObraVinculo : EntityBase
    {
        // Obra onde foi criada o vinculo
        //public int IdObra { get; set; }

        public int IdObraOriginal { get; set; }

        // TODO: Resolver conversão para boleano!
        public int? EhSupressaoConteudo { get; set; }

        public int? EhAcrescimoConteudo { get; set; }

        public int? EhPublicacao { get; set; }

        public int? EhMudancaTitulo { get; set; }

        public int? EhTransferenciaTitular { get; set; }

        [StringLength(50)]
        public string Outros { get; set; }

        //public virtual Obra Obra { get; set; }

        public virtual Obra ObraOriginal { get; set; }
    }
}
