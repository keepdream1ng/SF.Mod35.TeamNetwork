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
			var user = _mapper.Map<User>(model);
			var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
				{
					return Redirect(model.ReturnUrl);
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ModelState.AddModelError("", "Incorrect login/passwod pair!");
			}
		}
		return View("/Home/Index");
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
