using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;
namespace WebBookStore.Controllers
{
    public class SearchingController : Controller
    {
        // GET: Searching
        BookStoreEntities db = new BookStoreEntities();
        [HttpPost]
        public ActionResult ResultSearching(FormCollection f)
        {
            string keybook = f["textboxtimkiem"].ToString();
            List<Book> result = db.Books.Where(x => x.Title.Contains(keybook)).ToList();
            if(result.Count == 0)
            {
                ViewBag.Thongbao = "Không tìm thấy bất kỳ sách nào theo tìm kiếm của bạn";
            }    

            return View(result);
        }
        public ActionResult ResultSearchingInLayoutSearching(string Keyword)
        {
            List<Book> result = db.Books.Where(x => x.Title.Contains(Keyword)).ToList();
            if (result.Count == 0)
            {
                ViewBag.Thongbao = "Không tìm thấy bất kỳ sách nào theo tìm kiếm của bạn";
            }

            return View(result);
        }
    }
}