using Clayton.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISiteSettingsRepository _siteSettingsRepository;

        public HeaderViewComponent(ISiteSettingsRepository siteSettingsRepository)
        {
            _siteSettingsRepository = siteSettingsRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_siteSettingsRepository.GetSettings());
        }
    }
}
