using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FileKeeper.DAL.Managers
{
    public abstract class DataManagerBase
    {
        //protected readonly string _connectionString;
        protected FKDataBaseContext GetConnect(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FKDataBaseContext>();
            var options = optionsBuilder
                .UseNpgsql(connectionString)
                .Options;
            return new FKDataBaseContext(options);
        }
    }
}
