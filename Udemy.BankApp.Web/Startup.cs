using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mappings;

namespace Udemy.BankApp.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BankContext>(opt =>
            {
                opt.UseSqlServer("server=TAHA\\SQLEXPRESS; database=BankDb; integrated security=true;"); //bu BankContext constructor options'a gidecek. Oradan da base(options)'a giderek onconfiguring metodunu aya�a kald�racak.
            });
            services.AddControllersWithViews();

            //repositoryde de dependency injection yapmam�z i�in
            //services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

            //repositoryde entity d�n�yor bunu model d�nmesi i�in �evirme class'� olu�turduk onu dependency injection ile kullanal�m
            services.AddScoped<IUserMapper , UserMapper>(); 

            //services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountMapper , AccountMapper>();

            //generic repository i�in
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //uow i�in olu�tural�m.
            services.AddScoped<IUow, Uow>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //node_modules klas�r�n� eri�ime a�mak i�in yazd���m�z kod
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
                RequestPath = "/node_modules"
            });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
