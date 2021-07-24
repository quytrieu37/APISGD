using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopContext _context;
        public CategoryRepository(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<Category> Categories => _context.Categories;

        public IUnitOfWork unitOfWork => _context;

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void EditCategory(Category category)
        {
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void RemoveCategory(Category category)
        {
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
    }
}
