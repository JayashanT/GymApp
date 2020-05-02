using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gym.Dtos;
using gym.Entity;
using gym.Helpers;
using gym.Repositories;
using gym.Services;
using gym.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace gym
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<GymDbContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICommonRepository<User>, CommonRepository<User>>();
            services.AddScoped<ICommonRepository<Exercise>, CommonRepository<Exercise>>();
            services.AddScoped<ICommonRepository<Shedule>, CommonRepository<Shedule>>();
            services.AddScoped<ICommonRepository<Member>, CommonRepository<Member>>();
            services.AddScoped<ICommonRepository<Admin>, CommonRepository<Admin>>();
            services.AddScoped<ICommonRepository<Event>, CommonRepository<Event>>();
            services.AddScoped<ICommonRepository<Gym>, CommonRepository<Gym>>();
            services.AddScoped<ICommonRepository<Image>, CommonRepository<Image>>();

            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<ISheduleService, SheduleService>();

            // configure strongly typed settings objects
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<User, UserDto>().ReverseMap();
                mapper.CreateMap<Admin, AdminDto>().ReverseMap();
                mapper.CreateMap<Member, MemberDto>().ReverseMap();
                mapper.CreateMap<Gym, GymDto>().ReverseMap();
                mapper.CreateMap<Exercise, ExerciseDto>().ReverseMap();
                mapper.CreateMap<Shedule, SheduleDto>().ReverseMap();
                mapper.CreateMap<Event, EventDto>().ReverseMap();
                mapper.CreateMap<Image, ImageDto>().ReverseMap();


                mapper.CreateMap<Shedule, SheduleVM>().ReverseMap();
                mapper.CreateMap<Exercise, ExerciseVM>().ReverseMap();
                
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseDefaultFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseWebSockets();
            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseHttpsRedirection();
           // app.UseMvc();
        }
    }
}
