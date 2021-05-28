using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class OrderController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: Order
        // Lấy list order tài khoảng đăng nhập hiện tại
        public ActionResult GetOrderByAcc()
        {
            Account acc = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            var item = db.Orders.Where(x => x.Id_Customer == acc.Id_Customer).ToList();
            foreach(var i in item)
            {
                i.Totalbill += 20000;
            }    
            return View(item);
        }
        // Trả về View Detail Order
        public ActionResult OrderDetailView(int Id_Order)
        {
            Account acc = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            var result = db.Orders.Where(x => x.Id_Customer == acc.Id_Customer && x.Id_Order == Id_Order).FirstOrDefault();
            var item = db.OrderDetails.Where(x => x.Order.Id_Order == Id_Order).ToList();
            // Trả về ngày Order
            ViewBag.TimeOrderDate = result.OrderDate.TimeOfDay; ViewBag.DayOrderDate = result.OrderDate.Day;
            ViewBag.MonthOrderDate = result.OrderDate.Month; ViewBag.YearOrderDate = result.OrderDate.Year;

            // Trả về Update
            ViewBag.OrderID = result.Id_Order; ViewBag.TimeNotify = DateTime.Now;
            //ViewBag.TimeExdeliveryDate = result.ExpDeliveryDate.Value.TimeOfDay; ViewBag.DayExdeliveryDate = result.ExpDeliveryDate.Value.Day;
            //ViewBag.MonthExdeliveryDate = result.ExpDeliveryDate.Value.Month; ViewBag.YearExdeliveryDate = result.ExpDeliveryDate.Value.Year;

            // Trả về Người nhận
            ViewBag.Name = result.Account.Name; ViewBag.AddressShipping = result.AddressShipping; 
            ViewBag.Phone = result.PhoneNumber; ViewBag.PayMethod = result.Paymethod;
            if(result.Note != null)
            {
                ViewBag.Noti = result.Note;
            }    

            // Trả về Tính tổng
            int Shippingtax = 20000;
            ViewBag.Total = result.Totalbill; ViewBag.ShippingTax = Shippingtax; ViewBag.AllPrice = result.Totalbill + Shippingtax;

            // Trả về View
            return View(item);
        }
        public PartialViewResult StatusOrder(int Id_Order)
        {
            var item = db.Orders.Where(x => x.Id_Order == Id_Order).FirstOrDefault();
            return PartialView(item);
        }
    }
}