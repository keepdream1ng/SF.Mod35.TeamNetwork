using Microsoft.AspNetCore.Mvc;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class ProfileController : Controller
{
	[Route("Profile")]
	[HttpGet]
	public IActionResult Profile()
	{
		return View("UserProfile");
	}
}
