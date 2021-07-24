/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.repoTest
{
    public class RepoTest<T>
    {
        private readonly ShopContext _context;
        public RepoTest(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<T> listItem => _context.

        public IQueryable<T> OrderDetails => _context.OrderDetails;

        public void AddOrder(T Order)
        {
            _context.
        }

        public void EditOrder(Order Order)
        {
            _context.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteOrder(Order Order)
        {
            _context.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void DeleteDetail(OrderDetail OrderDetail)
        {
            _context.Entry(OrderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }
    }
}
*//*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.repoTest
{
    public class RepoTest<T>
    {
        private readonly ShopContext _context;
        public RepoTest(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<T> listItem => _context.

        public IQueryable<T> OrderDetails => _context.OrderDetails;

        public void AddOrder(T Order)
        {
            _context.
        }

        public void EditOrder(Order Order)
        {
            _context.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteOrder(Order Order)
        {
            _context.Entry(Order).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void DeleteDetail(OrderDetail OrderDetail)
        {
            _context.Entry(OrderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }
    }
}
*/