using PingoneAuthDemo.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PingoneAuthDemo.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            PingoneAuthServices pingoneAuthServices = new PingoneAuthServices();
            var url = await pingoneAuthServices.Login();
            return Redirect(url);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}