using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Models
{
    public class EditOrderModel
    {
        [Key]
        public int Id_Order { get; set; }

        public int Id_Customer { get; set; }

        public DateTime OrderDate { get; set; }
        [Display(Name = "Ngày vận chuyển")]
        [Required(ErrorMessage = "* Yêu cầu nhập vào ngày vận chuyển !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Ngày hạn chót giao hàng")]
        [Required(ErrorMessage = "* Yêu cầu nhập vào ngày hạn chót giao hàng !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? ExpDeliveryDate { get; set; }
        public int? Id_Status { get; set; }
        public List<StatusOrder> Statuses { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressShipping { get; set; }

        [Display(Name = "Thông báo")]
        public string Note { get; set; }


    }
}