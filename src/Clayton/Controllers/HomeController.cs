using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clayton.Models;

namespace Clayton.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Posts = _postRepository.Posts;
            ViewBag.SiteName = "Clayton's new website!";
            return View(model);
        }

        //[Route("post/{id:int?}/{slug}", Name = "PostWithSlug")]
        public IActionResult ViewPost(int id, string slug)
        {
            Post post = _postRepository.GetPostById(id);
            return View(post);
        }

        public IActionResult ViewCategory(int id, string categoryTitle)
        {
            return View(_categoryRepository.GetCategoryByIdWithPosts(id));
        }
    }
}
