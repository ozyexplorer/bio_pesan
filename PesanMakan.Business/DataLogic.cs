using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Data;
using PesanMakan.Data;

namespace PesanMakan.Business
{
    public class DataLogic
    {
        public static string data(string str1, string str2, string str3)
        {
            string cardUID = str1;
            string CardValue = str2;
            string modifier = str3;

            string status;
            status = InsertData.insert(cardUID, CardValue, modifier);

            return status;
        }
        public static void insertMenuDB(string[][] array)
        {
            
            InsertData.insertMenu(array);
        }

        public static void InsertGambar(string namagambar,string tanggal)
        {
            InsertData.insertgambar(namagambar, tanggal);
        }

        public static string getGambar(string tanggal)
        {
            string gambar = GetData.GetGambar(tanggal);
            return gambar; 
        }
        

        public static string GetUserNumberByEmail(string email)
        {
            string nik = GetData.GetNik(email).Rows[0]["PERNR"].ToString();
            //table.Rows[i]["DATES"].ToString();
            return nik;
        }

        //insert data user ke database
        public static void inserttoDB(string nik, string nama, string[][] array, string modifier)
        {
            int n = array.Length;
            int i;
            string[] token = new string[n];
            Random rnd = new Random();
            
            for (i = 0; i < n; i++)
            {
                token[i] = Code.CreateCode((i + 1), 4) + rnd.Next(10, 99);
            }
            InsertData.insertDB(nik, nama, token, array, modifier);
        }

        //get datatable for menu in admin
        public static DataTable GetAllMenu(string Period)
        {
            StringBuilder _table = new StringBuilder();
            DataTable dt = new DataTable("MENU");
            DataTable table = GetData.MenuCheckDB(Period);
            StringBuilder jamMulai = new StringBuilder();
            StringBuilder jamSelesai = new StringBuilder();
            string Menu;
            string Hari = "";
            string On_Off;
            string note;
            string gambar;
            string preview;
            string[] prSplit = Period.Split('-') ;
            int y = Convert.ToInt32(prSplit[0]);
            int m = Convert.ToInt32(prSplit[1]);

            dt.Columns.Add(("Tanggal"));
            dt.Columns.Add(("Hari"));
            dt.Columns.Add(("Menu_Makan"));
            dt.Columns.Add(("Gambar"));
            dt.Columns.Add(("Preview"));
            dt.Columns.Add(("Jam_Mulai"));
            dt.Columns.Add(("Jam_Akhir"));
            dt.Columns.Add(("OnOff_Day"));
            dt.Columns.Add(("Note"));
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    jamMulai.Clear();
                    jamSelesai.Clear();

                    
                    //jamMulai = "<input type='text' id='Start_H' class='timepicker form-control m-bot10' value='" + table.Rows[i]["START_H"].ToString() + "'>";
                    //jamSelesai= "<input type='text' id='End_H' class='timepicker form-control m-bot10' value='" + table.Rows[i]["END_H"].ToString() + "'>";
                    if (table.Rows[i]["ON_OFF"].ToString().Equals("On") && table.Rows[i]["GAMBAR"].ToString()=="")
                    {
                        jamMulai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                      + "<input type='text' class='form-control' value='" + table.Rows[i]["START_H"].ToString() + "'>"
                      + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        jamSelesai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input type='text' class='form-control' value='" + table.Rows[i]["END_H"].ToString() + "'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon -time'></span> </span> </div> </div>");
                        On_Off = "<select class='form-control m-bot10 ddlOnOff'><option value='On' selected='selected'>On</option><option value='Off'>Off</option></select>";
                        note = "<input type='text' class='form-control m-bot13' value='" + table.Rows[i]["NOTE"].ToString() + "'>";
                        gambar = "<button type='button' class='btn btn-info btn-sm' id='myBtn' data-toggle='modal' data-target='#myModal'>Upload</button>";
                        preview = "<p>Tidak Ada Gambar</p>";
                        Menu = "<input type='text' class='form-control m-bot15' value='" + table.Rows[i]["MENU"].ToString() + "'>";
                    }
                    else if (table.Rows[i]["ON_OFF"].ToString().Equals("On") && table.Rows[i]["GAMBAR"].ToString() != "")
                    {
                        jamMulai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                      + "<input type='text' class='form-control' value='" + table.Rows[i]["START_H"].ToString() + "'>"
                      + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        jamSelesai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input type='text' class='form-control' value='" + table.Rows[i]["END_H"].ToString() + "'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        On_Off = "<select class='form-control m-bot10 ddlOnOff'><option value='On' selected='selected'>On</option><option value='Off'>Off</option></select>";
                        note = "<input type='text' class='form-control m-bot13' value='" + table.Rows[i]["NOTE"].ToString() + "'>";
                        gambar = "<button type='button' class='btn btn-info btn-sm' id='myBtn' data-toggle='modal' data-target='#myModal'>Change</button>";
                        preview = "<img src='../Images/" + table.Rows[i]["GAMBAR"].ToString() + "' alt='Not Found' width='75' height='75'>";
                        Menu = "<input type='text' class='form-control m-bot15' value='" + table.Rows[i]["MENU"].ToString() + "'>";
                    }
                    else
                    {
                        //disable all input
                        jamMulai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input disabled type='text' class='form-control' value='" + table.Rows[i]["START_H"].ToString() + "'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        jamSelesai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input disabled type='text' class='form-control' value='" + table.Rows[i]["END_H"].ToString() + "'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        On_Off = "<select class='form-control m-bot10 ddlOnOff'><option value='On'>On</option><option value='Off' selected='selected'>Off</option></select>";
                        note = "<input disabled type='text' class='form-control m-bot13' value='" + table.Rows[i]["NOTE"].ToString() + "'>";
                        gambar = "<button disabled type='button' class='btn btn-info btn-sm' id='myBtn' data-toggle='modal' data-target='#myModal'>Upload</button>";
                        preview = "<p>Tidak Ada Gambar</p>";
                        Menu = "<input disabled type='text' class='form-control m-bot15' value='" + table.Rows[i]["MENU"].ToString() + "'>";
                    }

                    if(table.Rows[i]["DAY"].ToString() == "Sunday"){
                        Hari = "Minggu";
                    }else if (table.Rows[i]["DAY"].ToString() == "Monday"){
                        Hari = "Senin";
                    }else if (table.Rows[i]["DAY"].ToString() == "Tuesday"){
                        Hari = "Selasa";
                    }else if (table.Rows[i]["DAY"].ToString() == "Wednesday"){
                        Hari = "Rabu";
                    }else if (table.Rows[i]["DAY"].ToString() == "Thursday"){
                        Hari = "Kamis";
                    }else if (table.Rows[i]["DAY"].ToString() == "Friday"){
                        Hari = "Jum'at";
                    }else if (table.Rows[i]["DAY"].ToString() == "Saturday"){
                        Hari = "Sabtu";
                    }
                   


                    object[] obj = new object[9];

                    obj[0] = table.Rows[i]["DATES"].ToString();
                    obj[1] = Hari;
                    obj[2] = Menu;
                    obj[3] = gambar;
                    obj[4] = preview;
                    obj[5] = jamMulai;
                    obj[6] = jamSelesai;
                    obj[7] = On_Off;
                    obj[8] = note;
                    dt.Rows.Add(obj);
                }
            }else{
                for (int i = 0; i < DateTime.DaysInMonth(y,m); i++)
                {
                    DateTime date = DateTime.Parse(Period + "-" + (i + 1));
                    DayOfWeek dow = date.DayOfWeek; //enum
                    string str = dow.ToString(); //string
                    string _action = GetAction();

                    jamMulai.Clear();
                    jamSelesai.Clear();

                    if(str.Equals("Sunday") || str.Equals("Saturday"))
                    {
                        jamMulai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input disabled type='text' class='form-control' value='12:00'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        jamSelesai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input disabled type='text' class='form-control' value='13:00'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        On_Off = "<select class='form-control m-bot10 ddlOnOff'><option value='On'>On</option><option value='Off' selected='selected'>Off</option></select>";
                        note = "<input disabled type='text' class='form-control m-bot13' >";
                        gambar = "<button disabled type='button' class='btn btn-info btn-sm' id='myBtn' data-toggle='modal' data-target='#myModal'>Upload</button>";
                        preview = "<p>Tidak Ada Gambar</p>";
                        Menu = "<input disabled type='text' class='form-control m-bot15' >";
                    }
                    else
                    {
                        jamMulai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                        + "<input type='text' class='form-control' value='12:00'>"
                        + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        jamSelesai.Append("<div class='input-group clockpicker ' data-placement='bottom' data-align='top' data-autoclose='true'>"
                          + "<input type='text' class='form-control' value='13:00'>"
                          + "<span class='input-group-addon'> <span class='glyphicon glyphicon-time'></span> </span> </div> </div>");
                        On_Off = "<select class='form-control m-bot10 ddlOnOff'><option value='On' selected='selected'>On</option><option value='Off'>Off</option></select>";
                        note = "<input type='text' class='form-control m-bot13' >";
                        gambar = "<button type='button' class='btn btn-info btn-sm' id='myBtn' data-toggle='modal' data-target='#myModal'>Upload</button>";
                        preview = "<p>Tidak Ada Gambar</p>";
                        Menu = "<input type='text' class='form-control m-bot15' >";
                    }
                    object[] obj = new object[9];


                    if (str == "Sunday")
                    {
                        Hari = "Minggu";
                    }
                    else if (str == "Monday")
                    {
                        Hari = "Senin";
                    }
                    else if (str == "Tuesday")
                    {
                        Hari = "Selasa";
                    }
                    else if (str == "Wednesday")
                    {
                        Hari = "Rabu";
                    }
                    else if (str == "Thursday")
                    {
                        Hari = "Kamis";
                    }
                    else if (str == "Friday")
                    {
                        Hari = "Jum'at";
                    }
                    else if (str == "Saturday")
                    {
                        Hari = "Sabtu";
                    }

                    obj[0] = date.ToString("yyyy/MM/dd");
                    obj[1] = Hari;
                    obj[2] = Menu;
                    obj[3] = gambar;
                    obj[4] = preview;
                    obj[5] = jamMulai;
                    obj[6] = jamSelesai;
                    obj[7] = On_Off;
                    obj[8] = note;
                    dt.Rows.Add(obj);
                }
            }
            

            return dt;

        }

        public static DataTable RecapOrder(string Period)
        {

            //StringBuilder _table = new StringBuilder();
            //DataTable dt = new DataTable("MENU");
            DataTable table = GetData.GetRecapOrderDB(Period);
            
            return table;

        }

        public static int TotalMember()
        {
            int totalmember = GetData.TotalMember();
            return totalmember;
        }

        public static int TotalBooking()
        {
            int totalboking = GetData.TotalBooking();
            return totalboking;
        }

        public static int TotalKonfirmasi()
        {
            int totalkonfirmasi = GetData.TotalKonfirmasi();
            return totalkonfirmasi;
        }


        public static DataTable Reconcile()
        {
            DataTable dt = new DataTable("RECONCILE");
            DataTable table = GetData.GetReconcile();
            string Hari = "";
            dt.Columns.Add(("Tanggal"));
            dt.Columns.Add(("Hari"));
            dt.Columns.Add(("Token"));
            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("Nama"));
            dt.Columns.Add(("TimeIn"));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string Period = table.Rows[i]["BOKDA"].ToString();
                DateTime date = DateTime.Parse(Period);
                DayOfWeek dow = date.DayOfWeek; //enum
                string str = dow.ToString(); //string

                if (str == "Sunday")
                {
                    Hari = "Minggu";
                }
                else if (str == "Monday")
                {
                    Hari = "Senin";
                }
                else if (str == "Tuesday")
                {
                    Hari = "Selasa";
                }
                else if (str == "Wednesday")
                {
                    Hari = "Rabu";
                }
                else if (str == "Thursday")
                {
                    Hari = "Kamis";
                }
                else if (str == "Friday")
                {
                    Hari = "Jum'at";
                }
                else if (str == "Saturday")
                {
                    Hari = "Sabtu";
                }

                object[] obj = new object[6];

                obj[0] = table.Rows[i]["BOKDAS"].ToString();
                obj[1] = Hari;
                obj[2] = table.Rows[i]["TOKEN"].ToString();
                obj[3] = table.Rows[i]["NIK"].ToString(); 
                obj[4] = table.Rows[i]["NAMA"].ToString(); 
                obj[5] = table.Rows[i]["TIMEIN"].ToString();
               
                dt.Rows.Add(obj);
            }
            return dt;

        }
        public static string CheckToken(string token)
        {
            string status = GetData.CheckToken(token);
            return status;
        }
        protected static string GetAction()
        {
            StringBuilder element = new StringBuilder();

            element.Append("<input type='button' value='Edit' style='text-align:center; width:50px; margin-right:5px;'  onclick='Edit(this)' class='btn btn-info btn-xs'></input>");
            element.Append("<input type='button' value='Delete' style='text-align:center; width:50px;'  onclick='deleteRow(this)' class='btn btn-danger btn-xs'></input>");
            return element.ToString();
        }

        //DeleteMenu
        public static void DeleteMenu(string tgl, string menu)
        {
            InsertData.DelMenuDB(tgl, menu);
        }
        //AddMenu
        public static void TambahMenu(string tgl, string menu, string jam)
        {
            InsertData.AddMenuDB(tgl, menu, jam);
        }
        public static void MenuCheck(string Period)
        {
            GetData.MenuCheckDB(Period);
        }

        //fungsi untuk return nama sesuai nik masukan
        public static string CariNik(string nik)
        {

            DataTable table = GetData.SearchNikDB(nik);
            string nama;
            int row = table.Rows.Count;
            if (row > 0)
            {
                nama = table.Rows[0]["CNAME"].ToString();
            }
            else
            {
                nama = " ";
            }


            return nama;
        }
        public static int RegKartu(string nik, string nama, string cardid, string role)
        {

            int stat = InsertData.RegCardDB(nik, nama, cardid, role);
            return stat;
        }
        public static void RegAnggota(string nik, string nama)
        {

            InsertData.RegMemberDB(nik, nama);
        }

        public static void ResetData(string nik)
        {
            InsertData.ResetDataDB(nik);
        }
        
        public static DataTable GetAllUserData()
        {
            DataTable dt = new DataTable("DATA"); ;
            StringBuilder _table = new StringBuilder();
            DataTable table = GetData.GetUserDataDB();

            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("NAMA"));
            dt.Columns.Add(("CardID"));
            dt.Columns.Add(("ROLE"));
            dt.Columns.Add(("Action"));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = GetAction();
                //int _yearRange = Convert.ToInt32((Convert.ToDateTime(table.Rows[i]["EXPDT"]) - Convert.ToDateTime(table.Rows[i]["EFFDT"])).TotalDays / 365);
                object[] obj = new object[5];

                //obj[0] = _status;
                obj[0] = table.Rows[i]["PERNR"].ToString();
                obj[1] = table.Rows[i]["CNAME"].ToString();
                obj[2] = table.Rows[i]["IDCard"].ToString();
                obj[3] = table.Rows[i]["ROLE"].ToString();
                obj[4] = _action;
                //if (table.Rows[i]["ISDELETED"].ToString().Equals("0"))
                //{
                    dt.Rows.Add(obj);
                //}
               
            }

            return dt;
        }


        public static DataTable GetSearchUserData(string nama)
        {
            DataTable dt = new DataTable("DATA"); ;
            StringBuilder _table = new StringBuilder();
            DataTable table = GetData.GetSearchDataNama(nama);

            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("NAMA"));
            dt.Columns.Add(("CardID"));
            dt.Columns.Add(("ROLE"));
            dt.Columns.Add(("Action"));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = GetAction();
                object[] obj = new object[5];

                obj[0] = table.Rows[i]["PERNR"].ToString();
                obj[1] = table.Rows[i]["CNAME"].ToString();
                obj[2] = table.Rows[i]["IDCard"].ToString();
                obj[3] = table.Rows[i]["ROLE"].ToString();
                obj[4] = _action;
                dt.Rows.Add(obj);
                

            }

            return dt;
        }
        //Delete USer Data
        public static void HapusData(string nik, string nama, string cardid, string role)
        {
            InsertData.DelDataDB(nik, nama, cardid, role);
        }
        public static void DeleteOrder(string tgl, string nik, string nama)
        {
            string[] tanggalsplit = tgl.Split('/');
            string[] tanggalthn = tanggalsplit[2].Split(' ');
            string tanggalfix = tanggalthn[0] + '-' + tanggalsplit[1] + '-' + tanggalsplit[0];
            InsertData.DelOrder(tanggalfix, nik, nama);
        }

        public static string[] LoginCard(string cardUID)
        {
            string role;
            string[] data = new string[3];
            DataTable d = GetData.LoginCardDB(cardUID);
            if (d.Rows.Count != 0)
            {
                data[0] = d.Rows[0]["NIK"].ToString();
                data[1] = d.Rows[0]["NAMA"].ToString();
                data[2] = d.Rows[0]["ROLE"].ToString();
                role = Cryptography.Encrypt(data[2], true);
                return data;
            }
            else
            {
                data[0] = "";
                data[1] = "";
                data[2] = "";
                return data;
            }


        }
        public static string[] LoginUserName(string nik)
        {
            string role;
            string[] data = new string[3];
            DataTable d = GetData.LoginUserNameDB(nik);
            if (d.Rows.Count != 0)
            {
                data[0] = d.Rows[0]["NIK"].ToString();
                data[1] = d.Rows[0]["NAMA"].ToString();
                data[2] = d.Rows[0]["ROLE"].ToString();
                role = Cryptography.Encrypt(data[2], true);
                return data;
            }
            else
            {
                data[0] = "";
                data[1] = "";
                data[2] = "";
                return data;
            }


        }
        public static DataTable GetOrderByUser(string User, String Nik)
        {
            StringBuilder _table = new StringBuilder();
            DataTable dt = new DataTable("ORDER");
            //DataTable table = GetData.GetOrderByUser(User, Nik);
            DataTable table = GetData.GetListOrder(Nik);
            StringBuilder element = new StringBuilder();
            string Hari = "";
            dt.Columns.Add(("Tanggal_Makan"));
            dt.Columns.Add(("Hari"));
            dt.Columns.Add(("Menu_Makan"));
            dt.Columns.Add(("Jam_Mulai"));
            dt.Columns.Add(("Jam_Akhir"));
            dt.Columns.Add(("Status_Makan"));
            dt.Columns.Add(("Jam_Makan"));
            //string _Action = "<input type='button' value='Delete Order' style='text-align:center; padding:3px;'  onclick='deleteRow(this)' class='btn btn-danger btn-xs'></input>";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i]["DAY"].ToString() == "Sunday")
                {
                    Hari = "Minggu";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Monday")
                {
                    Hari = "Senin";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Tuesday")
                {
                    Hari = "Selasa";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Wednesday")
                {
                    Hari = "Rabu";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Thursday")
                {
                    Hari = "Kamis";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Friday")
                {
                    Hari = "Jum'at";
                }
                else if (table.Rows[i]["DAY"].ToString() == "Saturday")
                {
                    Hari = "Sabtu";
                }


                object[] obj = new object[7];

                obj[0] = table.Rows[i]["BOKDAS"].ToString();
                obj[1] = Hari;
                obj[2] = table.Rows[i]["MENU"].ToString();
                obj[3] = table.Rows[i]["START_H"].ToString();
                obj[4] = table.Rows[i]["END_H"].ToString();
                obj[5] = table.Rows[i]["FLAGEAT"].ToString() == "True" ? "Telah Konfirmasi" : "Belum Konfirmasi";
                obj[6] = table.Rows[i]["TIMEIN"].ToString();
                dt.Rows.Add(obj);
            }

            return dt;

        }

        public static DataTable GetAllMenuBooking(string pernr)
        {

            StringBuilder _table = new StringBuilder();
            DataTable dt = new DataTable("MENU");
            DataTable table = GetData.GetListMenuBooking(pernr);
            
            dt.Columns.Add(("Booking"));
            dt.Columns.Add(("Tanggal_Makan"));
            dt.Columns.Add(("Tanggal_Makan_S"));
            dt.Columns.Add(("Hari"));
            dt.Columns.Add(("Menu_Makan"));
            dt.Columns.Add(("Jam_Mulai"));
            dt.Columns.Add(("Jam_Akhir"));
            dt.Columns.Add(("Gambar"));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                StringBuilder element = new StringBuilder();
                object[] obj = new object[8];

                if (table.Rows[i]["OrderDeleted"].ToString() == "False")
                {
                    element.Append("<input type = 'checkbox' value=Book' checked>");
                }
                else
                {
                    element.Append("<input type = 'checkbox' value=Book'>");
                }
                
                obj[0] = element;
                obj[1] = table.Rows[i]["DATE"].ToString();
                obj[2] = table.Rows[i]["DATES"].ToString();
                obj[3] = table.Rows[i]["DAY"].ToString();
                obj[4] = table.Rows[i]["MENU"].ToString();
                obj[5] = table.Rows[i]["START_H"].ToString();
                obj[6] = table.Rows[i]["END_H"].ToString();
                obj[7] = table.Rows[i]["GAMBAR"].ToString();
               
                dt.Rows.Add(obj);
            }

            return dt;

        }

        public static List<CalendarEventObj> GetCalendarMenuBooking(string pernr)
        {
            List<CalendarEventObj> list = new List<CalendarEventObj>();

            DataTable data = GetData.GetListMenuBooking(pernr);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i]["OrderDeleted"].ToString() == "False")
                {
                    CalendarEventObj ce = new CalendarEventObj();
                    DateTime bookDate = Convert.ToDateTime(data.Rows[i]["DATE"]);
                    ce.id = bookDate.ToString("yyyy-MM-dd");
                    ce.title = "booked";
                    ce.start = bookDate.ToString();
                    ce.menu = data.Rows[i]["MENU"].ToString();
                    ce.startHour = data.Rows[i]["START_H"].ToString();
                    ce.endHour = data.Rows[i]["END_H"].ToString();
                    list.Add(ce);
                }
            }
            return list;
        }

        public static DataTable GetDataNikNama()
        {
            DataTable dt = new DataTable("DATA"); ;
            StringBuilder _table = new StringBuilder();
            DataTable table = GetData.GetUserDataDB();

            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("NAMA"));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = GetAction();
                //int _yearRange = Convert.ToInt32((Convert.ToDateTime(table.Rows[i]["EXPDT"]) - Convert.ToDateTime(table.Rows[i]["EFFDT"])).TotalDays / 365);
                object[] obj = new object[2];

                //obj[0] = _status;
                //obj[0] = table.Rows[i]["PERNR"].ToString() +" "+ table.Rows[i]["CNAME"].ToString();
                obj[0] = table.Rows[i]["PERNR"].ToString();
                obj[1] = table.Rows[i]["CNAME"].ToString();

                dt.Rows.Add(obj);
            }

            return dt;
        }
        public static DataTable GetDataNikNamaAct()
        {
            DataTable dt = new DataTable("DATA"); ;
            StringBuilder _table = new StringBuilder();
            DataTable table = GetData.GetUserDataDB();

            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("NAMA"));
            dt.Columns.Add(("ACTION"));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = "<input type='button' value='Edit' style='text-align:center; width:50px;'  onclick='Edit(this)' class='btn btn-info btn-xs'></input>";
                object[] obj = new object[3];

                obj[0] = table.Rows[i]["PERNR"].ToString();
                obj[1] = table.Rows[i]["CNAME"].ToString();
                obj[2] = _action;

                dt.Rows.Add(obj);
            }

            return dt;
        }


        public static DataTable GetResetData()
        {
            DataTable dt = new DataTable("DATA"); ;
            StringBuilder _table = new StringBuilder();
            DataTable table = GetData.GetResetData();

            dt.Columns.Add(("NIK"));
            dt.Columns.Add(("NAMA"));
            dt.Columns.Add(("ACTION"));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = "<input type='button' value='Reset' style='text-align:center; width:50px;'  onclick='Reset(this)' class='btn btn-info btn-xs'></input>";
                object[] obj = new object[3];

                obj[0] = table.Rows[i]["NIK"].ToString();
                obj[1] = table.Rows[i]["NAMA"].ToString();
                obj[2] = _action;

                dt.Rows.Add(obj);
            }

            return dt;
        }
    }
}
