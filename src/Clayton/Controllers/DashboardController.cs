﻿using System;
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
            model.Posts = _postRepository.Posts.ToList();
            model.Categories = _categoryRepository.GetAll();
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
                if (model.SelectedCategories == null)
                {
                    ModelState.AddModelError("SelectedCategories", "Please choose at least one category");
                }
                return View(model);
            }
            // Do something about this ugly double check / repeated code for this model error
            if(model.SelectedCategories == null || model.SelectedCategories.Count() == 0)
            {
                ModelState.AddModelError("SelectedCategories", "Please choose at least one category");
            }
            else
            {
                List<Category> SelectedCategories = new List<Category>();
                foreach(var catId in model.SelectedCategories)
                {
                    SelectedCategories.Add(_categoryRepository.GetById(Convert.ToInt32(catId)));
                }

                // Save
                Post newPost = _postRepository.AddPost(model.Post);

            }
            
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
