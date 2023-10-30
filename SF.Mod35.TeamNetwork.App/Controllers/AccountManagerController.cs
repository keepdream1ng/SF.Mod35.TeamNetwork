using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.Views.Shared;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class AccountManagerController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;
	}

	[Route("Login")]
	[HttpGet]
	public IActionResult Login()
	{
		return View("Home/Login");
	}

	[Route("Login")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
			if (result.Succeeded)
			{
				return RedirectToAction("MyPage", "Profile");
			}
			else
			{
				ModelState.AddModelError(String.Empty, "Incorrect login/passwod pair!");
			}
		}
		return View("LoginView", model);
	}

	[Route("Logout")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return RedirectToAction("Index", "Home");
	}
}
