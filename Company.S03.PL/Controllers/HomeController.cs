using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Company.S03.PL.Models;
using Company.S03.PL.Services;
using System.Text;

namespace Company.S03.PL.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IScopedServices scopedServices2;
    private readonly ITransientServices transientServices1;
    private readonly ISingletonServices singletonServices1;

    public IScopedServices ScopedServices1 { get; }
    public ITransientServices TransientServices2 { get; }
    public ISingletonServices SingletonServices2 { get; }

    public HomeController(ILogger<HomeController> logger ,
        IScopedServices scopedServices1,
        IScopedServices scopedServices2,
        ITransientServices transientServices1,
        ITransientServices transientServices2,
        ISingletonServices singletonServices1,
        ISingletonServices singletonServices2
        )
    {
        _logger = logger;
        ScopedServices1 = scopedServices1;
        this.scopedServices2 = scopedServices2;
        this.transientServices1 = transientServices1;
        TransientServices2 = transientServices2;
        this.singletonServices1 = singletonServices1;
        SingletonServices2 = singletonServices2;
    }

    public string LifeTimeTest()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append($"ScopedServices1 : {ScopedServices1.GetGuid()}");
        builder.Append($"ScopedServices2 : {scopedServices2.GetGuid()}");
        builder.Append($"TransientServices1 : {transientServices1.GetGuid()}");
        builder.Append($"TransientServices2 : {TransientServices2.GetGuid()}");
        builder.Append($"SingletonServices1 : {singletonServices1.GetGuid()}");
        builder.Append($"SingletonServices2 : {SingletonServices2.GetGuid()}");
        return builder.ToString();
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
