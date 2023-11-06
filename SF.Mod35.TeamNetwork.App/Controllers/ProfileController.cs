using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class ProfileController : Controller
{

	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUnitOfWork _unitOfWork;

	public ProfileController(
		UserManager<User> userManager,
		SignInManager<User> signInManager,
		IUnitOfWork unitOfWork,
		IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	[Route("MyPage")]
	[HttpGet]
	public async Task<IActionResult> MyPage()
	{
		if (_signInManager.IsSignedIn(User))
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var profileModel = new UserProfileModel()
			{
				UserData = currentUser,
				ConnectedUsers = _unitOfWork.ConnectionsRepo.GetConnectedWithUser(currentUser, ConnectionStatus.Mutual),
				Following = _unitOfWork.ConnectionsRepo.GetConnectedWithUser(currentUser, ConnectionStatus.Following),
				PendingConnection = _unitOfWork.ConnectionsRepo.GetConnectedWithUser(currentUser, ConnectionStatus.Pending),
			};
			return View("UserProfile", profileModel);
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
