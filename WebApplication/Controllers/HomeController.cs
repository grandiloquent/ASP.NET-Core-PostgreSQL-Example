using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private IDataRepository _repository;

        public HomeController(IDataRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "首页";
            ViewBag.Watch = true;
            ViewBag.SiteName = "激励过";
            return View(_repository.Videos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Video video)
        {
            if (!ModelState.IsValid)
            {
                return View(video);
            }

            video.UpdatedAt = DateTime.Now;
            video.CreatedAt = DateTime.Now;

            Console.WriteLine(video.ToString());
            return Content("");
        }
    }
}