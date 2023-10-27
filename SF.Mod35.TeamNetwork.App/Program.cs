using Microsoft.EntityFrameworkCore;

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
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

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
		//services.AddDbContext<>(options => options.UseSqlServer(connection));
	}
}