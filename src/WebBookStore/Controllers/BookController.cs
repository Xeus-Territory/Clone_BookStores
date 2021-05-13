using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            int? count = book.CountView;
            if (book == null)
            {
                //Trả về phương thức lỗi
                Response.StatusCode = 404;
                return null;
            }
            book.CountView = ++count;
            db.SaveChanges();
            return View(book);
        }
        public PartialViewResult RandomBook()
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, db.Books.Count());
            var item = db.Books.OrderBy(n => n.Id_Book).Skip(toSkip).Take(3).ToList();
            return PartialView(item);
        }
        public PartialViewResult CountViewBook()
        {
            var count = db.Books.OrderByDescending(n => n.CountView).Take(3).ToList();
            return PartialView(count);
        }
        public PartialViewResult DateExpUpdate()
        {
            var datenow = DateTime.Now;
            var dateupdate = db.Books.Select(n => n.DateExpUpdate).Distinct().ToList();
            DateTime min = datenow;
            for(int i = 0; i < dateupdate.Count; i++)
            {
                min = (DateTime.Compare(min, dateupdate[i].Value) < 0) ? min : dateupdate[i].Value;
            }
            var date = db.Books.Where(n => n.DateExpUpdate == min).Take(3).ToList();
            return PartialView(date);
        }
        public PartialViewResult BestSellList()
        {
            var item = db.Books.Join(db.OrderDetails, x => x.Id_Book, y => y.id_Book, 
                        (x, y) => new { x.Id_Book ,x.Images, x.Title, x.Price, y.Quantity })
                       .OrderByDescending(x => x.Quantity).Take(20).ToList();
            return PartialView(item);
        }
        public ViewResult ViewAll()
        {
            var listbook = db.Books.Take(20).ToList();
            return View(listbook);
        }
    }
}