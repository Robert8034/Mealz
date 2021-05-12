using System.Text;
using Authentication.DAL;
using Authentication.MessageHandlers;
using Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Authentication
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
            services.AddControllers();

            var key = "thisismytestprivatekey";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    
                };
            }
            );
            
            services.AddAuthorization();

            var connection = "Server=authdb;Database=master;User=sa;Password=Your_password123;";

            services.AddDbContext<UserContext>(
                 options => options.UseSqlServer(connection));

            services.AddSingleton<IJwtAuthenticationManager, JwtAuthenticationManager>();

            services.AddSingleton<ICryptographyService, CrypthographyService>();

            services.AddScoped<IUserDAL, UserDAL>();

            services.AddScoped<IUserService, UserService>();

            services.AddSharedServices("Authentication Service");

            services.AddMessagePublishing("Authentication Service", builder => {
                builder.WithHandler<UserRegisteredMessageHandler>("UserRegistered");
                builder.WithHandler<UserChangedMessageHandler>("UserChanged");
                builder.WithHandler<UserDeletedMessageHandler>("UserDeleted");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSharedAppParts("Authentication Service");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                        .GetRequiredService<IServiceScopeFactory>()
                        .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<UserContext>();
            context.Database.Migrate();
        }
    }
}
