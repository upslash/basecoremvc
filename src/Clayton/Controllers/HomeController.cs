using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clayton.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Clayton.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;

        public HomeController(IPostRepository postRepository)
        {
            // Dependency injection
            // This works because of the Startup.cs file
            _postRepository = postRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Posts = _postRepository.Posts;
            ViewBag.SiteName = "Clayton's new website!";
            return View(model);
        }
    }
}
