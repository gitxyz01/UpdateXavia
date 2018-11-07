using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.MessageModel
{
    public class CreateCategoryPostRequest
    {
        public int Id { get; set; }
        [DisplayName("Tên Danh Mục")]
        public string Name { get; set; }
        [DisplayName("Danh Mục Cha")]
        public Nullable<int> ParentId { get; set; }
        [DisplayName("Mô Tả")]
        public string Description { get; set; }
        [DisplayName("ĐƯỜNG DẪN")]
        public string Url { get; set; }
        [DisplayName("Thứ Tự")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng Thái")]
        public Nullable<int> Status { get; set; }
    }
}
