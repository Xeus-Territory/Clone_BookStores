using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Models
{
    public class SearchModel
    {
        public List<Book> listbook { get; set; }
        public List<Order> listorder { get; set; }
        public List<Account> listaccount { get; set; }
    }
}