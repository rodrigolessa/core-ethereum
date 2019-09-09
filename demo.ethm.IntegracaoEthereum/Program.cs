using System;
using Microsoft.EntityFrameworkCore;
using demo.ethm.Dominio.Entities;
using demo.ethm.Infraestrutura.Persistencia;
using System.Linq;
using System.Configuration;

namespace demo.ethm.IntegracaoEthereum
{
    class Program
    {
        static void Main(string[] args)
        {
            //string conString = Microsoft
            //   .Extensions
            //   .Configuration
            //   .ConfigurationExtensions
            //   .GetConnectionString(Configuration, "DefaultConnection");

            //while (true)
            //{
            //    var db = new MainContext();
            //    var transac = new Transacoes();

            //    foreach (Registro r in db.Registro.Where(x => x.Status == 1).ToList())
            //    {
            //        // TODO: Parâmetros corretos
            //        var hash = transac.CriarTransacoesAsync(r.Arquivo.SHA256, r.Arquivo.SHA256);

            //        var tp = hash.Result;

            //        if (string.IsNullOrWhiteSpace(tp.Item1))
            //            r.Status = 3;
            //        else
            //        {
            //            r.HashTransacao = tp.Item1;
            //            r.DataDeInclusao = tp.Item2;
            //            r.Status = 2;
            //        }

            //        db.Entry(r).State = EntityState.Modified;
            //    }

            //    db.SaveChanges();
            //}
        }
    }
}
