using WebAppNet.DBContext;
using WebAppNet.Interface;
using WebAppNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace WebAppNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure middleware
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add DbContext with connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
            builder.Services.AddControllersWithViews(); 
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IHobyRepo, HobbyRepo>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Ensure session middleware is called before your custom middleware
            app.UseSession();

            // Now your custom middleware can access the session
            app.UseMiddleware<WebAppNet.Middleware.SessionCheckMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
