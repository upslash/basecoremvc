using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.Repositories
{
    public interface ISiteSettingsRepository
    {
        SiteSettings GetSettings();
        void SaveSettings(SiteSettings settings);
    }
}
