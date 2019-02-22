using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HelpController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
                View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Feedback(Feedback feedback)
        {
            Console.WriteLine(feedback.Content);
            return RedirectToAction("Index");
        }

        public IActionResult Feedback()
        {
            return View();
        }
    }
}