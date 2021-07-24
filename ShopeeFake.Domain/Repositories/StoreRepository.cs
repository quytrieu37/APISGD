using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ShopContext _context;
        public StoreRepository(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<Store> Stores => _context.Stores;

        public IUnitOfWork unitOfWork => _context;

        public void AddStore(Store Store)
        {
            _context.Stores.Add(Store);
        }

        public void EditStore(Store Store)
        {
            _context.Entry(Store).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void RemoveStore(Store Store)
        {
            _context.Entry(Store).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
    }
}
