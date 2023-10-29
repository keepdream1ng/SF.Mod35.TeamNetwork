using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SF.Mod35.TeamNetwork.App.Views.Shared;

public class LoginViewModel
{
	[Required]
	[EmailAddress]
	[Display(Name = "Email")]
	public string Email { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	public string Password { get; set; }

	[Display(Name = "Remember me")]
	public bool RememberMe { get; set; }

	public string ReturnUrl { get; set; }
}

