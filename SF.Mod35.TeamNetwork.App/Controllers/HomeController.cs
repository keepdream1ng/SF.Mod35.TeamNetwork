using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	[Route("")]
	[Route("[controller]/[action]")]
	public IActionResult Index()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View();
	}
}
