using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Views.Search;

public class SearchViewModel
{
	public List<Tuple<User, ConnectionStatus>> UserList { get; set; }
}
