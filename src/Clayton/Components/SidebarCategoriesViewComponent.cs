using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clayton.Models;

namespace Clayton.Components
{
    public class SidebarCategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public SidebarCategoriesViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_categoryRepository.GetAll());
        }
    }
}