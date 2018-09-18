using System;
using System.Collections.Generic;
using System.Text;
using FileKeeper.DAL.Models;
using FileKeeper.DAL.Models.Db;
using System.Linq;
using System.Security.Cryptography;

namespace FileKeeper.DAL.Managers
{
    public class MRFilesManager : DataManagerBase
    {
        private ManagersFacade _mf;

        public MRFilesManager(ManagersFacade _mf)
        {
            this._mf = _mf;
        }

        public IList<UserFile> GetByUser (User user)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                return db.Userfiles.Where(o => o.Userid == user.Id).Select(GetInner).ToList();
            }
        }

        public void AddUserFile (User user, UserFile file)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                SHA256 sha = new SHA256Managed();
                file.Hash = Convert.ToBase64String(sha.ComputeHash(file.Bin));

                var newUserFile = new DbUserfile
                {
                    Userid = user.Id,
                    Hash = file.Hash,
                    Filename = file.Filename,
                    Contenttype = file.ContentType,
                };

                if (db.Files.Any(o => o.Hash == file.Hash))
                {
                    db.Userfiles.Add(newUserFile);
                    db.SaveChanges();
                }
                else
                {
                    DbFile newFile = new DbFile
                    {
                        Hash = file.Hash,
                        Bin = file.Bin,
                    };

                    db.Files.Add(newFile);
                    db.Userfiles.Add(newUserFile);
                    db.SaveChanges();
                }
            }
        }

        public UserFile GetFile(int id, string hash)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                //var file = GetInner(db.Userfiles.SingleOrDefault(o => o.Id == id && o.Hash == hash));

                //file.Bin = 
                return db.Userfiles.Where(o => o.Id == id && o.Hash == hash).Select(obj => new UserFile
                {
                    Id = obj.Id,
                    Filename = obj.Filename,
                    ContentType = obj.Contenttype ?? string.Empty,
                    Hash = obj.Hash,
                    UserId = obj.Userid,
                    Bin = obj.HashNavigation.Bin,
                }).Single();
            }
        }

        public void DelFile(int id, string hash)
        {
            using (var db = GetConnect(_mf.ConnectionString))
            {
                if (!db.Userfiles.Any(o => o.Id == id && o.Hash == hash)) return;

                var delUserFile = db.Userfiles.Single(o => o.Id == id);

                db.Userfiles.Remove(delUserFile);
                db.SaveChanges();

                if (!db.Userfiles.Any(o => o.Hash == hash))
                {
                    var delFile = db.Files.Single(o => o.Hash == hash);
                    db.Remove(delFile);
                    db.SaveChanges();
                }                          
            }
        }

        private UserFile GetInner(DbUserfile obj)
        {
            return new UserFile
            {
                Id = obj.Id,
                Filename = obj.Filename,
                ContentType = obj.Contenttype,
                Hash = obj.Hash,
                UserId = obj.Userid,
            };
        }
    }
}
