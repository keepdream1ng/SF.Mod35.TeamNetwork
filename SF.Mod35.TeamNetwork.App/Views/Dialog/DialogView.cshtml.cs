using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Views.Dialog;

public class DialogViewModel
{
	public User Viewer { get; set; }
	public User Respondent { get; set; }

	public List<Message> MessageHistory { get; set; }

	public string NewMessage { get; set; } = string.Empty;

	/// <summary>
	/// If in dialog entity Viewer is User2.
	/// </summary>
	public bool ReverseMessageMapping { get; set; }
}
