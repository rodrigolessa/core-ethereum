using demo.ethm.Aplicacao.Models;
using demo.ethm.Dominio.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace demo.ethm.Aplicacao
{
    public class MapperConfiguration
    {
        public MapperConfiguration()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Idioma, IdiomaDTO>();
                cfg.CreateMap<Usuario, UsuarioDTO>();
            });
        }
    }
}
