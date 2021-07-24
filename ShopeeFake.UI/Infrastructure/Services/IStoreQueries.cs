using ShopeeFake.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Infrastructure.Services
{
    public interface IStoreQueries
    {
        /// <summary>
        /// method get all data category
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetAllCategories();
        /// <summary>
        /// method get data category by id
        /// </summary>
        /// <param name="categoryId">category identify</param>
        /// <returns></returns>
        Task<Category> GetCategoryById(int categoryId);


        //Product


        /// <summary>
        /// method get all product(full infomation)
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAllProducts();
        /// <summary>
        /// get all product (summary)
        /// </summary>
        /// <returns></returns>
        Task<List<dynamic>> GetAllProductSummary();
        /// <summary>
        /// method get data product by Id
        /// </summary>
        /// <param name="Id">product identify</param>
        /// <returns></returns>
        Task<Product> GetProductById(int Id);
        /// <summary>
        /// method get product base storeId
        /// </summary>
        /// <param name="StoreId">Store Identify</param>
        /// <returns></returns>
        Task<List<Product>> GetProductByStoreId(int StoreId);

        //store

        /// <summary>
        /// Method get all store in db
        /// </summary>
        /// <returns></returns>
        Task<List<Store>> GetAllStore();
        /// <summary>
        /// Method get store infomation base store Identify
        /// </summary>
        /// <param name="Id">Store identify</param>
        /// <returns></returns>
        Task<Store> GetStoreById(int Id);
        /// <summary>
        /// method get list store base userId
        /// </summary>
        /// <param name="UserId">User Identify</param>
        /// <returns></returns>
        Task<List<Store>> GetStoreByUserId(int UserId);
        Task<List<Role>> GetRoleByUserId(int userId);

        /// <summary>
        /// method get all product purchased base user identify
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<dynamic>> GetProductPurchased(int UserId);

        //Order
        /// <summary>
        /// method get order theo thoi gian
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<List<Order>> GetAllOrder(DateTime? start =null, DateTime? end =null);
        Task<List<Order>> GetOrderByUserId(int UserId);
        Task<List<OrderDetail>> GetOrderDetailByOrderId(int OrderId);
        /// <summary>
        /// method get list order base store Identify
        /// </summary>
        /// <param name="StoreId"></param>
        /// <returns></returns>
        Task<List<dynamic>> GetOrderByStoreId(int StoreId);

        //admin
        Task<List<User>> GetAllUser();
    }
}
