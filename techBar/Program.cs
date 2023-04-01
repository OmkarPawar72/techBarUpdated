using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Cart;
using techBar.Data.Services;
using techBar.Models;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		//DbContext Configuration
		var connStr = builder.Configuration.GetConnectionString("techBar");
		builder.Services.AddDbContext<EcomDbContext>(options => options.UseSqlServer(connStr));

        //Services configuration

        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
        builder.Services.AddScoped<ISellersService, SellersService>();
		builder.Services.AddScoped<IProductsCategoryService,ProductsCategoryService>();
		builder.Services.AddScoped<IOrdersService, OrdersService>();


		//Configure 
		builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

		//Autentication and authorization
		builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EcomDbContext>();
		builder.Services.AddMemoryCache();
		builder.Services.AddSession();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme;
		});

		builder.Services.AddSession();

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
		app.UseSession();

		//Authentication & Authorization
		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAuthorization();

		app.UseAuthentication();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		//Seed datbase
		EcomDbInitializer.Seed(app);
		EcomDbInitializer.SeedUsersAndRolesAsync(app).Wait();

		app.Run();
	}
}