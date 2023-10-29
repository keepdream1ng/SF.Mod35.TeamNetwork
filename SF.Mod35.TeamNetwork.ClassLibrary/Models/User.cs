using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;

namespace SF.Mod35.TeamNetwork.ClassLibrary.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string ImageUrl { get; set; }
    public string? Status { get; set; }
    public string? About { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
