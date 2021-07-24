using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopeeFake.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> Orders { get; }
        void AddOrder(Order Order);
        void EditOrder(Order Order);
        void DeleteOrder(Order Order);
        IQueryable<OrderDetail> OrderDetails { get; }
        void AddOrderDetail(OrderDetail orderDetail);
        /// <summary>
        /// delete one line in order
        /// </summary>
        /// <param name="OrderDetail"></param>
        void DeleteDetail(OrderDetail OrderDetail);
    }
}
