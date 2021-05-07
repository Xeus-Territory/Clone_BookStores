using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookStore.Models
{
    public class Register
    {
        [Key]
        public long Id_Customer { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*Yêu cầu nhập tên đăng nhập !")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*Yêu cầu nhập mật khẩu !")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "*Độ dài mật khẩu ít nhất 6 ký tự !")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "*Xác nhận mật khẩu không đúng !")]
        public string ConfirmPass { get; set; }
        [Display(Name = "Họ tên")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Favorite:")]
        public string Favourite { get; set; }
        public virtual bool EmailConfirm { get; set; }
        public System.Guid ActivationCode { get; set; }
    }
}