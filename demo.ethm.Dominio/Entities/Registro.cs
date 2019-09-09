using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Registro : EntityBase
    {
        public Registro()
        {
            RegistroTransmissao = new HashSet<RegistroTransmissao>();
        }

        public int IdObra { get; set; }

        public int IdArquivo { get; set; }

        [StringLength(200)]
        public string HashTransacao { get; set; }

        // TODO: Criar um Enumerador
        public int Status { get; set; }

        public virtual Obra Obra { get; set; }

        public virtual Arquivo Arquivo { get; set; }

        public virtual ICollection<RegistroTransmissao> RegistroTransmissao { get; set; }
    }
}
