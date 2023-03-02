using BeribitStatistics.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeribitStatistics.Repositories;
using BeribitStatistics.Services;
using BeribitStatistics.Tables.Users;
using Quartz;
using BeribitStatistics.Extensions;
using BeribitStatistics.Hubs;
using BeribitStatistics.Jobs;

namespace BeribitStatistics
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<CustomUser>(options => options.User.AllowedUserNameCharacters = string.Empty)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<StatisticRepository>();
            services.AddTransient<StatisticService>();
            services.AddSingleton<StatisticCashService>();

            services.AddRazorPages();

            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"Недопустимое значение: {x}");
                    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => $"Недопустимое значение: {x}");
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => $"Недопустимое значение: {x}");
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => $"Недопустимое значение: {x}");
                });

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
                q.AddJobAndTrigger<SaveHistoryJob>(Configuration);
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddSignalR();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<StatisticsHub>("/hubs/stat");
            });
        }
    }
}
