using System;
using System.Collections.Generic;

namespace FileKeeper.DAL.Models.Db
{
    public partial class DbFile
    {
        public DbFile()
        {
            Userfiles = new HashSet<DbUserfile>();
        }

        public int Id { get; set; }
        public string Hash { get; set; }
        public byte[] Bin { get; set; }

        public ICollection<DbUserfile> Userfiles { get; set; }
    }
}
