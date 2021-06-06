using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Models
{
    public class BookDao
    {
        BookStoreEntities db = null;

        private static BookDao _Instance;
        public static BookDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BookDao();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }
        public BookDao()
        {
            db = new BookStoreEntities();
        }
        public string Add(Book entity, Author entity1, Category entity2, Publisher entity3)
        {
            db.Books.Add(entity);
            db.Authors.Add(entity1);
            db.Categories.Add(entity2);
            db.Publishers.Add(entity3);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Author entity1, Category entity2)
        {
            db.Books.Add(entity);
            db.Authors.Add(entity1);
            db.Categories.Add(entity2);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Author entity1, Publisher entity3)
        {
            db.Books.Add(entity);
            db.Authors.Add(entity1);
            db.Publishers.Add(entity3);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Category entity2, Publisher entity3)
        {
            db.Books.Add(entity);
            db.Categories.Add(entity2);
            db.Publishers.Add(entity3);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Author entity1)
        {
            db.Books.Add(entity);
            db.Authors.Add(entity1);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Category entity2)
        {
            db.Books.Add(entity);
            db.Categories.Add(entity2);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity, Publisher entity3)
        {
            db.Books.Add(entity);
            db.Publishers.Add(entity3);
            SaveChanges();
            return entity.Id_Book;
        }
        public string Add(Book entity)
        {
            db.Books.Add(entity);
            SaveChanges();
            return entity.Id_Book;
        }
        public void Update(Book entity)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            SaveChanges();
        }
        public void Update(Book entity, Author entity1, Category entity2, Publisher entity3)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Authors.Add(entity1);
            db.Categories.Add(entity2);
            db.Publishers.Add(entity3);
            SaveChanges();
        }
        public void Update(Book entity, Author entity1, Category entity2)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Authors.Add(entity1);
            db.Categories.Add(entity2);
            SaveChanges();
        }
        public void Update(Book entity, Author entity1, Publisher entity3)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Authors.Add(entity1);
            db.Publishers.Add(entity3);
            SaveChanges();
        }
        public void Update(Book entity, Category entity2, Publisher entity3)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Categories.Add(entity2);
            db.Publishers.Add(entity3);
            SaveChanges();
        }
        public void Update(Book entity, Author entity1)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Authors.Add(entity1);
            SaveChanges();
        }
        public void Update(Book entity, Category entity2)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Categories.Add(entity2);
            SaveChanges();
        }
        public void Update(Book entity, Publisher entity3)
        {
            var book = db.Books.Find(entity.Id_Book);
            book.Title = entity.Title;
            book.Images = entity.Images;
            book.Description = entity.Description;
            book.Price = entity.Price;
            book.Quantity = entity.Quantity;
            book.DateExpUpdate = DateTime.Now;
            book.Id_Author = entity.Id_Author;
            book.Id_Category = entity.Id_Category;
            book.Id_Publisher = entity.Id_Publisher;
            db.Publishers.Add(entity3);
            SaveChanges();
        }
        public string GetIdAuthor(string name)
        {
            try
            {
                var entity = db.Authors.Where(x => x.Name == name).FirstOrDefault();
                return entity.Id_Author;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetBookTitle(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Title;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int? GetBookQuantity(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Quantity;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public string GetBookDescription(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Description;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetBookImage(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Images;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int? GetBookPrice(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Price;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public string GetBookAuthor(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Author.Name;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetBookCategory(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Category.NameCatelogy;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetBookPublisher(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Id_Book == name);
                return entity.Publisher.Name;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetIdCategory(string name)
        {
            try
            {
                var entity = db.Categories.Where(x => x.NameCatelogy == name).FirstOrDefault();
                return entity.Id_Category;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string GetIdPublisher(string name)
        {
            try
            {
                var entity = db.Publishers.Where(x => x.Name == name).FirstOrDefault();
                return entity.Id_Publisher;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string CheckCondition(string name)
        {
            try
            {
                var entity = db.Books.SingleOrDefault(x => x.Title == name);
                return entity.Condition.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
        public bool CheckBookId(string name)
        {
            return db.Books.Count(x => x.Id_Book == name) > 0;
        }
        public bool CheckBookTitle(string title)
        {
            return db.Books.Count(x => x.Title == title) > 0;
        }
        public bool CheckAuthorName(string name)
        {
            return db.Books.Count(x => x.Author.Name == name) > 0;
        }
        public bool CheckAuthorId(string name)
        {
            return db.Books.Count(x => x.Author.Id_Author == name) > 0;
        }
        public bool CheckCategoryName(string name)
        {
            return db.Books.Count(x => x.Category.NameCatelogy == name) > 0;
        }
        public bool CheckCategoryId(string name)
        {
            return db.Books.Count(x => x.Category.Id_Category == name) > 0;
        }
        public bool CheckPublisherName(string name)
        {
            return db.Books.Count(x => x.Publisher.Name == name) > 0;
        }
        public bool CheckPublisherId(string name)
        {
            return db.Books.Count(x => x.Publisher.Id_Publisher == name) > 0;
        }
        public bool Remove(string Id)
        {
            try
            {
                var book = db.Books.Find(Id);
                book.Condition = false;
                //db.Books.Remove(book);
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}