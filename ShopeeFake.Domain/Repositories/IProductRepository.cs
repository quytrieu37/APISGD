using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> Products { get; }
        void AddProduct(Product product);
        void EditProduct(Product product);
        void DeleteProduct(Product product);
    }
}
