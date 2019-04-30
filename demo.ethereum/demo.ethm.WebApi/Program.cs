using demo.ethm.Dominio.Entities;
using demo.ethm.Dominio.Entities.Enums;
using demo.ethm.Dominio.Entities.ValueObjects;
using demo.ethm.Infraestrutura.Persistencia;
using demo.ethm.WebApi.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.ethm.WebApi
{
    /// <summary>
    /// Bootstrapping for migrating database
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //var config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: false)
            //.Build();

            //var host = new WebHostBuilder()
            //.UseKestrel()
            //.UseConfiguration(config)
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.UseIISIntegration()
            //.UseStartup<Startup>()
            //.Build();

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.
                        GetRequiredService<MainContext>();

                    // TODO: Verificar diferenças ao criar migrações com o .Net CLI.
                    context.Database.Migrate();

                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ocorreu um erro ao popular o banco de dados.");
                }
            }

            host.Run();
        }

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
