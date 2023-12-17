using PingoneAuthDemo.Services;
using PingOneDemo.Model;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PingoneAuthDemo.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public async Task<ActionResult> Login()
        {
            PingoneAuthServices pingoneAuthServices = new PingoneAuthServices();
            var url = await pingoneAuthServices.Login();
            return Redirect(url);
        }
        public ActionResult SignUp(RegistrationViewModel model)
        {
            PingoneAuthServices pingoneAuthServices = new PingoneAuthServices();
            try
            {
                pingoneAuthServices.Register(model);
                return RedirectToAction("SignUpSuccess");

            }
            catch (Exception)
            {
                return RedirectToAction("SignUpError");
            }
        }
        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult SignUpError()
        {
            return View();
        }
    }
}