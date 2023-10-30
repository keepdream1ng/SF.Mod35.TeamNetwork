using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Views.Profile;

public class UserProfileModel
{
	public User UserData { get; set; }
	public UserProfileModel(User user)
	{
		UserData = user;
	}
}
