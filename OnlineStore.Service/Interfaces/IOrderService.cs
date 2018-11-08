using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderViewModel> GetOrdersByCustomer(long customerId = 0);
        OrderViewModel GetOrderByID(long id);
        void Delete(long id);
        List<CustomerViewModel> GetListCustomer();
        void Update(OrderViewModel order);
    }
}
