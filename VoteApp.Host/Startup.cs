using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VoteApp.Database;
using VoteApp.Host.ExceptionFilter;
using VoteApp.Host.Service;
using VoteApp.Host.Service.Candidate;
using VoteApp.Host.Service.Document;
using VoteApp.Host.Service.User;
using VoteApp.Host.Utils;
using VoteApp.Host.Utils.Document;
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
                        .WithOrigins(
                            "http://localhost:4200",
                            "http://localhost:5000", 
                            "http://192.168.10.250:5010",
                            "https://vote.my:5010")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });
            
            SwaggerStartup.ConfigureServices(services);
            
            RegisterDatabaseServices(services);
            RegisterAppServices(services);
            RegisterUtils(services);
            
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
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(24);
                    
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddScoped<IDatabaseContainer, DatabaseContainer>();
            
            var typeOfContent = typeof(Startup);

            services.AddDbContext<PostgresContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("PostgresConnection"),
                    b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
                )
            );
        }

        private void RegisterAppServices(IServiceCollection services)
        {
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICandidateService, CandidateService>();
        }

        private void RegisterUtils(IServiceCollection services)
        {
            services.AddScoped<IUtilsFactory, UtilsFactory>();
            services.AddScoped<IUserUtils, UserUtils>();
            services.AddScoped<IDocumentUtils, DocumentUtils>();
        }
        
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SwaggerStartup.Configure(app, env);
            
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