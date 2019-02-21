using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    /*[Authorize]*/
    public class PsychoController : Controller
    {
        private readonly ILogger<PsychoController> _logger;

        public PsychoController(ILogger<PsychoController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.Title = "登录";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Psycho model, string returnUrl = null)
        {
            _logger.Log(LogLevel.Error, model.Name + ModelState.IsValid);

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid login attempt.");

                return View(model);
            }

            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}