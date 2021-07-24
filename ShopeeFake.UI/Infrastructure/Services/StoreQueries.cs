using Microsoft.Data.SqlClient;
using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ShopeeFake.UI.Infrastructure.Services
{
    public class StoreQueries : IStoreQueries
    {
        private readonly string _connectionString;
        public StoreQueries(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * ");
                sb.Append("from Categories ");
                string query = sb.ToString();
                IEnumerable<Category> list = await connection.QueryAsync<Category>(query);
                return list.ToList();
            }    
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select Categories.Id ,Categories.CategoryName ");
                sb.Append("from Categories where Categories.id = @categoryId");
                string query = sb.ToString();
                Category category = await connection.QueryFirstOrDefaultAsync<Category>(query, new { categoryId = categoryId});
                return category;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Products ");
                string query = sb.ToString();
                IEnumerable<Product> products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<List<dynamic>> GetAllProductSummary()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select p.ProductName,p.Price, p.CategoryId ");
                sb.Append("from Products as p ");
                string query = sb.ToString();
                IEnumerable<dynamic> products = await connection.QueryAsync<dynamic>(query);
                return products.ToList();
            }
        }
        public async Task<Product> GetProductById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Products as p where p.Id = @ProductId ");
                string query = sb.ToString();
                Product products = await connection.QueryFirstOrDefaultAsync<Product>(query, new { ProductId = Id});
                return products;
            }
        }
        public async Task<List<Product>> GetProductByStoreId(int StoreId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Products as p where p.StoreId = @StoreId");
                string query = sb.ToString();
                IEnumerable<Product> products = await connection.QueryAsync<Product>(query, new { StoreId = StoreId});
                return products.ToList();
            }
        }

        public async Task<List<Store>> GetAllStore()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Stores ");
                string query = sb.ToString();
                IEnumerable<Store> stores = await connection.QueryAsync<Store>(query);
                return stores.ToList();
            }
        }

        public async Task<Store> GetStoreById(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Stores as s where s.Id = @StoreId ");
                string query = sb.ToString();
                Store store = await connection.QueryFirstOrDefaultAsync<Store>(query, new { StoreId = Id });
                return store;
            }
        }

        public async Task<List<Store>> GetStoreByUserId(int UserId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Stores where Stores.UserId = @UserId");
                string query = sb.ToString();
                IEnumerable<Store> stores = await connection.QueryAsync<Store>(query, new { UserId = UserId});
                return stores.ToList();
            }
        }

        public async Task<List<Order>> GetAllOrder(DateTime? start=null, DateTime? end =null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                if(start ==null)
                {
                    start = new DateTime(1754, 1, 1);
                    end = new DateTime(1754, 1, 1);
                }
                TimeSpan tSpan = new TimeSpan(1, 0, 0, 0);
                end += tSpan;
                string st = start.Value.ToString("yyyy-MM-dd");
                string e = end.Value.ToString("yyyy-MM-dd");

                sb.Append("select * from Orders as o where 1=1 ");
                sb.Append("and @st = '1754-1-1' ");
                sb.Append("or ( o.OrderDate <= @e ");
                sb.Append(" and o.OrderDate >= @st) ");
                string query = sb.ToString();
                IEnumerable<Order> orders = await connection.QueryAsync<Order>(query, new { st = st, e = e});
                return orders.ToList();
            }
        }

        public async Task<List<Order>> GetOrderByUserId(int UserId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Orders as o where o.UserId =@UserId ");
                string query = sb.ToString();
                IEnumerable<Order> orders = await connection.QueryAsync<Order>(query, new {UserId = UserId});
                return orders.ToList();
            }
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int OrderId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select * from OrderDetails as od where od.OrderId =@OrderId ");
                string query = sb.ToString();
                IEnumerable<OrderDetail> orderDetails = await connection.QueryAsync<OrderDetail>(query, new {OrderId = OrderId});
                return orderDetails.ToList();
            }
        }

        public async Task<List<dynamic>> GetProductPurchased(int UserId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT Orders.Id as OrderId ,Orders.OrderDate, Products.ProductName, Products.StoreId, ");
                sb.Append("Products.Price, Products.Amount, Products.Description, c.CategoryName ");
                sb.Append("FROM Orders INNER JOIN Products ON Orders.Id = Products.Id ");
                sb.Append("inner join Categories as c ON c.Id = Products.CategoryId where UserId =@UserId ");
                string query = sb.ToString();
                IEnumerable<dynamic> products = await connection.QueryAsync<dynamic>(query, new { UserId = UserId});
                return products.ToList();
            }
        }

        public async Task<List<dynamic>> GetOrderByStoreId(int StoreId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT OrderDetails.ProductId, ProductName , OrderDetails.Amount, OrderDetails.OrderDetailNote, ");
                sb.Append("Orders.OrderDate, Users.FullName as CustomerName ");
                sb.Append("FROM OrderDetails INNER JOIN Orders ON OrderDetails.OrderId = Orders.Id INNER JOIN ");
                sb.Append("Users ON Orders.UserId = Users.Id INNER JOIN Products ON OrderDetails.ProductId = Products.Id where OrderDetails.StoreId =@StoreId ");
                string query = sb.ToString();
                IEnumerable<dynamic> products = await connection.QueryAsync<dynamic>(query, new { StoreId = StoreId });
                return products.ToList();
            }
        }

        public async Task<List<Role>> GetRoleByUserId(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT Roles.Id, Roles.RoleName FROM Roles INNER JOIN ");
                sb.Append("UserRoles ON Roles.Id = UserRoles.RoleId where UserRoles.UserId= @userId");
                string query = sb.ToString();

                IEnumerable<Role> result = await connection.QueryAsync<Role>(query, new { userId });

                return result.ToList();
            }
        }

        public async Task<List<User>> GetAllUser()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT * from Users ");
                string query = sb.ToString();

                IEnumerable<User> result = await connection.QueryAsync<User>(query);

                return result.ToList();
            }
        }
    }
}
