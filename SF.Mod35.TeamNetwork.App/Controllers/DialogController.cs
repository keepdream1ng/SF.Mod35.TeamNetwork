using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;
using SF.Mod35.TeamNetwork.App.Views.Dialog;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class DialogController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUnitOfWork _unitOfWork;

	public DialogController(
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

	[Route("Chat")]
	[HttpGet]
	public async Task<IActionResult> Chat([FromQuery] string id)
	{
		DialogViewModel model = await GetDialogWith(id);
		return View("DialogView", model);
	}

	[Route("Chat")]
	[HttpPost]
	public async Task<IActionResult> NewMessage(string id, DialogViewModel model)
	{
		if (!string.IsNullOrEmpty(model.NewMessage))
		{
			User currentUser = await _userManager.GetUserAsync(User);
			User respondent = await _userManager.FindByIdAsync(id);
			Dialog dialog = _unitOfWork.DialogRepo.GetDialogByUsers(currentUser.Id, id);
			bool reverseMessageMapping = (currentUser.Id != dialog.User1Id);
			Message message = new Message(dialog, model.NewMessage, !reverseMessageMapping);
			_unitOfWork.MessagesRepo.Create(message);
			_unitOfWork.DialogRepo.Update(dialog);
			var result = await _unitOfWork.SaveChanges();

			model.MessageHistory = dialog.Messages;
			model.Respondent = respondent;
			model.NewMessage = string.Empty;
			model.ReverseMessageMapping = reverseMessageMapping;
		}
		else
		{
			model = await GetDialogWith(id);
		}

		return View("DialogView", model);
	}
	
	private async Task<DialogViewModel> GetDialogWith(string id)
	{
		User currentUser = await _userManager.GetUserAsync(User);
		User respondent = await _userManager.FindByIdAsync(id);

		Dialog dialog = _unitOfWork.DialogRepo.GetDialogByUsers(currentUser.Id, respondent.Id);
		// If chat is brand new.
		if (dialog == null)
		{
			dialog = new Dialog(currentUser, respondent);
			_unitOfWork.DialogRepo.Create(dialog);
			var result = await _unitOfWork.SaveChanges();
		}

		var model = new DialogViewModel()
		{
			Respondent = respondent,
			MessageHistory = dialog.Messages,
			ReverseMessageMapping = (currentUser.Id != dialog.User1Id)
		};
		return model;
	}
}
