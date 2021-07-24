using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopContext _context;
        public OrderRepository(ShopContext shopContext)
        {
            _context = shopContext;
        }
        public IQueryable<Order> Orders => _context.Orders;

        public IUnitOfWork unitOfWork => _context;

        public IQueryable<OrderDetail> OrderDetails => _context.OrderDetails;

        public void AddOrder(Order Order)
        {
            _context.Orders.Add(Order);
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
