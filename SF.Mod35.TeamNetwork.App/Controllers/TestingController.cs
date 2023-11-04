using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SF.Mod35.TeamNetwork.App.Views.Profile;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using SF.Mod35.TeamNetwork.App.Views.Search;
using SF.Mod35.TeamNetwork.App.DataAccess.Repository;
using SF.Mod35.TeamNetwork.App.DataAccess.UoW;
using SF.Mod35.TeamNetwork.Testing;

namespace SF.Mod35.TeamNetwork.App.Controllers;

/// <summary>
/// This controller will be used for testing purposes like
/// creating users and easily switching between they profiles.
/// </summary>
public class TestingController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly DataGenerator _data = new DataGenerator();
    private readonly string _testPassword = "123456";

    public TestingController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [Route("TestingEnviroment")]
    [HttpGet]
    public async Task<IActionResult> TestingEnviroment(string search)
    {
        if (IsTestingEnabled())
        {
            return View("TestingEnviroment");
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("Generate")]
    [HttpGet]
    public async Task<IActionResult> Generate()
    {
        if (IsTestingEnabled())
        {
            List<User> testUsers = _data.UserFaker.Generate(35);
            foreach (var user in testUsers)
            {
                var result = await _userManager.CreateAsync(user, _testPassword);
            }
            return RedirectToAction("TestingEnviroment", "Testing");
        }
        return RedirectToAction("Index", "Home");
    }

    private bool IsTestingEnabled()
    {
        string enableTestController = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // Return true if the environment variable is set to "true"
        return string.Equals(enableTestController, "Development", StringComparison.OrdinalIgnoreCase);
    }
}

