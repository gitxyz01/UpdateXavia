using OnlineStore.Model.Context;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface ICartService
    {
        ecom_Products GetProductByID(long id);
        void ProsessOrder(ShippingDetailsViewModel shippingDetails, Cart cart);
    }
}

