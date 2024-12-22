using Microsoft.EntityFrameworkCore;
using SalesCRMApp.Repo;
using Microsoft.AspNetCore.Identity;

namespace SalesCRMApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string DefaultConnectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]!;
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DefaultConnectionString));

            builder.Services
                .AddDefaultIdentity<IdentityUser>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}