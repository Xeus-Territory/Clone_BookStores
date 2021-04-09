using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        BookStoreEntities db = new BookStoreEntities();
        public PartialViewResult BookPartial()
        {
            var listbook = db.Books.Take(20).ToList();
            return PartialView(listbook);
        }
        public ViewResult DetailBook(string idbook)
        {
            Book book = db.Books.SingleOrDefault(n => n.Id_Book == idbook);
            if (book == null)
            {
                //Trả về phương thức lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(book);
        }
    }
}