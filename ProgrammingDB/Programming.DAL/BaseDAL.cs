using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming.DAL
{
    public class BaseDAL
    {
        public ProgrammingDbEntities db;

        public BaseDAL()
        {
            db = new ProgrammingDbEntities();
        }
    }
}
