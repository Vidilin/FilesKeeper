using System;
using System.Collections.Generic;
using System.Text;
using FileKeeper.DAL.Managers;

namespace FileKeeper.DAL
{
    public class ManagersFacade
    {
        public string ConnectionString { get; }
        public MRUsersManager MRUserManager;
        public MRFilesManager MRFilesManager;

        public ManagersFacade(string connectionString)
        {
            ConnectionString = connectionString;
            MRUserManager = new MRUsersManager(this);
            MRFilesManager = new MRFilesManager(this);
        }
    }
}
