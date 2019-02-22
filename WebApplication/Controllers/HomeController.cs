using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataRepository _repository;

        public HomeController(IDataRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "首页";
            ViewBag.Watch = false;
            ViewBag.SiteName = "激励过";
            return View(_repository.GetLastVideos());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            _repository.Delete(id);
            return RedirectToAction("Views");
        }

        [HttpPost]
        public IActionResult Edit(Video video)
        {
            _repository.UpdateVideo(video, null);
            return RedirectToAction("Views");
        }

        public IActionResult Edit(long id)
        {
            ViewBag.CreateMode = false;
            return View("Create", _repository.GetVideo(id));
        }

        public IActionResult Views()
        {
            return View(_repository.GetAllVideos());
        }

        [HttpPost]
        public IActionResult Create(Video video)
        {
            if (!ModelState.IsValid)
            {
                return View(video);
            }

            video.UpdatedAt = DateTime.UtcNow;
            video.CreatedAt = DateTime.UtcNow;

            var result = _repository.CreateVideo(video);
            return RedirectToAction("Views");
        }
    }
}