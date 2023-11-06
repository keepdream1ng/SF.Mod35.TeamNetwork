using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SF.Mod35.TeamNetwork.App.DataAccess;
using SF.Mod35.TeamNetwork.App.DataAccess.Repository;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureServices();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error");
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

		app.MapRazorPages();

		app.Run();
	}
	private static void ConfigureServices(this WebApplicationBuilder builder)
	{
		string connection = builder.Configuration.GetConnectionString("DefaultConnection");
		// I know its lame, but its just a study project.
		if (builder.Configuration["DB_LOCATION"] == "Docker")
		{
			connection = builder.Configuration.GetConnectionString("DockerContainer");
		}
		// Add services to the container.
		var services = builder.Services;
		services.AddRazorPages();
		services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequiredLength = 5;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>();
		services.AddControllersWithViews();
		services.AddSingleton(GetConfiguredMapper());
	}

	private static IMapper GetConfiguredMapper()
	{
		var mapperConfig = new MapperConfiguration(v =>
		{
			v.AddProfile(new MappingProfile());
		});
		return mapperConfig.CreateMapper();
	}
}