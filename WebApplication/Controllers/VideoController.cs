namespace WebApplication.Controllers{using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
    public  class VideoController: Controller
{private readonly IDataRepository _repository;

        public VideoController(IDataRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int id)
        {
            var video = _repository.GetVideo(id);
            ViewBag.Watch = true;
            ViewBag.SiteName = "激励过";
            ViewBag.Title = video.Title;
            return View(video);
        }
}}