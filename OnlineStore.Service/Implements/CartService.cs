using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Implements
{
    public class CartService : ICartService
    {
        OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();

        public ecom_Products GetProductByID(long id)
        {
            return context.ecom_Products.Find(id);
        }

        public void ProsessOrder(ShippingDetailsViewModel shippingDetails, Cart cart)
        {
            var customer = context.ecom_Customer.FirstOrDefault(x => x.Phone == shippingDetails.Phone);
            if (customer == null)
            {
                customer = new ecom_Customer()
                {
                    Name = shippingDetails.Name,
                    Email = shippingDetails.Email,
                    Address = shippingDetails.Address,
                    Phone = shippingDetails.Phone,
                    //NoiDung = shippingDetails.NoiDung
                };
                context.ecom_Customer.Add(customer);
            }
            else
            {
                customer.Name = shippingDetails.Name;
                customer.Address = shippingDetails.Address;
                customer.Phone = shippingDetails.Phone;
            }
            context.SaveChanges();

            ecom_Orders order = new ecom_Orders()
            {
                CreatedDate = DateTime.Now,
                Status = (int)Define.OrderStatus.Waiting,
                CustomerId = customer.Id,
                AddressOfRecipient = customer.Address,
                NameOfRecipient = customer.Email,
                PhoneOfRecipient = customer.Phone
            };
            context.ecom_Orders.Add(order);
            context.SaveChanges();

            foreach (var item in cart.LineCollection)
            {
                ecom_OrderDetails orderDetaisl = new ecom_OrderDetails()
                {
                    OrderID = order.Id,
                    ProductID = item.Product.Id,
                    Quantity = item.Quantity,
                    PriceOfUnit = item.Product.Price,
                    CreatedDate = DateTime.Now,
                    CreatedByTy = customer.Name,
                    Status = (int)Define.OrderStatus.Waiting
                };
                context.ecom_OrderDetails.Add(orderDetaisl);
            }
            context.SaveChanges();

            //foreach (var item in cart.LineCollection)
            //{
            //    var model = context.ecom_Products.Where(x => x.Id == item.Product.Id).FirstOrDefault();
            //    model.Quantity -= item.Quantity;
            //}
            //context.SaveChanges();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ol>");
            foreach (var line in cart.LineCollection)
            {
                    stringBuilder.Append("<li>");
                    stringBuilder.Append(line.Product.Name);
                    stringBuilder.Append("<span>" + line.Quantity + "x" + line.Product.Price);
                    stringBuilder.Append("= " + line.Quantity * line.Product.Price);
                    stringBuilder.Append("</span>");
                stringBuilder.Append("</ol>");
                stringBuilder.Append("<h3>Tổng Tiền : <strong>" + cart.ComputerTotal() + "</strong></h3>");
            }
            MailHelper emailHelper = new MailHelper();
            emailHelper.Send(
                "Đơn Hàng" + order.Id,
                shippingDetails.Email,
                stringBuilder.ToString(),
                null,
                null);
        }
    }
}
