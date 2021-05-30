using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Common;
using WebBookStore.Models;

namespace WebBookStore.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            string UserFullName = UserDao.Instance.GetUserFullName(UserDao.Instance.GetUserId());
            ViewBag.UserFullName = UserFullName;
            var user = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            return View(user);
        }

        public ActionResult Edit()
        {
            string UserFullName = UserDao.Instance.GetUserFullName(UserDao.Instance.GetUserId());
            ViewBag.UserFullName = UserFullName;
            var user = UserDao.Instance.ViewDetails(UserDao.Instance.GetUserId());
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Account user)
        {
            if (ModelState.IsValid)
            {
                var result = UserDao.Instance.Update(user);
                if (result)
                {
                    ModelState.AddModelError("", "Cap nhat thanh cong");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.Fail = "Cap nhat khong thanh cong";
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                }
            }
            return View();
        }
        // GET: NewAdmin1/User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "EmailConfirm, ActivationCode")] Register model)
        {
            bool Status = false;
            string Message = "";

            //Model Validation
            if (ModelState.IsValid)
            {
                //username already exists
                if (UserDao.Instance.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "*Tên đăng nhập tồn tại!");
                }
                //email already exists
                else if (UserDao.Instance.CheckUserEmail(model.Email))
                {
                    ModelState.AddModelError("", "*Email đã tồn tại !");
                }
                else
                {
                    var user = new Account();

                    model.ActivationCode = Guid.NewGuid();
                    user.ActivationCode = model.ActivationCode;
                    user.UserName = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Email = model.Email;
                    user.ConfirmEmail = false;
                    user.Name = model.Name;
                    user.Access = true;
                    user.CreatedDate = DateTime.Now;
                    user.GroupID = "CUSTOMER";
                    var result = UserDao.Instance.Insert(user);
                    if (result > 0)
                    {
                        SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                        Message = "Đăng ký thành công. Tài khoản đã được kích hoạt" +
                            "Link kích hoạt đã được gửi qua email của bạn: " + user.Email;
                        Status = true;
                        model = new Register();
                    }
                    else
                    {
                        Message = "Invalid Request";
                    }
                }
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(model);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            UserDao.Instance.ValidateOnSaveEnabled();
            var user = UserDao.Instance.CheckUserByActivationCode(new Guid(id));

            if (user != null)
            {
                user.ConfirmEmail = true;
                UserDao.Instance.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request";
            }

            ViewBag.Status = Status;
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string Email, string activationCode, string emailFor = "VerifyAccount")
        {
            try
            {
                var verifyUrl = "/User/" + emailFor + "/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                var fromEmail = new MailAddress("nguyenhoangkim120201@gmail.com");
                var toEmail = new MailAddress(Email);
                var fromEmailPassword = "0935740126"; // replace actual password
                string subject = "";
                string body = "";
                if (emailFor == "VerifyAccount")
                {
                    subject = "You account is successfully created!";

                    body = "<br/><br/> We are excited to tell you that your Book Store account is " +
                       "Successfully created ^^. Please click on the below link to verify your account" +
                       "<br/><br/><a href =" + link + ">" + link + "</a>";
                }
                else if (emailFor == "ResetPassword")
                {
                    subject = "Reset Password";
                    body = "Hi, <br/><br/>We got request for reset your account password. Please click on the below link to reset" +
                        "<br/><br/><a href =" + link + ">Reset Password link</a>";
                }

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword),
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                })
                    smtp.Send(message);
            }
            catch(Exception)
            {

            }
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //verify email
            ////generate reset password link
            //send email
            string message = "";
            bool status = false;
            var user = UserDao.Instance.GetUserByEmail(Email);
            if (user != null)
            {
                //send email for reset pass
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(user.Email, resetCode, "ResetPassword");
                user.ResetPasswordCode = resetCode;
                //this line i have added here to avoid confirm password not match issue, as we had addded a confirm password property
                //in our model class Account
                UserDao.Instance.ValidateOnSaveEnabled();
                UserDao.Instance.SaveChanges();
                status = true;
                message = "Check your email to reset password";
            }
            else
            {
                message = "Account not found!";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            //verify the reset password link
            //find account associated with this link
            //redirect to view reset pass word
            if (UserDao.Instance.GetUserByResetCode(id) != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var user = UserDao.Instance.GetUserByResetCode(model.ResetCode);
                if (user != null)
                {
                    user.Password = Encryptor.MD5Hash(model.NewPassword);
                    user.ResetPasswordCode = "";
                    UserDao.Instance.ValidateOnSaveEnabled();
                    UserDao.Instance.SaveChanges();
                    message = "New password updated successfully!";
                }
            }
            else
            {
                message = "Something went wrong!";
            }
            ViewBag.Message = message;
            return View(model);
        }

    }
}