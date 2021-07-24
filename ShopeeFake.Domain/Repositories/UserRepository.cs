using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopContext _context;
        public UserRepository(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<User> Users => _context.Users;

        public IUnitOfWork unitOfWork => _context;

        public IQueryable<UserRole> UserRole => _context.UserRoles;

        public void AddUser(User User)
        {
            _context.Users.Add(User);
        }

        public void EditUser(User User)
        {
            _context.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteUser(User User)
        {
            _context.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void AddUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
        }
    }
}
