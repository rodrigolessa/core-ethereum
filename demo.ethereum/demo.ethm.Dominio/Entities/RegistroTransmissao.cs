using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class RegistroTransmissao : EntityBase
    {
        [Required]
        [StringLength(200)]
        public string HashTransacao { get; set; }

        public DateTime? DataDeConclusao { get; set; }

        [StringLength(200)]
        public string De { get; set; }

        [StringLength(200)]
        public string Para { get; set; }

        [StringLength(200)]
        public string Bloco { get; set; }

        public decimal? Valor { get; set; }

        public int? GasLimite { get; set; }

        public int? GasUsado { get; set; }

        public decimal? GasPreco { get; set; }

        public decimal? CustoFinal { get; set; }

        public int? Nonce { get; set; }

        public int? Posicao { get; set; }

        [StringLength(200)]
        public string DadoEnviado { get; set; }

        [StringLength(200)]
        public string NotaPrivada { get; set; }

        // TODO: Criar um enumerador?
        [StringLength(2)]
        public string Status { get; set; }

        public string Mensagem { get; set; }

        public virtual Registro Registro { get; set; }
    }
}
