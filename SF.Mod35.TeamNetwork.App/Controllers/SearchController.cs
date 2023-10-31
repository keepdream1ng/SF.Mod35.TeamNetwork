using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using SF.Mod35.TeamNetwork.App.Views.Search;

namespace SF.Mod35.TeamNetwork.App.Controllers;

public class SearchController : Controller
{
	private IMapper _mapper;
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public SearchController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;
	}

	[Route("UserList")]
	[HttpPost]
	public IActionResult UserList(string search)
	{
		var model = new SearchViewModel
		{
			UserList = _userManager.Users
				.AsEnumerable()
				.Where(x => x.FirstName.ToLower().Contains(search.ToLower()))
				.ToList()
		};
		return View("SearchView", model);
	}
}
