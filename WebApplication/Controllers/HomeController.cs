using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebApplication.Shared;

namespace WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplication.Models;

    public class HomeController : Controller
    {
        private readonly IDataRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDataRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            var video = new Video();
            return View(video);
        }

        [HttpPost]
        public IActionResult Create(Video video)
        {
            /*if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                _logger.Log(LogLevel.Error, ModelState.DumpModelState());
                return View(video);
            }*/

            video.UpdatedAt = DateTime.UtcNow;
            video.CreatedAt = DateTime.UtcNow;

            var result = _repository.CreateVideo(video);
            return RedirectToAction("Views");
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            _repository.Delete(id);
            return RedirectToAction("Views");
        }

        [HttpPost]
        public IActionResult Edit(Video video, Video original)
        {
            _repository.UpdateVideo(video, original);
            return RedirectToAction("Views");
        }

        public IActionResult Edit(long id)
        {
            ViewBag.CreateMode = false;
            var video = _repository.GetVideo(id);

            return View("Create", _repository.GetVideo(id));
        }

        public IActionResult Index()
        {
            ViewBag.Title = "首页";
            ViewBag.Watch = false;
            return View(_repository.GetLastVideos());
        }

        public IActionResult Views()
        {
            return View(_repository.GetAllVideos());
        }

        [HttpPost]
        [Route("api/insert")]
        public IActionResult Insert([FromBody] Video o)
        {
            if (o != null)
            {
                o.Id = 0;
                o.CreatedAt = DateTime.UtcNow;
                o.UpdatedAt = DateTime.UtcNow;
                _repository.CreateVideo(o);
                _logger.Log(LogLevel.Trace, "Insert data with api.");
                return StatusCode((int) HttpStatusCode.OK);
            }
            else
            {
                return StatusCode((int) HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/increase/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Increase(long id)
        {
            await _repository.IncreaseWatchTimes(id);
            return Content("Success");
        }

        [HttpPost]
        [Route("api/like/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Like(long id)
        {
            var val = await _repository.Like(id);
            return Content(val.ToString());
        }

        [HttpPost]
        [Route("api/unlike/{id?}")]
        [AllowAnonymous]
        public async Task<IActionResult> Unlike(long id)
        {
            var val = await _repository.Unlike(id);
            return Content(val.ToString());
        }
    }
}