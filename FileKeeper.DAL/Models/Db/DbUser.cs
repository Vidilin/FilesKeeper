using System;
using System.Collections.Generic;

namespace FileKeeper.DAL.Models.Db
{
    public partial class DbUser
    {
        public DbUser()
        {
            Userfiles = new HashSet<DbUserfile>();
        }

        public int Id { get; set; }
        public string Login { get; set; }

        public ICollection<DbUserfile> Userfiles { get; set; }
    }
}
