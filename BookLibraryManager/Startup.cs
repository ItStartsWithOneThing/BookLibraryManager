using BookLibraryManagerBL;
using BookLibraryManagerBL.Auth;
using BookLibraryManagerBL.AutoMapper.Profiles;
using BookLibraryManagerBL.BooksService.Services;
using BookLibraryManagerBL.Options;
using BookLibraryManagerBL.Services.AuthService;
using BookLibraryManagerBL.Services.EncryptionService;
using BookLibraryManagerBL.Services.HashService;
using BookLibraryManagerBL.Services.LibrariesService;
using BookLibraryManagerBL.Services.SMTPService;
using BookLibraryManagerDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BookLibraryManager.Middlewares;

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
            services.AddHttpContextAccessor(); // Serilog


            services.Configure<AuthOptions>(options =>
                Configuration.GetSection(nameof(AuthOptions)).Bind(options));

            services.Configure<SmtpConfiguration>(options =>
                Configuration.GetSection(nameof(SmtpConfiguration)).Bind(options));

            services.Configure<EncryptionConfiguration>(options =>
                Configuration.GetSection(nameof(EncryptionConfiguration)).Bind(options));

            var authOptions = Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                            ValidateIssuerSigningKey = true
                        };
                    });

            services.AddControllers();

            services.AddDbContext<EFCoreContext>(options =>
                        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<UserService>();
            services.AddScoped<ILibrariesService, LibrariesService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ISmtpService, SmtpService>();
            services.AddScoped<IEncryptionService, EncryptionService>();

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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                }); 

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
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

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
