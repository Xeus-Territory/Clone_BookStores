using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookStore.Models
{
    public class Login
    {
        [Required(ErrorMessage = "*Mời nhập user name !")]
        public string UserName { set;  get; }

        [Required(ErrorMessage = "*Mời nhập password !")]
        public string Password { set; get; }
    }
}