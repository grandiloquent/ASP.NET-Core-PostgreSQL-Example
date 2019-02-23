using System;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index([FromQuery(Name = "search")] string search)
        {
            Console.WriteLine(search);
            return StatusCode(501);
        }
    }
}