using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BookStoreEntities db = new BookStoreEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}