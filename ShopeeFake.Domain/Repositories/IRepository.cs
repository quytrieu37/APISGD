using System;
using System.Collections.Generic;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface IRepository<T>
    {
        IUnitOfWork unitOfWork { get; }
    }
}
