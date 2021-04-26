using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        BookStoreEntities db = new BookStoreEntities();
        public ActionResult BookByCategory(string id_category)
        {
            Category category = db.Categories.SingleOrDefault(x => x.Id_Category == id_category);
            if(category == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Book> books = db.Books.Where(n => n.Id_Category == id_category).ToList();
            if(books.Count == 0)
            {
                ViewBag.sach = "Không tồn tại loại sách thuộc chủ đề";
            }
            return View(books);
        }
    }
}