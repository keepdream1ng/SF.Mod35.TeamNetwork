using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.Views.Register;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class RegisterController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public RegisterController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_mapper = mapper;
		_userManager = userManager;
		_signInManager = signInManager;
	}

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

	[Route("RegisterEnd")]
	[HttpPost]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			var user = _mapper.Map<User>(model);
			var result = await _userManager.CreateAsync(user, model.PasswordReg);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
		}
		return View("RegisterEnd", model);
	}

}
