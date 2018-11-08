using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Abtract
{
    public interface IOrderRepository
    {
        IQueryable<ecom_OrderDetails> Orderdetails { get; }
        IQueryable<ecom_Customer> Customers { get; }
        IQueryable<ecom_Orders> Orders { get; }

        List<ecom_Orders> GetAllOrder();
        List<ecom_Orders> GetAllOrderActive();
        ecom_Orders GetOrderById(long id);
        void Add(ecom_Orders order);
        void DeleteOrder(ecom_Orders order);
        void Save();

        ecom_Customer GetCustomerById(long id);
        void Add(ecom_Customer customer);
        void DeleteCustomer(ecom_Customer customer);

        ecom_OrderDetails GetOrderDetailsById(long id);
        void Add(ecom_OrderDetails orderDetails);
        void DeleteOrderDetails(ecom_OrderDetails orderDetails);
    }
}
