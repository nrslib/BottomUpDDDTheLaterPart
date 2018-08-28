using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Application;
using Domain.Domain.Users;
using Domain.UnitOfWorkSample;
using InMemoryInfrastructure.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using ProductInfrastructure.DbContexts;
using ProductInfrastructure.UnitOfWorkSample;
using ProductInfrastructure.Users;
using UserRepository = ProductInfrastructure.Users.UserRepository;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            SetupByNormal(services);

            // UnitOfWork で動かしたい場合は↑をコメントにして、↓をコメントアウト
            // SetupByUnitOfWork(services);
            // UserController で UserApplicationServiceByUow を利用するようにしてください
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void SetupByNormal(IServiceCollection services) {
            RegisterInMemory(services);
            // ↓SQL を利用して動かしたい場合
//            RegisterSql(services);
            // ↓EntityFramework を利用して動かしたい場合
//            RegisterEntityFramework(services);

            services.AddTransient<UserApplicationService>();
        }

        private void RegisterInMemory(IServiceCollection services) {
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        }

        private void RegisterSql(IServiceCollection services) {
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private void RegisterEntityFramework(IServiceCollection services) {
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddTransient(provider => {
                var context = new BottomUpDddDbContext();
                context.Database.AutoTransactionsEnabled = false; // 自動トランザクションを無効にして Nested transaction にならないようにする
                return context;
            });
            services.AddDbContext<BottomUpDddDbContext>();
            services.AddTransient<IUserRepository, EFUserRepository>();
        }

        private void SetupByUnitOfWork(IServiceCollection services) {
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddTransient<IUserRepository, ProductInfrastructure.UnitOfWorkSample.UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<UserApplicationServiceByUow>();
        }
    }
}
