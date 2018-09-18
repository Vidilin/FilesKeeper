using System;
using System.Collections.Generic;

namespace FileKeeper.DAL.Models.Db
{
    public partial class DbUserfile
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Filename { get; set; }
        public string Contenttype { get; set; }
        public string Hash { get; set; }

        public DbFile HashNavigation { get; set; }
        public DbUser User { get; set; }
    }
}
