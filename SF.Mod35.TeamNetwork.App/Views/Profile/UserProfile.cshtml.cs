using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Views.Profile;

public class UserProfileModel
{
	public User UserData { get; set; }
	public List<User> ConnectedUsers { get; set; }
	public List<User> Following { get; set; }
	public List<User> PendingConnection { get; set; }
}
