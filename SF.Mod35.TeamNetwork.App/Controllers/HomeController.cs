using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using System.Diagnostics;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly SignInManager<User> _signInManager;

	public HomeController(ILogger<HomeController> logger,
		SignInManager<User> signInManager)
	{
		_logger = logger;
		_signInManager = signInManager;
	}

	[Route("")]
	[Route("[controller]/[action]")]
	public IActionResult Index()
	{
		if (_signInManager.IsSignedIn(User))
		{
			return RedirectToAction("MyPage", "Profile");
		}
		else
		{
			return View();
		}
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View();
	}
}
