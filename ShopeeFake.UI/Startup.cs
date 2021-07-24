using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopeeFake.Domain;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Application.Command.CategoryCommands;
using ShopeeFake.UI.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using ShopeeFake.UI.Model;
using ShopeeFake.UI.Infrastructure.Filters;

namespace ShopeeFake.UI
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

            services.Configure<Audience>(Configuration.GetSection("Audience"));
            services.AddControllers(options=>
            {
                //khi có ngoại lệ xảy ra thì bắt ngay, không cần try catch ở từng nơi
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopeeFake.UI", Version = "v1" });
                /*setup bearer token*/
                c.AddSecurityDefinition("Bearer",
                   new OpenApiSecurityScheme
                   {
                       In = ParameterLocation.Header,
                       Description = "Please enter into field the word" +
                       " 'Bearer' following by space and JWT",
                       Name = "Authorization",
                       Type = SecuritySchemeType.ApiKey,
                       Scheme = "MyAuthKey"
                   });
            });
            services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"]);
            }, ServiceLifetime.Scoped);
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddScoped<IIdentityServices, IdentityServices>();

            //Configure conectionString
            services.AddTransient<IStoreQueries>(x => new StoreQueries(Configuration["ConnectionString"]));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(AddCategoryCommand).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopeeFake.UI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
