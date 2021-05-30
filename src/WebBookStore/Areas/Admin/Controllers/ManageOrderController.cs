using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Areas.Admin.Models;
using WebBookStore.Common;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: Admin/ManageOrder
        BookStoreEntities db = new BookStoreEntities();
        // GET: Admin/EditOrder
        [HasCredential(RoleID = "VIEW_ORDER")]
        public PartialViewResult Index()
        {
            //var model = OrderDao.Instance.ListAllPaging(page, pageSize);
            var model = OrderDao.Instance.ListAllOrder();
            return PartialView(model);
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_ORDER")]
        public ActionResult Edit(int Id_Order)
        {
            EditOrderModel order = OrderDao.Instance.GetOrderById(Id_Order);

            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            using (BookStoreEntities db = new BookStoreEntities())
            {
                order.Statuses = db.StatusOrders.ToList<StatusOrder>();
            }
            return View(order);
        }

        [HttpPost]
        [HasCredential(RoleID = "EDIT_ORDER")]
        [ValidateInput(false)]
        public ActionResult Edit(EditOrderModel order)
        {

            if (ModelState.IsValid)
            {
                var result = OrderDao.Instance.Update(order);
                if (result == 1)
                {
                    ModelState.AddModelError("", "* Cập nhật thành công !");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "* Ngày vận chuyển phải nhỏ hơn ngày hạn chót giao hàng !");
                }
                else if(result == -2)
                {
                    ModelState.AddModelError("", "* Ngày vận chuyển phải lớn hơn ngày đặt hàng " + order.OrderDate.ToString("dd/MM/yyyy") + "!");
                }
                else
                    ModelState.AddModelError("", "* Cập nhật không thành công !");

            }
            ViewBag.Id_Order = order.Id_Order;
            using (BookStoreEntities db = new BookStoreEntities())
            {
                order.Statuses = db.StatusOrders.ToList<StatusOrder>();
            }
            return View(order);
        }

        [HttpGet]
        [HasCredential(RoleID = "DELETE_ORDER")]
        public ActionResult Delete(int Id_Order)
        {
            EditOrderModel order = OrderDao.Instance.GetOrderById(Id_Order);
            if (order == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            using (BookStoreEntities db = new BookStoreEntities())
            {
                order.Statuses = db.StatusOrders.ToList<StatusOrder>();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [HasCredential(RoleID = "DELETE_ORDER")]
        public ActionResult ConfirmDelete(int Id_Order)
        {
            var result = OrderDao.Instance.Delete(Id_Order);
            if (!result)
            {
                Response.StatusCode = 404;
                return null;
            }
            return RedirectToAction("Index", "HomePage");
        }
    }
}