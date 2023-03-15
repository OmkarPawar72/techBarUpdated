using Microsoft.EntityFrameworkCore;
using techBar.Data;
using techBar.Data.Services;

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

		app.UseAuthentication();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		//Seed datbase
		EcomDbInitializer.Seed(app);

		app.Run();
	}
}