using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.repoTest
{
    public interface IRepoTest<T>
    {
        IQueryable<T> ListItem { get; }
        void Add(T item);
        void Edit(T Item);
        void Delete(T item);
    }
}
