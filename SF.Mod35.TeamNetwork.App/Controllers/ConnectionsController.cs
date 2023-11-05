using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class ConnectionsController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUnitOfWork _unitOfWork;

	public ConnectionsController(
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

	[Route("SuggestConnection")]
	[HttpPost]
	public async Task<IActionResult> SuggestConnection(string id)
	{
		User currentUser = await _userManager.GetUserAsync(User);
		User target = await _userManager.FindByIdAsync(id);

		var connectionFromTarget = _unitOfWork.ConnectionsRepo.GetConnectionStatus(target, currentUser);
		if ((connectionFromTarget != null) && (connectionFromTarget == ConnectionStatus.Following))
		{
			_unitOfWork.ConnectionsRepo.SetConnectionStatus(target, currentUser, ConnectionStatus.Mutual);
			_unitOfWork.ConnectionsRepo.CreateConnection(currentUser, target, ConnectionStatus.Mutual);
		}
		else
		{
			_unitOfWork.ConnectionsRepo.CreateConnection(currentUser, target, ConnectionStatus.Following);
			_unitOfWork.ConnectionsRepo.CreateConnection(target, currentUser, ConnectionStatus.Pending);
		}
		var result = await _unitOfWork.SaveChanges();
		return RedirectToAction("MyPage", "Profile");
	}

	[Route("DeclineConnection")]
	[HttpPost]
	public async Task<IActionResult> DeclineConnection(string id)
	{
		User currentUser = await _userManager.GetUserAsync(User);
		User target = await _userManager.FindByIdAsync(id);

		_unitOfWork.ConnectionsRepo.SetConnectionStatus(currentUser, target, ConnectionStatus.Declined);
		// Check to set from "Mutual" to "Following" for the target user.
		var targetConnectionStatus = _unitOfWork.ConnectionsRepo.GetConnectionStatus(target, currentUser);
		if (targetConnectionStatus == ConnectionStatus.Mutual)
		{
			_unitOfWork.ConnectionsRepo.SetConnectionStatus(target, currentUser, ConnectionStatus.Following);
		}
		await _unitOfWork.SaveChanges();
		return RedirectToAction("MyPage", "Profile");
	}

	[Route("ApproveConnection")]
	[HttpPost]
	public async Task<IActionResult> ApproveConnection(string id)
	{
		User currentUser = await _userManager.GetUserAsync(User);
		User target = await _userManager.FindByIdAsync(id);

		var targetConnectionStatus = _unitOfWork.ConnectionsRepo.GetConnectionStatus(target, currentUser);
		var newStatus = targetConnectionStatus == ConnectionStatus.Following ? ConnectionStatus.Mutual : ConnectionStatus.Following;

		_unitOfWork.ConnectionsRepo.SetConnectionStatus(currentUser, target, newStatus);
		// Check to set from "Following" to "Mutual" for the target user.
		if (targetConnectionStatus == ConnectionStatus.Following)
		{
			_unitOfWork.ConnectionsRepo.SetConnectionStatus(target, currentUser, ConnectionStatus.Mutual);
		}
		await _unitOfWork.SaveChanges();
		return RedirectToAction("MyPage", "Profile");
	}
}
