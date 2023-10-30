using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class ProfileController : Controller
{

	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;
	}

	[Route("MyPage")]
	[HttpGet]
	public IActionResult MyPage()
	{
		if (_signInManager.IsSignedIn(User))
		{
			var result = _userManager.GetUserAsync(User);
			return View("UserProfile", new UserProfileModel(result.Result));
		}
		else
		{
			return View("LoginView");
		}
	}
}
