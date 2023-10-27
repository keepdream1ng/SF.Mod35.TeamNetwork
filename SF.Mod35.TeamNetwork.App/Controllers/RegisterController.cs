using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.Views.Register;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class RegisterController : Controller
{
	[Route("RegisterStart")]
	[HttpGet]
	public IActionResult Register()
	{
		return View("Home/Register", new RegisterViewModel());
	}

	[Route("RegisterEnd")]
	[HttpGet]
	public IActionResult RegisterEnd(RegisterViewModel model)
	{
		return View("RegisterEnd", model);
	}
}
