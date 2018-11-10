using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Abtract;
using OnlineStore.Model.Context;
using OnlineStore.Model.Repository;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Implements
{
    public class OrderService: IOrderService
    {
        private IOrderRepository orderRepository = new OrderRepository();
        OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();

        public void Delete(long id)
        {
            var orderToDelete = orderRepository.GetOrderById(id);
            var listOrderDetailsToDelete = orderRepository.Orderdetails.Where(x => x.OrderID == id).ToList();
            foreach (var item in listOrderDetailsToDelete)
            {
                orderRepository.DeleteOrderDetails(item);
                var product = context.ecom_Products.Where(x => x.Id == item.ProductID).FirstOrDefault();
                product.Quantity += item.Quantity;
                product.TotalBuy -= item.Quantity;
            }
            context.SaveChanges();
            orderRepository.DeleteOrder(orderToDelete);
            orderRepository.Save();
          
        }

        public List<CustomerViewModel> GetListCustomer()
        {
            return orderRepository.Customers.Where(x => x.ecom_Orders.Count() > 0).Select(x => new CustomerViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Address = x.Address,
                Phone = x.Phone,
            }).ToList();
        }

        public List<OrderViewModel> GetOrdersByCustomer(long customerId = 0)
        {

            List<OrderViewModel> model = orderRepository.Orders.Where(x => customerId == 0 || x.CustomerId == customerId).ToList().Select(x => new OrderViewModel()
            {
                OrderId = x.Id,
                CustomerId = x.CustomerId,
                ShipName = x.NameOfRecipient,
                ShipAdress = x.AddressOfRecipient,
                ShipEmail = x.EmailOfRecipient,
                ShipMobile = x.PhoneOfRecipient,
                CreateBy = x.CreateByTy,
                CreateDate = x.CreatedDate,
                Status = x.Status,
                ModifiedBy = x.ModifiedByTy,
                ModifiedDate = x.ModifiedDate,
            }).ToList();
            return model;


        }

        public OrderViewModel GetOrderByID(long id)
        {
            var x = orderRepository.GetOrderById(id);
            OrderViewModel model = new OrderViewModel()
            {
                OrderId = x.Id,
                CustomerId = x.CustomerId,
                ShipName = x.NameOfRecipient,
                ShipAdress = x.AddressOfRecipient,
                ShipEmail = x.EmailOfRecipient,
                ShipMobile = x.PhoneOfRecipient,
                CreateBy = x.CreateByTy,
                CreateDate = x.CreatedDate,
                Status = x.Status,
                ModifiedBy = x.ModifiedByTy,
                ModifiedDate = x.ModifiedDate,
                Note = x.OrderNote,
            };
            model.ListOrderDetails = orderRepository.Orderdetails.Where(z => z.OrderID == model.OrderId).ToList();
            decimal? total = 0;
            foreach (var item in model.ListOrderDetails)
            {
                    total += item.Quantity * item.ecom_Products.Price;
            }
            model.TotalOrder = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}", total);
            return model;
        }

        public void Update(OrderViewModel order)
        {
            var orderToUpdate = orderRepository.GetOrderById(order.OrderId);
            if (orderToUpdate != null)
            {
                orderToUpdate.ModifiedDate = DateTime.Now;
                orderToUpdate.ModifiedByTy = order.ModifiedBy;
                orderToUpdate.Status = order.Status;
                orderRepository.Save();
            }
        }
    }
}
