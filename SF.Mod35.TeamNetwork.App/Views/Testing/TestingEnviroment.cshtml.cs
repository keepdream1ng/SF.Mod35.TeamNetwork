using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.Views.Testing;

public class TestingEnviromentModel
{
    public List<User> Users { get; set; }
    public string TestingPassword { get; set; }

    public TestingEnviromentModel(List<User> users, string password)
    {
        Users = users;
        TestingPassword = password;
    }
}
