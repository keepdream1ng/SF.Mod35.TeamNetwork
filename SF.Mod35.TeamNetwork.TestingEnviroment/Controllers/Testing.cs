using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;
using System.Web.Mvc;

namespace SF.Mod35.TeamNetwork.TestingEnviroment.Controllers;

public class Testing : Controller
{
    private readonly ILogger<Testing> _logger;
    private readonly SignInManager<User> _signInManager;

    public Testing(ILogger<Testing> logger,
        SignInManager<User> signInManager)
    {
        _logger = logger;
        _signInManager = signInManager;
    }

    [Route("")]
    [Route("[controller]/[action]")]
    public IActionResult Index()
    {
        return View();
    }
}
