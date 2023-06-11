using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VoteApp.Database;
using VoteApp.Host.ExceptionFilter;
using VoteApp.Host.Service;
using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;
using VoteApp.Host.Utils;
using VoteApp.Host.Utils.DocumentUtils;
using VoteApp.Host.Utils.User;

namespace VoteApp.Host
{
    public class Startup
    {
     
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200","http://localhost:5000", "http://192.168.10.250:5010", "http://vote.my:5010")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VoteApp Api", Version = "v1" });

                c.AddSecurityDefinition("Cookies", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Name = "UserCookies"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Cookies"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            
            var typeOfContent = typeof(Startup);

            services.AddDbContext<PostgresContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("PostgresConnection"),
                    b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
                )
            );
            
            services.AddScoped<IDatabaseContainer, DatabaseContainer>();
            
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICandidateService, CandidateService>();
            
            services.AddScoped<IUtilsFactory, UtilsFactory>();
            services.AddScoped<IUserUtils, UserUtils>();
            services.AddScoped<IDocumentUtils, DocumentUtils>();
            
            services.AddControllers(options =>
            {
                options.Filters.Add(new CustomExceptionAttribute());
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "UserCookies";
                    options.Cookie.Path = "/";
                    options.Cookie.Domain = "localhost";
                    options.Cookie.HttpOnly = false;
                    options.ExpireTimeSpan = TimeSpan.FromHours(24);
                    
                });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VoteApp Api v1"));
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseCors("AllowOrigin");
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