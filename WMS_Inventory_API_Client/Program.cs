using WMS_Inventory_API_Client.Services;
using WMS_Inventory_API_Client.Services.Interfaces;
namespace WMS_Inventory_API_Client
{
    public class Program
    {
        public static ServiceDescriptor? videoGame { get; private set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // \/ Add one of these for each Interface/Service \/
            builder.Services.AddHttpClient<IAccountService, AccountService>(c =>
            c.BaseAddress = new Uri("https://localhost:7256/"));


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
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}