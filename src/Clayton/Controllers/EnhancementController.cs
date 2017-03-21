using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Clayton.Models;

namespace Clayton.Controllers
{
    [Authorize]
    public class EnhancementController : Controller
    {
        private readonly IEnhancementRepository _enhancementRepository;

        public EnhancementController(IEnhancementRepository enhancementRepository)
        {
            _enhancementRepository = enhancementRepository;
        }

        public IActionResult Index()
        {
            EnhancementViewModel model = new EnhancementViewModel();
            model.Enhancements = _enhancementRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Enhancement enhancement)
        {
            if (!ModelState.IsValid)
            {
                return View(enhancement);
            }

            Enhancement saved = _enhancementRepository.Create(enhancement);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Enhancement enhancement = _enhancementRepository.GetById(id);
            return View(enhancement);
        }

        [HttpPost]
        public IActionResult Edit(Enhancement enhancement)
        {
            if (!ModelState.IsValid)
            {
                return View(enhancement);
            }
            _enhancementRepository.Edit(enhancement);
            return RedirectToAction("Index");
        }
    }
}
