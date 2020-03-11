using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CarPool.Helpers;
using CarPool.Services.Providers;
using CarPool.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CarPool.Automapper;

namespace CodeFirst
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            //Adding AutoMapper
            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            services.AddDbContext<Context>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));          
            services.AddScoped<IUserService,UserService>( );
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IBookingService, BookingService>();


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            
            //app.Map("/map1", HandleMapTest1);

            //app.Map("/map2", HandleMapTest2);

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from non-Map delegate. <p>");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
        //private static void HandleMapTest1(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Map Test 1");
        //    });
        //}

        //private static void HandleMapTest2(IApplicationBuilder app)
        //{
        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync("Map Test 2");
        //    });
        //}
    }
}
