using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class ShippingDetailsViewModel
    {
        [Required(ErrorMessage = "Bạn Chưa Nhập Họ Tên")]
        [DisplayName("Tên Khách Hàng:")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Email")]
        [DisplayName("Địa Chỉ Email:")]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Địa Chỉ")]
        [DisplayName("Địa Chỉ Khách Hàng:")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn Chưa Nhập Số Điện Thoại")]
        [DisplayName("Số Điện Thoại:")]
        [StringLength(200)]
        public string Phone { get; set; }

        [DisplayName("Lời Nhắn")]
        public string Note { get; set; }
    }
}
