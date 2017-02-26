using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clayton.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Clayton.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPostRepository _postRepository;

        public DashboardController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        
        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Posts = _postRepository.Posts.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult EditPost(int postId)
        {
            return View(_postRepository.GetPostById(postId));
        }

        [HttpPost]
        public IActionResult EditPost(Post post)
        {
            var result = _postRepository.UpdatePost(post);
            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(int postId)
        {

        }
    }
}
