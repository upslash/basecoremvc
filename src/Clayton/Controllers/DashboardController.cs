using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clayton.Models;
using Microsoft.AspNetCore.Authorization;

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
            _postRepository.UpdatePost(post);
            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(int postId)
        {
            _postRepository.DeletePost(postId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            if(!ModelState.IsValid)
            {
                return View(post);
            }

            _postRepository.AddPost(post);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult HardDelete(int postId)
        {
            _postRepository.HardDelete(postId);
            return RedirectToAction("Index");
        }

        public IActionResult RevivePost(int postId)
        {
            _postRepository.RevivePost(postId);
            return RedirectToAction("Index");
        }
    }
}
