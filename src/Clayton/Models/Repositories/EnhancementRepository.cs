using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.Reposistories
{
    public class EnhancementRepository : IEnhancementRepository
    {
        private readonly AppDbContext _appDbContext;

        public EnhancementRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Enhancement Create(Enhancement enhancement)
        {
            enhancement.CreateDate = DateTime.Now;
            _appDbContext.Enhancements.Add(enhancement);
            _appDbContext.SaveChanges();
            return enhancement;
        }

        public void Delete(int enhancementId)
        {
            throw new NotImplementedException();
        }

        public void Edit(Enhancement enhancement)
        {
            var dataEnhancement = GetById(enhancement.EnhancementId);
            dataEnhancement.Title = enhancement.Title;
            dataEnhancement.Content = enhancement.Content;
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Enhancement> GetAll()
        {
            return _appDbContext.Enhancements
                .Include(x => x.EnhancementProgress);
        }

        public Enhancement GetById(int id)
        {
            return _appDbContext.Enhancements.Where(x => x.EnhancementId == id)
                .Include(x => x.EnhancementProgress).FirstOrDefault();
        }

        public Enhancement MarkCompleted(Enhancement enhancement)
        {
            var dataEnhancement = GetById(enhancement.EnhancementId);
            dataEnhancement.CompletedDate = DateTime.Now;
            _appDbContext.SaveChanges();
            return enhancement;
        }
    }
}
