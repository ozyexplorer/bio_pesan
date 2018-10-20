using PesanMakan.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PesanMakan.Business
{
    public class User
    {
        public static DataTable GetUserDataByNIK(string pernr)
        {
            return UserCatalogGet.GetUserDataByNIK(pernr);
        }
    }
}
