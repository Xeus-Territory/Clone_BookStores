using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        BookStoreEntities db = new BookStoreEntities();
        public PartialViewResult CustomerExis()
        {
            var result = db.Accounts.Where(x => x.GroupID == "CUSTOMER").ToList();
            return PartialView(result);
        }
    }
}