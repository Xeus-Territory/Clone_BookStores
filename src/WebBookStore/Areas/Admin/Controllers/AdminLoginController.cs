using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Common;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(WebBookStore.Models.Login model)
        {
            if (ModelState.IsValid)
            {
                var result = UserDao.Instance.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = UserDao.Instance.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserId = user.Id_Customer;

                    var listCredentials = UserDao.Instance.GetListCredentials(userSession.UserName);

                    UserDao.Instance.SaveUserId(userSession.UserId);

                    Session.Add(CommonConstant.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstant.USER_SESSION, userSession);

                    return RedirectToAction("Index", "HomePage");
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
                else if (result == -3)
                {
                    ModelState.AddModelError("", "*Tài khoản chưa xác nhận Email !");

                }
                else if (result == -4)
                {
                    ModelState.AddModelError("", "*Tài khoản không có quyền truy cập trang Admin !");
                }
                else
                {
                    ModelState.AddModelError("", "*Tài khoản hoặc mật khẩu không đúng !");
                }
            }
            return View("Index");
        }
    }
}