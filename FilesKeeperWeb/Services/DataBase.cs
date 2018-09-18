using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileKeeper.DAL;

namespace FilesKeeperWeb.Services
{
    public class DataBase : ManagersFacade
    {
        public DataBase(string connectionString) : base (connectionString)
        {

        }
    }
}
