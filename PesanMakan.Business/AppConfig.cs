using PesanMakan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PesanMakan.Business
{
    public class CalendarEventObj
    {
        public string id { set; get; }
        public string title { set; get; }
        public string start { set; get; }
        public string menu { set; get; }
        public string startHour { set; get; }
        public string endHour { set; get; }
    }

    public class AppConfig
    {
        public static DateTime GetDateTimeServer()
        {
            return ApplicationCatalogGet.GetDateTimeServer() ?? DateTime.Now;
        }
    }
}
