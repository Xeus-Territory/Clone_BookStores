using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebBookStore.Models;

namespace WebBookStore.Areas.Admin.Models
{
    public class OrderDao
    {
        BookStoreEntities db = null;

        private static OrderDao _Instance;
        public static OrderDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new OrderDao();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }

        public OrderDao()
        {
            db = new BookStoreEntities();
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

        //public IEnumerable<Order> ListAllPaging(int page, int pageSize)
        //{
        //    return db.Orders.OrderByDescending(x => x.DeliveryDate).ToPagedList(page, pageSize);
        //}

        public int Update(EditOrderModel entity)
        {
            try
            {
                var order = db.Orders.Find(entity.Id_Order);

                //get infor not update
                entity.OrderDate = order.OrderDate;

                DateTime date1 = Convert.ToDateTime(entity.DeliveryDate);
                DateTime date2 = Convert.ToDateTime(entity.ExpDeliveryDate);
                DateTime date3 = order.OrderDate;
                if(DateTime.Compare(date1, date2) >= 0)
                {
                    return -1;
                }
                else if(DateTime.Compare(date1, date3) <= 0)
                {
                    return -2;
                }
                else
                {
                    order.DeliveryDate = entity.DeliveryDate;
                    order.ExpDeliveryDate = entity.ExpDeliveryDate;
                    order.Id_Status = entity.Id_Status;
                    order.Note = entity.Note;
                    SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Delete(int Id_Order)
        {
            try
            {
                Order obj = db.Orders.Find(Id_Order);
                obj.Id_Access = 2;
                SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            //try
            //{
            //    List<OrderDetail> list = db.OrderDetails.ToList();
            //    foreach (var item in list)
            //    {
            //        if (item.id_Order == entity.Id_Order)
            //        {
            //            db.OrderDetails.Remove(item);
            //        }
            //    }
            //    Order obj = db.Orders.Find(entity.Id_Order);
            //    db.Orders.Remove(obj);
            //    SaveChanges();
            //    return true;
            //}
            //catch(Exception)
            //{
            //    return false;
            //}
        }

        public EditOrderModel GetOrderById(int id)
        {
            Order order = db.Orders.Find(id);
            EditOrderModel item = new EditOrderModel
            {
                Id_Customer = order.Id_Customer,
                Id_Order = order.Id_Order,
                DeliveryDate = order.DeliveryDate,
                ExpDeliveryDate = order.ExpDeliveryDate,
                PhoneNumber = order.PhoneNumber,
                AddressShipping = order.AddressShipping,
                Id_Status = order.Id_Status,
                Note = order.Note
            };
            return item;
        }   

        public List<Order> ListAllOrder()
        {
            //return db.Orders.ToList();
            List<Order> orders = db.Orders.ToList();
            List<Order> show = new List<Order>();
            foreach(var item in orders)
            {
                if(item.Id_Access == 1)
                {
                    show.Add(item);
                }
            }
            return show;
        }
    }
}