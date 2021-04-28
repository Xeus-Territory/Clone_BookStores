using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookStore.Models
{
    public class Cart
    {
        BookStoreEntities db = new BookStoreEntities();
        public string sID_Book { get; set; }
        public string sTitle { get; set; }
        public string sAuthor { get; set; }
        public int sQuantity { get; set; }
        public string sImages { get; set; }
        public int sPrice { get; set; }
        public int Total
        {
            get { return sQuantity * sPrice; }
        }
        // Ham tao gio hang
        public Cart(string id_book)
        {
            sID_Book = id_book;
            Book book = db.Books.Single(x => x.Id_Book == sID_Book);
            sTitle = book.Title;
            sAuthor = book.Author.Name;
            sImages = book.Images;
            sPrice = (int)book.Price;
            sQuantity = 1;
        }
    }
}