using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMO_Library
{
    public class SqlDatabase
    {
        public string Name { get; set; }
        public Database Database { get; set; }
        public bool Exists { get; set; }
        public DateTime CreationDateTime()
        {
            if (Exists)
            {
                return Database.CreateDate;
            }
            else
            {
                return new DateTime();
            }
        }


    }
}
