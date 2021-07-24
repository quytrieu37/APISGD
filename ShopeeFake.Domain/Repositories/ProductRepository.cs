using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<Product> Products => _context.Products;

        public IUnitOfWork unitOfWork => _context;

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void EditProduct(Product product)
        {
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteProduct(Product product)
        {
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
    }
}
