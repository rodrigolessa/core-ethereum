using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using demo.ethm.Dominio.Entities.Enums;

namespace demo.ethm.Dominio.Entities
{
    public class Obra : EntityBase
    {
        public Obra()
        {
            //Arquivo = new HashSet<Arquivo>();
            OriginalVinculos = new HashSet<ObraVinculo>();
            Registros = new HashSet<Registro>();
            //RequerenteObra = new HashSet<RequerenteObra>();
        }

        public int IdUsuario { get; set; }

        public int IdGeneroObra { get; set; }

        public int IdRequerente { get; set; }

        public int? IdRepresentante { get; set; }

        // TODO: Resolver conversão para boleano!
        public int? EhRequerimento { get; set; }

        public int? EhAverbacao { get; set; }

        public int? EhInedita { get; set; }

        public int? EhPublicada { get; set; }


        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [StringLength(100)]
        public string Grafica { get; set; }

        [StringLength(100)]
        public string Editora { get; set; }

        [StringLength(4)]
        public string Ano { get; set; }

        [StringLength(20)]
        public string Volume { get; set; }

        [StringLength(20)]
        public string Edicao { get; set; }

        [StringLength(100)]
        public string LocalPublicacao { get; set; }
        
        public int? Paginas { get; set; }

        [StringLength(200)]
        public string AdaptacaoTituloOriginal { get; set; }

        [StringLength(200)]
        public string AdaptacaoAutorOriginal { get; set; }

        [StringLength(200)]
        public string TraducaoTituloOriginal { get; set; }

        [StringLength(200)]
        public string TraducaoAutorOriginal { get; set; }

        public string Observacoes { get; set; }

        public SituacaoDaObra? Situacao { get; private set; }

        public DateTime? DataDaExclusao { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual GeneroObra GeneroObra { get; set; }

        public virtual Requerente Requerente { get; set; }

        public virtual Representante Representante { get; set; }

        //public virtual ObraVinculo Vinculo { get; set; }

        public virtual ICollection<ObraVinculo> OriginalVinculos { get; set; }

        //public virtual ICollection<RequerenteObra> RequerenteObra { get; set; }

        //public virtual ICollection<Arquivo> Arquivo { get; set; }

        public virtual ICollection<Registro> Registros { get; set; }
    }
}
