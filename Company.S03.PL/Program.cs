using Company.S03.BLL.Interface;
using Company.S03.BLL.Repositories;
using Company.S03.DAL.Data.Contexts;
using Company.S03.DAL.Models;
using Company.S03.PL.Mapping;
using Company.S03.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.S03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
    
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
           //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
           //builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<CompanyDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.AddDbContext<CompanyDbContext>(options => 
            { 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IScopedServices, ScopedServices>();//Par Request
            builder.Services.AddTransient<ITransientServices, TransientServices>(); //Par Operation
            builder.Services.AddSingleton<ISingletonServices, SingletonServices>();//Par App

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                config.LogoutPath = "/Account/SignOut";
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
