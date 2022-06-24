using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.ApplicationRepository;
using IJobs.Repositories.CompanyRepository;
using IJobs.Repositories.DomainRepository;
using IJobs.Repositories.InterviewRepository;
using IJobs.Repositories.InviteRepository;
using IJobs.Repositories.JobRepository;
using IJobs.Repositories.SubdomainRepository;
using IJobs.Repositories.TutorialRepository;
using IJobs.Repositories.UserRepository;
using IJobs.Services;
using IJobs.Services.ApplicationService;
using IJobs.Services.DomainService;
using IJobs.Services.InterviewService;
using IJobs.Services.InviteService;
using IJobs.Services.SubdomainService;
using IJobs.Services.TutorialService;
using IJobs.Utilities;
using IJobs.Utilities.JWTUtils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

namespace IJobs
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
            services.AddDbContext<projectContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();


            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.AddAuthorization(config =>
            {
                config.AddPolicy("0",
                    options => options.RequireClaim(ClaimTypes.Role));
                config.AddPolicy("1",
                    options => options.RequireClaim(ClaimTypes.Role)); 
                config.AddPolicy("2",
                     options => options.RequireClaim(ClaimTypes.Role));
            });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(AppSettings));
            services.Configure<AppSettings>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(AppSettings.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(AppSettings.Audience)];
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtSecret"])), SecurityAlgorithms.HmacSha256);
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(AppSettings.Issuer)],
                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(AppSettings.Audience)],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtSecret"]))
                };
                options.ClaimsIssuer = jwtAppSettingOptions[nameof(AppSettings.Issuer)];
                options.SaveToken = true;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(c => c.AddPolicy("AllowOrigin", options => {
                    options.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                }));

            services.AddMvc();

            services.AddRazorPages();
            services.AddAutoMapper(typeof(Startup));


            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IDomainRepository, DomainRepository>();
            services.AddScoped<ISubdomainRepository, SubdomainRepository>();
            services.AddScoped<ITutorialRepository, TutorialRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IInterviewRepository, InterviewRepository>();
            services.AddScoped<IInviteRepository, InviteRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<ISubdomainService, SubdomainService>();
            services.AddScoped<ITutorialService, TutorialService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IInviteService, InviteService>();

            services.AddScoped<IJWTUtils<User>, JWTUtils<User>>();
            services.AddScoped<IJWTUtils<Company>, JWTUtils<Company>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JWTMiddleware<User>>();

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}
