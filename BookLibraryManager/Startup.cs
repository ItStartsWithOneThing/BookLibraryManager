using BookLibraryManagerBL;
using BookLibraryManagerBL.AutoMapper.Profiles;
using BookLibraryManagerBL.BooksService.Services;
using BookLibraryManagerBL.Services.LibrariesService;
using BookLibraryManagerDAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BookLibraryManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<EFCoreContext>(options =>
                        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<UserService>();
            services.AddScoped<ILibrariesService, LibrariesService>();

            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(BookProfile))
            };

            services.AddAutoMapper(assemblies);

            services.AddScoped(typeof(IDbGenericRepository<>), typeof(DbGenericRepository<>));
            services.AddScoped<IDbBooksRepository, DbBooksRepository>();
            services.AddScoped<IDbLibraryRepository, DbLibraryRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookLibraryManager", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookLibraryManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
