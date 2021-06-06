using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Common;
using WebBookStore.Models;
using WebBookStore.Areas.Admin.Models;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class HomePageController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: Admin/HomePage
        [HasCredential(RoleID = "VIEW_BOOK")]
        public ViewResult Index()
        {
            return View();
        }

        [HasCredential(RoleID = "VIEW_BOOK")]
        public PartialViewResult resultSomething(string searchsomething)
        {
            SearchModel result = new SearchModel
            {
                listbook = db.Books.Where(x => x.Title.Contains(searchsomething) && x.Condition == true).ToList(),
                listorder = db.Orders.Where(x => x.Account.Name.Contains(searchsomething)).ToList(),
                listaccount = db.Accounts.Where(x => x.UserName.Contains(searchsomething)).ToList()
            };
            ViewBag.Searching = searchsomething;
            ViewBag.Result = "Không tìm thấy dữ liệu bạn cần tìm";
            return PartialView(result);
        }
    }
}