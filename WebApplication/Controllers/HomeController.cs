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
            if (o == null) return BadRequest("Illegal data");
            o.Id = 0;
            o.CreatedAt = DateTime.UtcNow;
            o.UpdatedAt = DateTime.UtcNow;
            _repository.CreateVideo(o);
            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/update")]
        public async Task<IActionResult> Update([FromBody] Video o)
        {
            if (o.Id < 1)
            {
                return BadRequest("更新数据必须提供 ID");
            }

            var result = await _repository.UpdateVideo(o);
            return Content("Success. Operation: " + result);
        }


        [HttpPost]
        [Route("api/export")]
        [AllowAnonymous]
        public async Task<IActionResult> Export()
        {
            var dst = await _repository.ExportDatabase();

            return Content(dst);
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