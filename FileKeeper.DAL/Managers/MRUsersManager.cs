using System;
using System.Collections.Generic;
using System.Text;
using FileKeeper.DAL.Models.Db;
using FileKeeper.DAL.Models;
using System.Linq;

namespace FileKeeper.DAL.Managers
{
    public class MRUsersManager : DataManagerBase
    {
        private ManagersFacade _mf;

        public MRUsersManager(ManagersFacade _mf)
        {
            this._mf = _mf;
        }

        public User Get (int id)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                return db.Users.Where(o => o.Id == id).Select(GetInner).SingleOrDefault();
            }
        }

        public User GetUser (string login)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                if (db.Users.Any(o => o.Login == login))
                {
                    return db.Users.Where(o => o.Login == login).Select(GetInner).Single();
                }
                else
                {
                    var newUser = new DbUser { Login = login };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return GetInner(newUser);
                }
            }
        }

        public IList<User> GetAll()
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                return db.Users.Select(GetInner).ToList();
            }
        }

        private User GetInner(DbUser obj)
        {
            return new User
            {
                Id = obj.Id,
                Login = obj.Login,
            };
        }
    }
}
