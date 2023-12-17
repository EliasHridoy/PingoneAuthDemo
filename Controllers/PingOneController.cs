using System.Web.Mvc;

public class PingOneController : Controller
{
    public ActionResult Index()
    {
        ViewBag.Message = "Your application description page.";

        return View();
    }
}