using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SwimmingPool.Data;
using SwimmingPool.Service;
using SwimmingPool.Service.Authentication;
using SwimmingPool.Service.Common;
using System;
using System.Text;

namespace SwimmingPool.Api
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
            services.AddCors(option => option.AddPolicy("local", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddDbContext<SwimmingPoolContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"), o => o.UseNodaTime())
                       .UseSnakeCaseNamingConvention();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SwimmingPoolSecurityKey", EnvironmentVariableTarget.Machine))),
                    ValidateIssuer = true,
                    ValidIssuer = "https://swimmingpool.com",
                    ValidateAudience = true,
                    ValidAudience = "https://swimmingpool.com",
                };
            });
            services.AddControllers();

            services.AddScoped(typeof(DbContext), typeof(SwimmingPoolContext));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddScoped(typeof(ISmtpService), typeof(SmtpService));
            services.AddScoped(typeof(IClockService), typeof(ClockService));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("local"))
            {
                app.UseCors("local");
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();
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