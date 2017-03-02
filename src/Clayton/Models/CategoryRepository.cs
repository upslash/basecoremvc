using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Category Create(Category category)
        {
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
            return category;
        }

        public void Delete(int categoryId)
        {
            var dataCategory = _appDbContext.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if(dataCategory != null)
            {
                _appDbContext.Categories.Remove(dataCategory);
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public void Edit(Category category)
        {
            var dataCategory = _appDbContext.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (dataCategory != null)
            {
                dataCategory.Title = category.Title;
                _appDbContext.SaveChanges();
            }
            else
            {
                // Add error handling
            }
        }

        public List<Category> GetAll()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _appDbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
        }
    }
}
