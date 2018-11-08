using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Abtract;
using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();

        public IQueryable<ecom_Orders> Orders => context.ecom_Orders;

        public IQueryable<ecom_OrderDetails> Orderdetails => context.ecom_OrderDetails;

        public IQueryable<ecom_Customer> Customers => context.ecom_Customer;

        public void Add(ecom_Orders order)
        {
            context.ecom_Orders.Add(order);
        }

        public void Add(ecom_Customer customer)
        {
            context.ecom_Customer.Add(customer);
        }

        public void Add(ecom_OrderDetails orderDetails)
        {
            context.ecom_OrderDetails.Add(orderDetails);
        }

        public void DeleteOrder(ecom_Orders order)
        {
            context.ecom_Orders.Remove(order);
        }

        public void DeleteCustomer(ecom_Customer customer)
        {
            context.ecom_Customer.Remove(customer);
        }

        public void DeleteOrderDetails(ecom_OrderDetails orderDetails)
        {
            context.ecom_OrderDetails.Remove(orderDetails);
        }

        public List<ecom_Orders> GetAllOrder()
        {
            return context.ecom_Orders.ToList();
        }

        public List<ecom_Orders> GetAllOrderActive()
        {
            throw new NotImplementedException();
        }

        public ecom_Customer GetCustomerById(long id)
        {
            return context.ecom_Customer.Find(id);
        }

        public ecom_Orders GetOrderById(long id)
        {
            return context.ecom_Orders.Find(id);
        }

        public ecom_OrderDetails GetOrderDetailsById(long id)
        {
            return context.ecom_OrderDetails.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        //List<ecom_Orders> IOrderRepository.GetAllOrderActive()
        //{
        //    return context.ecom_Orders.Where(x => x.Status != (int)Define.OrderStatus.Huy).ToList();
        //}
    }
}
