using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Common;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserId = user.Id_Customer;
                    dao.SaveUserId(userSession.UserId);
                    Session.Add(CommonConstant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "*Tài khoản không tồn tại !");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "*Tài khoản bị khóa !");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "*Tài khoản hoặc mật khẩu không đúng !");
                }
                else
                {
                    ModelState.AddModelError("", "*Tài khoản hoặc mật khẩu không đúng !");
                }
            }
            return View("Index");
        }
        public PartialViewResult PartialLogin()
        {
            if(Session[CommonConstant.USER_SESSION] == null)
            {
                ViewBag.Login = 0;
                return PartialView();
            }
            ViewBag.Login = 1;
            Account user = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            return PartialView(user);
        }
        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}