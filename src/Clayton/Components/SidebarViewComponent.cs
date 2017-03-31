using Microsoft.AspNetCore.Mvc;
using Clayton.Models.Reposistories;
using Clayton.Models.ViewModels;

namespace Clayton.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;

        public SidebarViewComponent(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }

        public IViewComponentResult Invoke()
        {
            SidebarViewModel model = new SidebarViewModel();
            model.RecentPosts = _postRepository.GetRecentPosts(10);
            model.ActiveCategories = _categoryRepository.GetAllWithPosts();
            return View(model);
        }
    }
}