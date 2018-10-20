using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Biofarma.NFC.Data;
using System.Web;
using System.Web.Services;
using System.Data;
namespace Biofarma.NFC.Business
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

        //insert data user ke database
        public static void inserttoDB(string nik, string nama, string[][] array)
        {
            int n = array.Length;
            int i;
            string[] token = new string[n];
            for (i = 0; i < n; i++)
            {
                token[i] = Code.CreateCode((i+1),6);
            }
            InsertData.insertDB(nik, nama, token, array);
        }

        //get datatable for menu in admin
        public static DataTable GetAllMenu()
        {

            StringBuilder _table = new StringBuilder();
            DataTable dt = new DataTable("MENU");
            DataTable table = GetData.GetMenuDB();

            dt.Columns.Add(("Tanggal"));
            dt.Columns.Add(("Menu_Makan"));
            dt.Columns.Add(("Jam"));
            dt.Columns.Add(("Action"));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = GetAction();
                //int _yearRange = Convert.ToInt32((Convert.ToDateTime(table.Rows[i]["EXPDT"]) - Convert.ToDateTime(table.Rows[i]["EFFDT"])).TotalDays / 365);
                object[] obj = new object[4];

                //obj[0] = _status;
                obj[0] = table.Rows[i]["DATE"].ToString();
                obj[1] = table.Rows[i]["MENU"].ToString();
                obj[2] = table.Rows[i]["JAM"].ToString();
                obj[3] = _action;
                dt.Rows.Add(obj);
            }

            return dt;

        }

        protected static string GetAction()
        {
            StringBuilder element = new StringBuilder();

            element.Append("<input type='button' value='Edit' style='text-align:center;'  onclick='Edit(this)' class='btn btn-danger btn-xs'></input>");
            element.Append("<input type='button' value='Delete' style='text-align:center;'  onclick='deleteRow(this)' class='btn btn-danger btn-xs'></input>");
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
        public static void RegKartu(string nik, string nama, string cardid, string role)
        {

            InsertData.RegCardDB(nik, nama, cardid, role);
        }
        public static void RegAnggota(string nik, string nama)
        {

            InsertData.RegMemberDB(nik, nama);
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
                dt.Rows.Add(obj);
            }

            return dt;
        }
        //Delete USer Data
        public static void HapusData(string nik, string nama, string cardid, string role)
        {
            InsertData.DelDataDB(nik, nama, cardid,role);
        }

        public static string[] LoginCard(string cardUID)
        {
            string role;
            string[] data = new string[3];
            DataTable d = GetData.LoginCardDB(cardUID);
            if(d.Rows.Count != 0)
            {
                data[0] = d.Rows[0]["NIK"].ToString();
                data[1] = d.Rows[0]["NAMA"].ToString();      
                data[2] = d.Rows[0]["ROLE"].ToString();
                role = Cryptography.Encrypt(data[2],true);
                return data;
            }else
            {
                data[0] = "";
                data[1] = "";
                data[2] = "";
                return data;
            }
           
            
        }

        public static DataTable GetAllMenuBooking()
        {

            StringBuilder _table = new StringBuilder();
            DataTable dt = new DataTable("MENU");
            DataTable table = GetData.GetMenuDB();
            StringBuilder element = new StringBuilder();
            element.Append("<input type='checkbox' value=Book'>");

            dt.Columns.Add(("Booking"));
            dt.Columns.Add(("Tanggal_Makan"));
            dt.Columns.Add(("Menu_Makan"));
            dt.Columns.Add(("Jam"));
            dt.Columns.Add(("Action"));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string _action = GetAction();
                //int _yearRange = Convert.ToInt32((Convert.ToDateTime(table.Rows[i]["EXPDT"]) - Convert.ToDateTime(table.Rows[i]["EFFDT"])).TotalDays / 365);
                object[] obj = new object[5];

                //obj[0] = _status;
                obj[0] = element;
                obj[1] = table.Rows[i]["DATE"].ToString();
                obj[2] = table.Rows[i]["MENU"].ToString();
                obj[3] = table.Rows[i]["JAM"].ToString();
                obj[4] = _action;
                dt.Rows.Add(obj);
            }

            return dt;

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
                string _action = GetAction();
                object[] obj = new object[3];
               
                obj[0] = table.Rows[i]["PERNR"].ToString();
                obj[1] = table.Rows[i]["CNAME"].ToString();
                obj[2] = _action;

                dt.Rows.Add(obj);
            }

            return dt;
        }
    }
}
