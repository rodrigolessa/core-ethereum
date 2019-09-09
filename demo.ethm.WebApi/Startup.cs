using demo.ethm.Infraestrutura.Persistencia;
using demo.ethm.Infraestrutura.Mailing;
using demo.ethm.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace demo.ethm.WebApi
{
    // TODO: User referências do projeto https://github.com/EduardoPires/EquinoxProject

    /// <summary>
    /// Configurações iniciais do projeto
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Acesso as configurações da aplicação
        /// </summary>
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _env;
        private readonly ILogger _logger;

        /// <summary>
        /// Construtor do projeto, onde é injetado, Configuração, Ambiente e Log
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: Boas práticas: Obtendo o usuário logado em qualquer camada
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add the whole configuration object here.
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddDbContext<MainContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            Microsoft.Extensions.DependencyInjection.OptionsConfigurationServiceCollectionExtensions
                .Configure<Infraestrutura.Helpers.SmtpConfig>(services, Configuration.GetSection("Smtp"));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, AuthMessageSender>();

            // TODO: Boas práticas: Criar um método por extensão para tratar todas as injeções de dependências.

            // Add application services
            // Add domain services
            // Add repositories
            // TODO: Add our repository type
            //services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            //_logger.LogInformation("Added Entity Repository to services");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "demo.ethm",
                        Version = "v1",
                        Description = "API do sistema",
                        Contact = new Contact
                        {
                            Name = "demo.ethm Team",
                            Url = "https://www.demo.ethm.com/"
                        }
                    });

                var caminhoAplicacao = System.AppContext.BaseDirectory; // or System.AppDomain.CurrentDomain.BaseDirectory
                var nomeAplicacao = System.Reflection.Assembly.GetEntryAssembly().GetName().Name; // or System.AppDomain.CurrentDomain.SetupInformation.ApplicationName
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
                //ApplicationEnvironment.ApplicationVersion // System.Reflection.Assembly.GetEntryAssembly().GetName().Version
                //ApplicationEnvironment.RuntimeFramework // System.Reflection.Assembly.GetEntryAssembly().GetCustomAttribute<TargetFrameworkAttribute>().FrameworkName or System.AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory
            //.AddConsole()
            //.AddDebug();

            if (env.IsDevelopment())
            {
                _logger.LogInformation("In Development environment");

                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAPIErrorHandler();
            //app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "demo.ethm");
            });
        }
    }
}
