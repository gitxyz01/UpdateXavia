using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infractructure.Utility
{
    public class Define
    {
        public const int PAGE_SIZE = 10;
        public const int ID_PAGE_INTRODUCTION = 18;
        public enum SystemConfig
        {
            TotalVisitors
        }

        public enum Status
        {
            [Description("Ngưng hoạt động")]
            Deactive = 0,

            [Description("Đang hoạt động")]
            Active = 1,

            [Description("Xóa")]
            Delete = 2,

            [Description("Duyệt đăng")]
            WaitingCreate = 3,

            [Description("Duyệt xóa")]
            WaitingDelete = 4
        }
        public enum OrderStatus
        {
            [Description("Chờ Xác Nhận")]
            Waiting = 0,

            [Description("Đang Giao Hàng")]
            Shipping = 1,

            [Description("Đã Giao Hàng")]
            Finish = 2,

            [Description("Đã Hủy")]
            Remove = 3,
        }

        public enum MenuEnum
        {
            [Description("Admin")]
            Admin = 1,

            [Description("User")]
            User = 2,
        }

        public enum BannerTypes
        {
            [Description("Banner 1 - Slide")]
            Banner1 = 1,

            [Description("Banner 2 - Mùa Xuân")]
            SpringSeason = 2,

            [Description("Banner 2 - Mùa Hạ")]
            SummerSeason = 3,

            [Description("Banner 2 - Mùa Thu")]
            AutumnSeason = 4,

            [Description("Banner 2 - Mùa Đông")]
            WinterSeason = 5,

            [Description("Popup")]
            Popup = 10
        }
    }
}
