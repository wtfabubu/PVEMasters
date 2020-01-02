using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PVEMasters.Models;
using PVEMasters.Repositories.AccountRepository;
using PVEMasters.Repositories.EquipmentRepository;
using PVEMasters.Repositories.MissionsRepository;
using PVEMasters.Services.AccountService;
using PVEMasters.Services.ChampionsRepository;
using PVEMasters.Services.ChampionsService;
using PVEMasters.Services.EquipmentService;
using PVEMasters.Services.MissionsService;
using System;
using System.Net;
using System.Text;

namespace PVEMasters
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public class ExceptionFilter : IExceptionFilter
        {
            public void OnException(ExceptionContext context)
            {
                context.Result = new JsonResult(context.Exception);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(@"Server=.;Database=PVPMasters;Trusted_Connection=True;MultipleActiveResultSets=true"));
            //services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("user"));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
            services.AddScoped<IChampionsRepository, ChampionsRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IMissionsRepository, MissionsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IChampionsService, ChampionsService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IMissionsService, MissionsService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddHttpContextAccessor();

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret phrase"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = signingKey,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddMvc(a => a.Filters.Add<ExceptionFilter>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<UserDbContext>();
            //    context.Database.Migrate();
            //}

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("Cors");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
