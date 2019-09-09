using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class Arquivo : EntityBase
    {
        public Arquivo()
        {
            Registros = new HashSet<Registro>();
        }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(4)]
        public string Extensao { get; set; }

        [StringLength(20)]
        public string Mime { get; set; }

        [Required]
        [StringLength(20)]
        public string Tamanho { get; set; }

        [Required]
        [StringLength(32)]
        public string MD5 { get; set; }

        [Required]
        [StringLength(100)]
        public string SHA256 { get; set; }

        [Required]
        [StringLength(200)]
        public string SHA512 { get; set; }

        public string JWTCertificado { get; set; }

        public string JWTChavePublica { get; set; }

        //public virtual Obra Obra { get; set; }

        public virtual ICollection<Registro> Registros { get; set; }
    }
}
