using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SF.Mod35.TeamNetwork.App.Views.Profile;

public class UserEditViewModel
{
	// Id property is not displayed in the view.
	public string Id { get; set; }

	[Required]
	[Display(Name = "FirstName")]
	public string FirstName { get; set; }

	[Required]
	[Display(Name = "LastName")]
	public string LastName { get; set; }

	[Required]
	[Display(Name = "Email")]
	public string Email { get; set; }

	[Required]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	[Display(Name = "Date of birth")]
	public DateOnly DateOfBirth { get; set; }

	[Required]
	[DataType(DataType.ImageUrl)]
	[Display(Name = "Profile picture link")]
    public string ImageUrl { get; set; }

	[DataType(DataType.Text)]
	[Display(Name = "Profile status")]
    public string? Status { get; set; }

	[DataType(DataType.Text)]
	[Display(Name = "Profile about section")]
    public string? About { get; set; }
}
