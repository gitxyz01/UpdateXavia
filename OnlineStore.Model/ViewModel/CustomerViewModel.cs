using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class CustomerViewModel
    {
        public long Id { get; set; }

        [DisplayName("Tên Khách Hàng")]
        [StringLength(200)]
        public string Name { get; set; }

        [DisplayName("Địa Chỉ Email")]
        [StringLength(200)]
        public string Email { get; set; }

        [DisplayName("Địa Chỉ")]
        [StringLength(200)]
        public string Address { get; set; }

        [DisplayName("Số Điện Thoại")]
        [StringLength(200)]
        public string Phone { get; set; }

        [DisplayName("Danh Sách Đơn Hàng")]
        public IEnumerable<OrderViewModel> AllOrders { get; set; }
    }
}
