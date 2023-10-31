﻿using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using SF.Mod35.TeamNetwork.App.Views.Search;
using SF.Mod35.TeamNetwork.App.DataAccess.Repository;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class SearchController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IUnitOfWork _unitOfWork;

	public SearchController(
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

	[Route("UserList")]
	[HttpPost]
	public async Task<IActionResult> UserList(string search)
	{
		SearchViewModel model = await CreateSearch(search);
		return View("SearchView", model);
	}

	private async Task<SearchViewModel> CreateSearch(string search)
	{
		var currentUser = await _userManager.GetUserAsync(User);
		List<User> userSearch = _userManager.Users
				.AsEnumerable()
				.Where(x => x.FirstName.ToLower().Contains(search.ToLower()))
				.ToList();
		var model = new SearchViewModel();
		// If current user is not logged in - he wont get an option to add connections.
		if(currentUser == null)
		{
			model.UserList = userSearch.Select(u =>
				new Tuple<User, ConnectionStatus>(u, ConnectionStatus.Unknown)).ToList();
		}
		else
		{
			model.UserList = userSearch.Select(u =>
				new Tuple<User, ConnectionStatus>(u, _unitOfWork.ConnectionsRepo.GetConnectionStatus(currentUser, u))).ToList();
		}
		return model;
	}
}

