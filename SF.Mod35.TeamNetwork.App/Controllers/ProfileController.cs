using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	public async Task<IActionResult> MyPage()
	{
		if (_signInManager.IsSignedIn(User))
		{
			var userModel = await _userManager.GetUserAsync(User);
			return View("UserProfile", new UserProfileModel(userModel));
		}
		else
		{
			return View("LoginView");
		}
	}

	[Authorize]
	[Route("Edit")]
	[HttpGet]
	public async Task<IActionResult> Edit()
	{
		var userModel = await _userManager.GetUserAsync(User);
		var userViewModel = _mapper.Map<UserEditViewModel>(userModel);
		return View("UserEditView", userViewModel);
	}

	[Authorize]
	[Route("Edit")]
	[HttpPost]
	public async Task<IActionResult> Edit(UserEditViewModel model)
	{
		if (ModelState.IsValid)
		{
			var user = await _userManager.GetUserAsync(User);
			_mapper.Map(model, user);
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return RedirectToAction("MyPage", "Profile");
			}
			else
			{
				return RedirectToAction("Edit", "Profile");
			}
		}
		else
		{
			ModelState.AddModelError("", "Incorrect data!");
			return View("UserEditView", model);
		}
	}
}
