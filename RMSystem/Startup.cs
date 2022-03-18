using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RMSystem.Services;

namespace RMSystem
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
            // Внедряем базу данных, создает пул контестов.
            services.AddDbContextPool<RMSystemContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("RiskDBConnect"));
            });
            services.AddRazorPages();

            // Используя паттерн Singleton внедряем зависимость
            // с базой данных Репозитория <интерфейс, класс DB>.
            // services.AddSingleton<IRiskRepository, MockRiskRepository>();

            // Подключение к реальной базе данных
            // используя паттерн IRepository.
            // с базой данных Репозитория <интерфейс, класс DB>.
            services.AddScoped<IRiskRepository, SQLRiskRepository>();

            // Конфигурируем адрессную строку.
            // LowercaseUrls - слова маленькими буквами.
            // LowercaseQueryStrings - параметры маленькими буквами.
            // AppendTrailingSlash - разделение слешами.
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
