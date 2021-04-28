using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class CartController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: Session Cart
        public List<Cart> TakeCartToView()
        {
            List<Cart> listcart = Session["Cart"] as List<Cart>;
            if (listcart == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tọa list giỏ hàng (session cart)
                listcart = new List<Cart>();
                Session["Cart"] = listcart;
            }
            return listcart;
        }
        // GET: AddToCart
        public ActionResult AddToCart(string id_book, string Strurl)
        {
            Book book = db.Books.SingleOrDefault(x => x.Id_Book == id_book);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Take Session giỏ hàng 
            List<Cart> listcart = TakeCartToView();
            //Kiểm tra sách đã tồn tại trong session chưa
            Cart cart = listcart.Find(x => x.sID_Book == id_book);
            if (cart == null)
            {
                //Chưa tồn tại thì thêm mới
                cart = new Cart(id_book);
                listcart.Add(cart);
                // Trả về string url cần add
                return Redirect(Strurl);
            }
            else
            {
                //Đã tồn tại thì tăng thêm 1
                cart.sQuantity++;
                return Redirect(Strurl);
            }

        }
        // Cập nhật giỏ hàng
        public ActionResult UpdateCart(string id_book, FormCollection f)
        {
            Book book = db.Books.SingleOrDefault(x => x.Id_Book == id_book);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listcart = TakeCartToView();
            Cart cart = listcart.Find(x => x.sID_Book == id_book);
            if (cart != null)
            {
                cart.sQuantity = Convert.ToInt32(f["Quantity"].ToString());
            }
            return View("Cart");

        }

        // Xóa giỏ hàng 
        public ActionResult DeleteCart(string id_book)
        {
            Book book = db.Books.SingleOrDefault(x => x.Id_Book == id_book);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Cart> listcart = TakeCartToView();
            Cart cart = listcart.SingleOrDefault(n => n.sID_Book == id_book);
            if (cart != null)
            {
                listcart.RemoveAll(x => x.sID_Book == id_book);
            }
            if (listcart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        //Trả lại View Giỏ hàng
        public ActionResult ViewCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(TakeCartToView());
        }
        // Tính tổng số sách và tổng giá tiền
        public int TotalItem()
        {
            int TongSoSP = 0;
            List<Cart> listcart = Session["Cart"] as List<Cart>;
            if (listcart != null)
            {
                TongSoSP = listcart.Sum(x => x.sQuantity);
            }
            return TongSoSP;
        }
        public int TotalPrice()
        {
            int TongSoTien = 0;
            List<Cart> listcart = Session["Cart"] as List<Cart>;
            if (listcart != null)
            {
                TongSoTien = listcart.Sum(x => x.Total);
            }
            return TongSoTien;
        }
        // Clear All Giỏ hàng 
        public List<Cart> ClearAll()
        {
            List<Cart> listcart = TakeCartToView();
            if (listcart != null)
            {
                Session["Cart"] = new List<Cart>();
                listcart = Session["Cart"] as List<Cart>;
            }
            return listcart;
        }
        // Partial Giỏ hảng
        public ActionResult PartialCart()
        {
            if(TotalItem() == 0)
            {
                return PartialView();
            }
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if(listCart != null)
            {
                ViewBag.TotalItem = TotalItem();
                ViewBag.TotalPrice = TotalPrice();
                return PartialView(listCart);
            }
            ViewBag.TotalItem = TotalItem();
            ViewBag.TotalPrice = TotalPrice();
            return PartialView();
        }
            
    }
}