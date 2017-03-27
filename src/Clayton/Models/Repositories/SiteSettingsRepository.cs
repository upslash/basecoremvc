using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.Repositories
{
    public class SiteSettingsRepository : ISiteSettingsRepository
    {
        private readonly AppDbContext _appDbContext;

        public SiteSettingsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public SiteSettings GetSettings()
        {
            return _appDbContext.SiteSettings.FirstOrDefault();
        }

        public void SaveSettings(SiteSettings settings)
        {
            var dataSettings = _appDbContext.SiteSettings.FirstOrDefault();
            if (dataSettings != null)
            {
                // Update
                dataSettings.Name = settings.Name;
                dataSettings.Slogan = settings.Slogan;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add
                _appDbContext.Add(settings);
                _appDbContext.SaveChanges();
            }
        }
    }
}
