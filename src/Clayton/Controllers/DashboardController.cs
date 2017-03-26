using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Clayton.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clayton.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DashboardController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel();
            model.Posts = _postRepository.Posts;
            model.Categories = _categoryRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult EditPost(int postId)
        {
            PostViewModel model = new PostViewModel();
            model.Post = _postRepository.GetPostById(postId);
            model.CategoryList = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Title").ToList();
            List<string> selectedCats = new List<string>();
            foreach (var cat in model.Post.PostCategory)
            {
                selectedCats.Add(cat.CategoryId.ToString());
            }
            model.SelectedCategories = selectedCats.ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPost(PostViewModel model)
        {

            if (!ModelState.IsValid)
            {
                // Reset categories list
                model.CategoryList = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Title").ToList();
                return View(model);
            }

            // Verify slug input is correct
            model.Post.Slug = model.Post.Slug.Replace(" ", "-").ToLower();

            _postRepository.UpdatePost(model);
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
            PostViewModel model = new PostViewModel();
            model.CategoryList = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Title").ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddPost(PostViewModel model)
        {
            if(!ModelState.IsValid)
            {
                // Reset categories list
                model.CategoryList = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Title").ToList();
                return View(model);
            }

            // Verify slug input is correct
            model.Post.Slug = model.Post.Slug.Replace(" ", "-").ToLower();

            // Save
            Post newPost = _postRepository.AddPost(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ActivatePost(int postId)
        {
            _postRepository.ActivatePost(postId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeactivatePost(int postId)
        {
            _postRepository.DeactivatePost(postId);
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

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryRepository.Create(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
            return RedirectToAction("Index");
        }
    }
}
