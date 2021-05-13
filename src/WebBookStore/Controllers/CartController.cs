using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Common;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class CartController : Controller
    {
        BookStoreEntities db = new BookStoreEntities();
        // GET: Session Cart
        #region Giỏ hàng
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
        // Thêm giỏ hàng từ trang detail
        public ActionResult AddToCartMany(string id_book, string Strurl, FormCollection f)
        {
            //Conver f ra thành số liệu
            int re = Convert.ToInt32(f["value-item"].ToString());
            //Kiểm tra sách có tồn tại hay không
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
                if (re > 0)
                {
                    //Chưa tồn tại thì thêm mới
                    cart = new Cart(id_book);
                    for(int i = 0; i < re; i++)
                    {
                        listcart.Add(cart);
                    }
                    return Redirect(Strurl);
                }
                else
                {
                    return Redirect(Strurl);
                }    
            }
            else
            {
                if (re > 0)
                {
                    //Đã tồn tại thì tăng đúng thêm số lượng
                    cart.sQuantity += re;
                    return Redirect(Strurl);
                }
                else
                {
                    return Redirect(Strurl);
                }    
            }    
        }
        // Cập nhật giỏ hàng
        public ActionResult UpdateCart(string id_book, string Strurl, FormCollection f)
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
                if(cart.sQuantity > 0)
                {
                    cart.sQuantity = cart.sQuantity + int.Parse(f["btninc"].ToString());
                }
                else
                {
                    cart.sQuantity = cart.sQuantity + 1;
                }    
            }
            return Redirect(Strurl);
        }

        // Xóa giỏ hàng 
        public ActionResult DeleteCart(string id_book, string Strurl)
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
                return Redirect(Strurl);
            }
            return Redirect(Strurl);
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
        public ActionResult ClearAllCart(string Strurl)
        {
            ClearAll();
            return Redirect(Strurl);
        }
        #endregion
        #region Order sản phẩm
        [HttpPost]
        public ActionResult ViewOrderDetail()
        {
            //Kiểm tra đăng nhập
            if (Session[CommonConstant.USER_SESSION] == null || Session[CommonConstant.USER_SESSION].ToString() == "")
            {
                return RedirectToAction("Index", "Login");
            }
            //Kiểm tra giỏ hàng
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TotalPrice = TotalPrice();
            return View(listCart);
        }
        public int ApproveDiscount(string Id_discount)
        {
            int total = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            List<Discount> listdiscount = db.Discounts.ToList();
            foreach (var item in listCart)
            {
                foreach (var i in listdiscount)
                {
                    if (Id_discount == i.id_Discount && item.sID_Book == i.id_Book)
                    {
                        total += (item.sPrice - (item.sPrice * (int.Parse(i.DiscountDetail))) / 100) * item.sQuantity;
                    }
                    else
                    {
                        if (Id_discount != i.id_Discount)
                        {
                            total += 0;
                        }
                        else
                        {
                            total += item.sPrice * item.sQuantity;
                        }
                    }
                }
            }
            return total;
        }
        public ActionResult GetDiscount(FormCollection f)
        {
            string iddiscount = f["discount"].ToString();
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (ApproveDiscount(f["discount"].ToString()) == TotalPrice())
            {
                ViewBag.Discount = 0;
                ViewBag.TotalPrice = TotalPrice();
                return View(listCart);
            }
            ViewBag.TotalPrice = TotalPrice();
            ViewBag.ApproveDiscount = ApproveDiscount(f["discount"].ToString());
            var discount = db.Discounts.Where(x => x.id_Discount == iddiscount).FirstOrDefault();
            Session["discount"] = discount;
            ViewBag.Discount = discount.DiscountDetail;
            return View(listCart);
        }
        public ActionResult OrderCart(FormCollection f)
        {
            Order order = new Order();
            List<Cart> listCart = TakeCartToView();
            Account acc = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            Discount discount = (Discount)Session["discount"];
            order.Id_Customer = acc.Id_Customer;
            order.OrderDate = DateTime.Now;
            order.Paymethod = "Trả bằng tiền mặt khi giao hàng";
            //Kiểm tra có áp dụng discount không
            if(Session["discount"] == null)
            {
                order.Totalbill = TotalPrice();
            }
            else
            {
                order.Totalbill = ApproveDiscount(discount.id_Discount);
            }    
            order.AddressShipping = f["Address"].ToString() + ", " + f["City"].ToString();
            //Add don hang vao de co id don hang
            db.Orders.Add(order);
            //Them chi tiet don hang
            foreach (var item in listCart)
            {
                OrderDetail detail = new OrderDetail();
                detail.id_Order = order.Id_Order;
                detail.id_Book = item.sID_Book;
                detail.Quantity = item.sQuantity;
                detail.Price = item.Total;
                db.OrderDetails.Add(detail);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return View(acc);
        }
    }
    #endregion
}