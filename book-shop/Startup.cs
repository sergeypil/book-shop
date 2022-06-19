using AutoMapper;
using book_shop.Data;
using book_shop.Data.Entities;
using book_shop.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace book_shop
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration config, IHostingEnvironment environment)
        {
            _config = config;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }
            ));
        
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<BookContext>();

            services.AddAuthentication().AddCookie().AddJwtBearer(
              cfg =>
              cfg.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidIssuer = _config["Tokens:Issuer"],
                  ValidAudience = _config["Tokens:Audience"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
              }
            );
            services.AddDbContext<BookContext>(cfg => cfg.UseNpgsql(_config.GetConnectionString("BookConnectionString")));
            services.BuildServiceProvider().GetService<BookContext>().Database.Migrate();
            Mapper.Reset();
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(i => i.Id))
                .ReverseMap();
                cfg.CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(oi => oi.ProductIsbn, ex => ex.MapFrom(oi => oi.Product.ISBN))
                .ReverseMap();

                cfg.ValidateInlineMaps = false;
            });
            services.AddTransient<BookSeeder>();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddMvc(opt =>
            {
                if (_environment.IsProduction() && _config["DisableSSL"] != "true")
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            }).AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=App}/{action=Index}/{id?}");
                endpoints.MapFallbackToController("Index", "App");
            });

            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<BookSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }
    }
}