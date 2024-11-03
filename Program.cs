using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InTheBag
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache(); // Add memory caching
            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true; // Ensure the session cookie is set
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 5); // Set idle timeout to 5 minutes
            });
            builder.Services.AddControllersWithViews(); // Add MVC services

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Default HSTS value
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession(); // Enable session state

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
