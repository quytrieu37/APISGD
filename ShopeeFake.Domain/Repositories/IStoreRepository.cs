using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        IQueryable<Store> Stores { get; }
        void AddStore(Store Store);
        void EditStore(Store Store);
        void RemoveStore(Store Store);
    }
}
