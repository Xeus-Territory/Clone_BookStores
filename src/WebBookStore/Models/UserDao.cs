using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace WebBookStore.Models
{
    public class UserDao
    {
        static long _Id;
        BookStoreEntities db = null;

        private static UserDao _Instance;
        public static UserDao Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserDao();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }


        public UserDao()
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

        public long Insert(Account entity)
        {
            db.Accounts.Add(entity);
            SaveChanges();
            return entity.Id_Customer;
        }
        public Account GetById(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName == userName);
        }

        public Account GetUserByEmail(string email)
        {
            return db.Accounts.Where(x => x.Email == email).FirstOrDefault();
        }

        public Account ViewDetails(long id)
        {
            return db.Accounts.Find(id);
        }

        public int Login(string UserName, string Password, bool isLoginAdmin = false)
        {
            var result = db.Accounts.SingleOrDefault(x => x.UserName == UserName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == Common.CommonConstant.ADMIN_GROUP)
                    {
                        if (result.Access == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == Password)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                        return -4;
                }
                else
                {
                    if (result.Access == false)
                    {
                        return -1;
                    }
                    else if (result.Password == Password)
                    {
                        if (result.ConfirmEmail == false)
                        {
                            return -3;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        return -2;
                    }
                }


            }
        }
        public List<string> GetListCredentials(string userName)
        {
            var user = db.Accounts.SingleOrDefault(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }


        public bool CheckUserName(string userName)
        {
            return db.Accounts.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return db.Accounts.Count(x => x.Email == email) > 0;
        }

        public Account CheckUserByActivationCode(Guid code)
        {
            return db.Accounts.Where(x => x.ActivationCode == code).FirstOrDefault();
        }
        public void RemoveUser(Account ac)
        {
            db.Accounts.Remove(ac);
            SaveChanges();
        }
        public bool Update(Account entity)
        {
            try
            {
                var user = db.Accounts.Find(entity.Id_Customer);
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                user.FavoriteCategory = entity.FavoriteCategory;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SaveUserId(long id)
        {
            _Id = id;
        }
        public long GetUserId()
        {
            return _Id;
        }

        public void ValidateOnSaveEnabled()
        {
            db.Configuration.ValidateOnSaveEnabled = false;
        }

        public Account GetUserByResetCode(string code)
        {
            Account user = new Account();
            List<Account> list = db.Accounts.ToList();
            foreach (Account item in list)
            {
                if (item.ResetPasswordCode == code)
                {
                    user = item;
                }
            }
            return user;
        }

        public string GetUserFullName(long id)
        {
            return ViewDetails(id).Name.ToString();
        }

    }
}