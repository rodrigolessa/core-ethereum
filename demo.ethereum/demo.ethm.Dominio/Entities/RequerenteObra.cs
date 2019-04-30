using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Dominio.Entities
{
    public class RequerenteObra : EntityBase
    {
        public int IdObra { get; set; }

        public int IdRequerente { get; set; }

        public int? IdRepresentante { get; set; }

        // TODO: Resolver conversão para boleano!
        public int? EhAutor { get; set; }

        public int? EhAdaptador { get; set; }

        public int? EhCessionario { get; set; }

        public int? EhTradutor { get; set; }

        public int? EhIlustrador { get; set; }

        public int? EhOrganizador { get; set; }

        public int? EhFotografo { get; set; }

        public int? EhRepresentante { get; set; }

        public int? EhCedente { get; set; }

        public int? EhHerdeiro { get; set; }

        public int? EhInventariante { get; set; }

        public int? EhEditor { get; set; }

        public virtual Obra Obra { get; set; }

        public virtual Requerente Requerente { get; set; }

        public virtual Representante Representante { get; set; }
    }
}