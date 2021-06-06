using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBookStore.Models;
using WebBookStore.Areas.Admin.Models;
using WebBookStore.Common;

namespace WebBookStore.Areas.Admin.Controllers
{
    public class ManageBookController : Controller
    {
        // GET: Admin/ManageBook
        string message = "";
        // GET: ManageBook
        BookStoreEntities db = new BookStoreEntities();

        [HasCredential(RoleID = "VIEW_BOOK")]
        public ActionResult Index(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(db.Books.ToList().OrderBy(n => n.Title).Where(n => n.Condition == true).ToPagedList(pagenumber, pagesize));
        }

        [HasCredential(RoleID = "VIEW_BOOK")]
        public ActionResult HomePage(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            return View(db.Books.ToList().OrderBy(n => n.Title).Where(n => n.Condition == true).ToPagedList(pagenumber, pagesize));
        }

        [HasCredential(RoleID = "DELETE_BOOK")]
        public ActionResult Remove(string ID_Book)
        {
            var result = BookDao.Instance.Remove(ID_Book);
            if (!result)
            {
                message = "*Sách không tồn tại !";
                SetAlert(message);
                return RedirectToAction("Index");
            }
            else
            {
                message = "*Xóa sách thành công !";
                SetAlert(message);
                return RedirectToAction("Index");
            }
        }

        [HasCredential(RoleID = "UPDATE_BOOK")]
        public ActionResult Update(string ID_Book)
        {
            Book book = db.Books.SingleOrDefault(n => n.Id_Book == ID_Book);
            if (book == null)
            {
                message = "*Sách không tồn tại !";
                SetAlert(message);
                return RedirectToAction("Index");
            }
            else return View(book);
        }
        public string GetCateNameByID(Book entity)
        {
            var listCate = db.Categories.ToList();
            string NameCategory = "";
            foreach(var item in listCate)
            {
                if(entity.Category.NameCatelogy == item.Id_Category)
                {
                    NameCategory = item.NameCatelogy;
                }
            }
            return NameCategory;
        }
        public string GetCateNameByForm(string id)
        {
            var listCate = db.Categories.ToList();
            string NameCategory = "";
            foreach(var item in listCate)
            {
                if(id == item.Id_Category)
                {
                    NameCategory = item.NameCatelogy;
                }
            }
            return NameCategory;
        }
        [HasCredential(RoleID = "UPDATE_BOOK")]
        [HttpPost]
        public ActionResult Update(Book entity)
        {
            Author entity1 = new Author();
            Category entity2 = new Category();
            Publisher entity3 = new Publisher();
            if (ModelState.IsValid)
            {
                string quantity = entity.Quantity.ToString();
                string price = entity.Price.ToString();
                entity1.Name = entity.Author.Name;
                entity2.NameCatelogy = GetCateNameByID(entity);
                entity3.Name = entity.Publisher.Name;
                if (entity.Title == BookDao.Instance.GetBookTitle(entity.Id_Book) && entity.Images == BookDao.Instance.GetBookImage(entity.Id_Book) &&
                    entity.Price == BookDao.Instance.GetBookPrice(entity.Id_Book) && entity.Description == BookDao.Instance.GetBookDescription(entity.Id_Book) &&
                    entity1.Name == BookDao.Instance.GetBookAuthor(entity.Id_Book) && entity2.NameCatelogy == BookDao.Instance.GetBookCategory(entity.Id_Book) &&
                    entity3.Name == BookDao.Instance.GetBookPublisher(entity.Id_Book) && entity.Quantity == BookDao.Instance.GetBookQuantity(entity.Id_Book))
                {
                    message = "*Sách đã tồn tại !";
                    SetAlert(message);
                    return View(entity);
                }
                else
                {
                    if (entity.Description == null || entity.Title == null || quantity == null || entity1.Name == null
                            || entity2.NameCatelogy == null || entity3.Name == null || price == null)
                    {
                        message = "*Chưa đủ thông tin !";
                        SetAlert(message);
                        return View(entity);
                    }
                    else
                    {
                        if (checkNumber(quantity) == false || checkNumber(price) == false)
                        {
                            message = "*Sai kiểu dữ liệu !";
                            SetAlert(message);
                            return View(entity);
                        }
                        else
                        {
                            entity.Quantity = Convert.ToInt32(quantity);
                            entity.Price = Convert.ToInt32(price);
                            bool status1 = BookDao.Instance.CheckAuthorName(entity1.Name);
                            bool status2 = BookDao.Instance.CheckCategoryName(entity2.NameCatelogy);
                            bool status3 = BookDao.Instance.CheckPublisherName(entity3.Name);
                            switch (status1)
                            {
                                case true:
                                    {
                                        switch (status2)
                                        {
                                            case true:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                BookDao.Instance.Update(entity);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                        case false:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Update(entity, entity3);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                    }
                                                    break;
                                                }
                                            case false:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity2.Id_Category = entity.Id_Category;
                                                                BookDao.Instance.Update(entity, entity2);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                        case false:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                entity2.Id_Category = entity.Id_Category;
                                                                BookDao.Instance.Update(entity, entity2, entity3);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case false:
                                    {
                                        switch (status2)
                                        {
                                            case true:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                BookDao.Instance.Update(entity, entity1);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                        case false:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Update(entity, entity1, entity3);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                    }
                                                    break;
                                                }
                                            case false:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity2.Id_Category = entity.Id_Category;
                                                                BookDao.Instance.Update(entity, entity1, entity2);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                        case false:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                entity2.Id_Category = entity.Id_Category;
                                                                entity1.Id_Author = entity.Id_Author;
                                                                BookDao.Instance.Update(entity, entity1, entity2, entity3);
                                                                message = "*Cập nhật thành công !";
                                                                SetAlert(message);
                                                                return View(entity);
                                                            }
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            else
            {
                message = "*Cập nhật thất bại !";
                SetAlert(message);
                return View(entity);
            }
            return View(entity);
        }
        public static bool checkNumber(String s)
        {
            int b = 0;
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(s);
            foreach (char a in ASCIIValues)
            {
                if (a < 48 || a > 57)
                {
                    return false;
                }
                else
                {
                    if (a == 48)
                    {
                        b += 0;
                    }
                    else b += a;
                }
            }
            if (b == 0) return false;
            return true;
        }
        public string randomID()
        {
            Random rd = new Random();
            int id = rd.Next(1, 999999999);
            return id.ToString();
        }

        [HasCredential(RoleID = "ADD_BOOK")]
        public ActionResult Add(FormCollection f)
        {
            Book entity = new Book();
            Author entity1 = new Author();
            Category entity2 = new Category();
            Publisher entity3 = new Publisher();
            if (ModelState.IsValid)
            {
                entity.Condition = true;
                entity.DateExpUpdate = DateTime.Now;
                entity.Title = f["Title"].ToString();
                entity.Images = f["Image"].ToString();
                entity.Description = f["Description"].ToString();
                string quantity = f["Quantity"].ToString();
                string price = f["Price"].ToString();
                entity1.Name = f["Author"].ToString();
                entity2.NameCatelogy = GetCateNameByForm(f["Category"].ToString());
                entity3.Name = f["Publisher"].ToString();

                if (entity.Description == "" || entity.Title == "" || quantity == "" || entity1.Name == "" || entity2.NameCatelogy == "" || entity3.Name == "" || price == "")
                {
                    message = "*Chưa đủ thông tin !";
                    SetAlert(message);
                    return RedirectToAction("Index");
                }
                else
                {
                    if (BookDao.Instance.CheckBookTitle(entity.Title) && BookDao.Instance.CheckAuthorName(entity1.Name) && BookDao.Instance.CheckPublisherName(entity3.Name))
                    {
                        if (BookDao.Instance.CheckCondition(entity.Title) == "true")
                        {
                            message = "*Sách đã tồn tại trong CSDL !";
                            SetAlert(message);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            message = "*Sách đã tồn tại !";
                            SetAlert(message);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        if (checkNumber(quantity) == false || checkNumber(price) == false)
                        {
                            message = "*Sai kiểu dữ liệu !";
                            SetAlert(message);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            do
                            {
                                entity.Id_Book = randomID();
                            }
                            while (BookDao.Instance.CheckBookId(entity.Id_Book));
                            entity.Quantity = Convert.ToInt32(quantity);
                            entity.Price = Convert.ToInt32(price);
                            bool status1 = BookDao.Instance.CheckAuthorName(entity1.Name);
                            bool status2 = BookDao.Instance.CheckCategoryName(entity2.NameCatelogy);
                            bool status3 = BookDao.Instance.CheckPublisherName(entity3.Name);
                            switch (status1)
                            {
                                case true:
                                    {
                                        switch (status2)
                                        {
                                            case true:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                BookDao.Instance.Add(entity);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                        case false:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Add(entity, entity3);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                    }
                                                    break;
                                                }
                                            case false:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity2.Id_Category = entity.Id_Category;
                                                                BookDao.Instance.Add(entity, entity2);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                        case false:
                                                            {
                                                                entity.Id_Author = BookDao.Instance.GetIdAuthor(entity1.Name);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity2.Id_Category = entity.Id_Category;
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Add(entity, entity2, entity3);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case false:
                                    {
                                        switch (status2)
                                        {
                                            case true:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                entity1.Id_Author = entity.Id_Author;
                                                                BookDao.Instance.Add(entity, entity1);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                        case false:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                entity.Id_Category = BookDao.Instance.GetIdCategory(entity2.NameCatelogy);
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Add(entity, entity1, entity3);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                    }
                                                    break;
                                                }
                                            case false:
                                                {
                                                    switch (status3)
                                                    {
                                                        case true:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                entity.Id_Publisher = BookDao.Instance.GetIdPublisher(entity3.Name);
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity2.Id_Category = entity.Id_Category;
                                                                BookDao.Instance.Add(entity, entity1, entity2);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                        case false:
                                                            {
                                                                do
                                                                {
                                                                    entity.Id_Author = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckAuthorId(entity.Id_Author));
                                                                do
                                                                {
                                                                    entity.Id_Category = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckCategoryId(entity.Id_Category));
                                                                do
                                                                {
                                                                    entity.Id_Publisher = randomID();
                                                                }
                                                                while (BookDao.Instance.CheckPublisherId(entity.Id_Publisher));
                                                                entity1.Id_Author = entity.Id_Author;
                                                                entity2.Id_Category = entity.Id_Category;
                                                                entity3.Id_Publisher = entity.Id_Publisher;
                                                                BookDao.Instance.Add(entity, entity1, entity2, entity3);
                                                                message = "*Thêm sách thành công !";
                                                                SetAlert(message);
                                                                return RedirectToAction("Index");
                                                            }
                                                    }
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }
            else
            {
                message = "*Thêm sách thất bại !";
                SetAlert(message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public void SetAlert(string message)
        {
            TempData["AlertMessage"] = message;
        }
    }
}