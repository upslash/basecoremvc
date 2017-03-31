using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.Reposistories
{
    public interface IEnhancementRepository
    {
        IEnumerable<Enhancement> GetAll();
        Enhancement GetById(int id);
        Enhancement Create(Enhancement enhancement);
        void Delete(int enhancementId);
        void Edit(Enhancement category);
        Enhancement MarkCompleted(Enhancement enhancement);
    }
}
