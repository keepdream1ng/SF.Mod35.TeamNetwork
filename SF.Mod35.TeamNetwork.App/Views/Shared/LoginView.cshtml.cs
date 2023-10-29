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
	[StringLength(100, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 5)]
	public string Password { get; set; }
}

