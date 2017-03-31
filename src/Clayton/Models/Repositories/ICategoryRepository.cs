using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.Reposistories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Create(Category category);
        void Delete(int categoryId);
        void Edit(Category category);

        Category GetCategoryByIdWithPosts(int id);

        IEnumerable<Category> GetAllWithPosts();
    }
}
