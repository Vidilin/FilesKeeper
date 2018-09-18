using System;
using System.Collections.Generic;
using System.Text;

namespace FileKeeper.DAL.Models
{
    public class UserFile
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        //public string User { get; set; }
        public int UserId { get; set; }
        public string Hash { get; set; }
        public byte[] Bin { get; set; }
    }
}
