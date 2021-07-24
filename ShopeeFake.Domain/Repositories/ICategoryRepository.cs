using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IQueryable<Category> Categories { get; }
        void AddCategory(Category category);
        void EditCategory(Category category);
        void RemoveCategory(Category category);
    }
}
