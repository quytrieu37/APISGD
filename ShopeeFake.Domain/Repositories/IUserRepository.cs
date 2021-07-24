using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Users { get; }
        IQueryable<UserRole> UserRole { get; }
        void AddUser(User User);
        void EditUser(User User);
        void DeleteUser(User User);
        void AddUserRole(UserRole userRole);
    }
}
