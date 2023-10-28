using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SF.Mod35.TeamNetwork.App.Views.Register;

public class RegisterViewModel
{
	[Required]
	[Display(Name = "FirstName")]
	public string FirstName { get; set; }

	[Required]
	[Display(Name = "LastName")]
	public string LastName { get; set; }

	[Required]
	[Display(Name = "Email")]
	public string EmailReg { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]	
	[Display(Name = "Date of birth")]
	public DateOnly DateOfBirth { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[Display(Name = "Password")]
	[StringLength(100, ErrorMessage = "Field {0} should have minimum {2} and max {1} characters.", MinimumLength = 5)]
	public string PasswordReg { get; set; }

	[Required]
	[Compare("PasswordReg", ErrorMessage = "Passwords are not the same.")]
	[DataType(DataType.Password)]
	[Display(Name = "Password confirm")]
	public string PasswordConfirm { get; set; }

	[Required]
	[Display(Name = "Login")]
	public string Login { get; set; }
}
