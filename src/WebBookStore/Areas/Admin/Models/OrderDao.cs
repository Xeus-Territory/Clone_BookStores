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

        public bool Update(EditOrderModel entity)
        {
            try
            {
                var order = db.Orders.Find(entity.Id_Order);
                order.DeliveryDate = entity.DeliveryDate;
                order.ExpDeliveryDate = entity.ExpDeliveryDate;
                order.Id_Status = entity.Id_Status;
                order.Note = entity.Note;

                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(EditOrderModel entity)
        {
            try
            {
                List<OrderDetail> list = db.OrderDetails.ToList();
                foreach (var item in list)
                {
                    if (item.id_Order == entity.Id_Order)
                    {
                        db.OrderDetails.Remove(item);
                    }
                }
                Order obj = db.Orders.Find(entity.Id_Order);
                db.Orders.Remove(obj);
                SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
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
            return db.Orders.ToList();
        }
    }
}