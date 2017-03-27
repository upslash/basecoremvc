using Clayton.Models;
using Clayton.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Controllers
{
    [Authorize]
    public class SiteSettingsController : Controller
    {
        private readonly ISiteSettingsRepository _siteSettingsRepository;

        public SiteSettingsController(ISiteSettingsRepository siteSettingsRepository)
        {
            _siteSettingsRepository = siteSettingsRepository;
        }

        public IActionResult Index()
        {
            SiteSettings settings = _siteSettingsRepository.GetSettings();
            if(settings == null)
            {
                settings = new SiteSettings();
            }
            return View(settings);
        }

        [HttpPost]
        public IActionResult Save(SiteSettings settings)
        {
            _siteSettingsRepository.SaveSettings(settings);
            return RedirectToAction("Index");
        }

    }
}
