using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Contacts
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Display(Name="Họ và tên:")]
        public string Name { get; set; }
        [Display(Name = "Ngày gửi:")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Display(Name = "Tiêu đề:")]
        public string Subject { get; set; }
        [Display(Name = "Nội dung:")]
        public string Content { get; set; }
    }
}
