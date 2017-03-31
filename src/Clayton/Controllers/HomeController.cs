using Microsoft.AspNetCore.Mvc;
using Clayton.Models;
using Clayton.Models.Reposistories;
using Clayton.Models.ViewModels;

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
            if(post != null && post.Slug == slug && post.Active && !post.DeletedDate.HasValue)
            {
                ViewBag.Title = " - " + post.Title;
                return View(post);
            }
            else
            {
                // Don't return the post if it's been deleted or it is inactive.
                ViewBag.Error = "Oops. We could not find that post.";
                ViewBag.Title = " - Could not find post.";

                return View(new Post());
            }
        }

        public IActionResult ViewCategory(int id, string categoryTitle)
        {
            Category cat = _categoryRepository.GetCategoryByIdWithPosts(id);
            if(cat != null && cat.Title == categoryTitle)
            {
                ViewBag.Title = " - Posts in " + cat.Title;
                return View(cat);
            }
            else
            {
                ViewBag.Error = "Oops. We could not find that category.";
                ViewBag.Title = " - Could not find category";
                return View();
            }
           
        }
    }
}
