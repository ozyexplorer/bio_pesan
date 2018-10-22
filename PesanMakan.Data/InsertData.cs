using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Timers;
using System.Configuration;


namespace PesanMakan.Data
{
    public class InsertData : Database
    {
        public static void insertMenu(string[][] array)
        {

            SqlConnection conn = Database.GetConnection();

            int row = 0;
            int maxrow = array.Length;
            string menu;
            string jam_mulai;
            string jam_selesai;
            string on_off;
            string note;
            string hari;
            string tanggalfix;

            for (row = 0; row < maxrow; row++)
            {
                StringBuilder query = new StringBuilder();
                StringBuilder query2 = new StringBuilder();
                //String strDateFormat = "yyyy-MM-dd";
                //tanggal = DateTime.ParseExact(array[row][0], strDateFormat, CultureInfo.InvariantCulture);    

                //tanggal = Convert.ToDateTime(array[row][0]);
                tanggalfix = array[row][0];
                hari = array[row][1];
                menu = array[row][2];
                jam_mulai  = array[row][5];
                jam_selesai = array[row][6];
                on_off = array[row][7];
                note = array[row][8];

                DataTable data = new DataTable();
                query.Append(" SELECT DATE FROM dbo.NFC_MENU WHERE DATE = '" + tanggalfix + "' AND ISDELETED = 'False' ");

                //cek apakah data sudah ada di db
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query.ToString(), conn);
                    comm.ExecuteNonQuery();
                    SqlDataAdapter adapter = Database.GetAdapter(comm);
                    adapter.Fill(data);
                    data.TableName = "MENU";
                }
                finally
                {
                    conn.Close();
                }
                //end of cek

                //apablia data tidak ada maka data baru ditambahkan
                if (data.Rows.Count == 0)
                {
                    query2.Append(" INSERT INTO dbo.NFC_MENU (DATE,DAY,MENU,START_H,END_H,ON_OFF,NOTE,ISDELETED,MODAT,MODBY,GAMBAR) ");
                    query2.Append(" VALUES ('" + tanggalfix + "', '" + hari + "','" + menu + "', '" + jam_mulai + "', '" + jam_selesai + "', '" + on_off + "','" + note + "','0',GETDATE(),'ADMIN','' )  ");
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }

                }else
                {
                    query2.Append(" UPDATE dbo.NFC_MENU ");
                    query2.Append(" SET MODAT=GETDATE(),MODBY='ADMIN', MENU='" + menu + "', ");
                    query2.Append(" START_H= '" + jam_mulai + "',END_H = '" + jam_selesai + "', ON_OFF=  '" + on_off + "',NOTE= '" + note + "'  ");
                    query2.Append("WHERE DATE = '" + tanggalfix + "'");

                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                //end of insert data
            }
        }

        public static void insertgambar(string namagambar,string tanggal)
        {

                SqlConnection conn = Database.GetConnection();
                StringBuilder query = new StringBuilder();
                StringBuilder query2 = new StringBuilder();
                StringBuilder query3 = new StringBuilder();
                DataTable data = new DataTable();
                string[] prSplit = tanggal.Split('/');
                int y = Convert.ToInt32(prSplit[0]);
                int m = Convert.ToInt32(prSplit[1]);
                int i = 0;

                query.Append(" SELECT DATE FROM dbo.NFC_MENU WHERE DATE = '" + tanggal + "' AND ISDELETED = 'False' ");

                //cek apakah data sudah ada di db
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query.ToString(), conn);
                    comm.ExecuteNonQuery();
                    SqlDataAdapter adapter = Database.GetAdapter(comm);
                    adapter.Fill(data);
                    data.TableName = "MENU";
                }
                finally
                {
                    conn.Close();
                }
                //end of cek 
   
                if (data.Rows.Count == 0)
                {
                    int a = DateTime.DaysInMonth(y, m);
                    for (i = 0; i < a; i++)
                    {
                        DateTime date = DateTime.Parse(y + "/" + m + "/" + (i + 1));
                        DayOfWeek dow = date.DayOfWeek; 
                        string str = dow.ToString(); 

                        
                        query2.Append(" INSERT INTO dbo.NFC_MENU (DATE,DAY,MENU,START_H,END_H,ON_OFF,NOTE,ISDELETED,MODAT,MODBY,GAMBAR) ");
                        query2.Append(" VALUES ('" + date.ToString("yyyy/MM/dd") + "', '" + str + "','', '" + "12:00:00" + "', '" + "13:00:00" + "', '"+"On"+"','','0',GETDATE(),'ADMIN','"+null+"' )  ");
       
                    }
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }


                    query3.Append(" UPDATE dbo.NFC_MENU ");
                    query3.Append(" SET MODAT=GETDATE(),MODBY='ADMIN', GAMBAR='" + namagambar + "' ");
                    query3.Append("WHERE DATE = '" + tanggal + "'");

                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query3.ToString(), conn);
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    query2.Append(" UPDATE dbo.NFC_MENU ");
                    query2.Append(" SET MODAT=GETDATE(),MODBY='ADMIN', GAMBAR='" + namagambar + "' ");
                    query2.Append("WHERE DATE = '" + tanggal + "'");

                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                //end of insert data
        }


        public static void insertDB(string nik, string nama, string[] token, string[][] array, string modifier)
        {

            SqlConnection conn = Database.GetConnection();

            //string errorMessage = string.Empty;
            int row = 0;
            int maxrow = array.Length;
            
            string bokda;
            string jam_akhir;
            string jam_mulai;
            //UpdateAllOrder(nik, token[0], "1", modifier);
            if (maxrow > 0)
            {
                for (row = 0; row < maxrow; row++)
                {
                    StringBuilder query = new StringBuilder();
                    StringBuilder query2 = new StringBuilder();
                    //DateTime bokDate = Convert.ToDateTime(array[row][0]);
                    //bokda = bokDate.ToString();
                    string bokdah = array[row][0];
                    jam_mulai = array[row][3];
                    jam_akhir = array[row][4];
                    string[] bokdasplit = bokdah.Split('/');
                    //string[] bokdathn = bokdasplit[2].Split(' ');
                    bokda = bokdasplit[1] + '/' + bokdasplit[2] + '/' + bokdasplit[0];

                    DataTable data = new DataTable();
                    query.Append(" SELECT NIK FROM dbo.NFC_ORDER WHERE BOKDA = '" + bokda + "' AND NIK = '" + nik + "' ");

                    //cek apakah data sudah ada di db
                    try
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand(query.ToString(), conn);
                        comm.ExecuteNonQuery();
                        SqlDataAdapter adapter = Database.GetAdapter(comm);
                        adapter.Fill(data);
                        data.TableName = "NIK";
                    }
                    finally
                    {
                        conn.Close();
                    }
                    //end of cek

                    //apablia data tidak ada maka data baru ditambahkan
                    if (data.Rows.Count == 0)
                    {
                                
                        query2.Append(" INSERT INTO dbo.NFC_ORDER (BOKDA,NIK,NAMA,TOKEN,FLAGPRINTED,FLAGEAT,ISDELETED,START_H,END_H,CHGUS,CHGDT) ");
                        query2.Append(" VALUES ('" + bokda + "', '" + nik + "', '" + nama + "', '" + token[row] + "', '0','0','0','" + jam_mulai + "','" + jam_akhir + "','" + modifier + "',GETDATE()) ");
                
                        try
                        {
                            conn.Open();
                            SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                            comm.ExecuteNonQuery();
                        }
                        finally
                        {
                            conn.Close();
                        }

                    }
                    else
                    {
                        UpdateOrder(nik, bokda, token[row], "0", modifier);
                        //query2.Append(" UPDATE dbo.NFC_ORDER ");
                        //query2.Append("SET NIK= '" + nik + "',NAMA='" + nama + "', TOKEN= '" + token[row] + "', FLAGPRINTED='0',FLAGEAT='0',START_H= '" + jam_mulai + "',END_H='" + jam_akhir + "' ");
                        //query2.Append("WHERE BOKDA='" + bokda + "' AND NIK= '" + nik + "' ");
                        //try
                        //{
                        //    conn.Open();
                        //    SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                        //    comm.ExecuteNonQuery();
                        //}
                        //finally
                        //{
                        //    conn.Close();
                        //}

                    }
                    //end of insert data

                }
            }
        }
        public static String insert(string cardUID, string CardValue, string modifier)
        {

            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            StringBuilder query3 = new StringBuilder();
            StringBuilder query4 = new StringBuilder();
            StringBuilder query5 = new StringBuilder();

            string errorMessage = string.Empty;
            DataTable data = new DataTable();
            DataTable data2 = new DataTable();
            DataTable data3 = new DataTable();
            DataTable data4 = new DataTable();
            DataTable data5 = new DataTable();
            //string timePrint= DateTime.Now.;

            //query.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE BOKDA = CAST(GETDATE() AS DATE) ");
            query.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE CARDID = '" + cardUID + "' AND ENDDA = '9999-12-31' ");
            string listUID = string.Empty;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();

                //DataTable data = new DataTable();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "NIK";

            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }

            //int iterasi = 0;
            //int found = 0;
            string decryptedstring = string.Empty;
            string[] array = data.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();


            //Cek NIK
            if (data.Rows.Count != 0)
            {
                string nik = data.Rows[0]["NIK"].ToString();
                query2.Append(" SELECT TOKEN FROM dbo.NFC_ORDER ");
                query2.Append("WHERE NIK = '" + nik + "' AND bokda =CAST(GETDATE() AS DATE) AND ISDELETED = 0 ");

                try
                {
                    conn.Open();
                    SqlCommand comm2 = new SqlCommand(query2.ToString(), conn);
                    comm2.ExecuteNonQuery();

                    SqlDataAdapter adapter2 = Database.GetAdapter(comm2);
                    adapter2.Fill(data2);
                    data2.TableName = "TOKEN";
                }
                finally
                {
                    conn.Close();
                }
        
                //Cek Tanggal
                if (data2.Rows.Count != 0)
                {
                    /*query3.Append(" SELECT TOKEN FROM dbo.NFC_ORDER ");
                    query3.Append("WHERE NIK = '" + nik + "' AND bokda =CAST(GETDATE() AS DATE) AND ISDELETED = 0 AND CONVERT(varchar(10), GETDATE(), 108) BETWEEN START_H AND END_H ");

                    try
                    {
                        conn.Open();
                        SqlCommand comm3 = new SqlCommand(query3.ToString(), conn);
                        comm3.ExecuteNonQuery();

                        SqlDataAdapter adapter3 = Database.GetAdapter(comm3);
                        adapter3.Fill(data3);
                        data3.TableName = "TANGGAL";
                    }
                    finally
                    {
                        conn.Close();
                    }

                    //Cek Print
                    if (data3.Rows.Count != 0)
                    {*/

                        query4.Append(" SELECT NAMA FROM dbo.NFC_ORDER ");
                        query4.Append("WHERE NIK = '" + nik + "' AND bokda =CAST(GETDATE() AS DATE) AND ISDELETED = 0 AND FLAGPRINTED = 0 ");

                        try
                        {
                            conn.Open();
                            SqlCommand comm4 = new SqlCommand(query4.ToString(), conn);
                            comm4.ExecuteNonQuery();

                            SqlDataAdapter adapter4 = Database.GetAdapter(comm4);
                            adapter4.Fill(data4);
                            data4.TableName = "PRINT";
                        }
                        finally
                        {
                            conn.Close();
                        }

                        //Cek Ada Data
                        if (data4.Rows.Count != 0)
                        {
                            string token = data2.Rows[0]["TOKEN"].ToString();
                            string nama = data4.Rows[0]["NAMA"].ToString();

                            query5.Append(" UPDATE dbo.NFC_ORDER ");
                            query5.Append(" SET FLAGEAT = 1, FLAGPRINTED = 1, TIMEIN = CONVERT(varchar(10), GETDATE(), 108) ");
                            query5.Append("WHERE BOKDA = CAST(GETDATE() AS DATE) AND TOKEN ='" + token + "' ");

                            try
                            {
                                conn.Open();
                                SqlCommand comm5 = new SqlCommand(query5.ToString(), conn);
                                comm5.ExecuteNonQuery();
                            }
                            finally
                            {
                                conn.Close();
                            }

                            return token+"-"+nama;

                        }
                        else
                        {
                            string token = "Print";
                            return token;
                        }
                     /*}
                     else
                     {
                     string token = "Jam Makan Tutup";
                     return token;
                     }*/
                }
                else
                {
                    string token = "Tidak Booking";
                    return token;
                }
            }
            else
            {
                string token = "Belum Terdaftar";
                return token;
            }

        }

        public static void DelMenuDB(string tgl, string menu)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE dbo.NFC_MENU ");
            query.Append(" SET ISDELETED='1',MODAT=GETDATE(),MODBY='ADMIN' ");
            query.Append("WHERE DATE = '" + tgl + "' AND MENU ='" + menu + "' ");

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();


            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }


        }


        //AddMenu
        public static void AddMenuDB(string tgl, string menu, string jam)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            string errorMessage = string.Empty;
            DataTable data = new DataTable();
            query.Append(" SELECT DATE FROM dbo.NFC_MENU WHERE DATE = '" + tgl + "' AND ISDELETED = 0 ");
            string listUID = string.Empty;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "TGL";

            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }

            int found = data.Rows.Count;

            if (found != 0)
            {
                query2.Append(" UPDATE dbo.NFC_MENU ");
                query2.Append(" SET DATE = '" + tgl + "', MENU = '" + menu + "', JAM= '" + jam + "',ISDELETED='0',MODAT=GETDATE(),MODBY='ADMIN' ");
                query2.Append("WHERE DATE = '" + tgl + "' AND ISDELETED = 0 ");
            }
            else
            {
                query2.Append(" INSERT INTO dbo.NFC_MENU (DATE,MENU,JAM,ISDELETED,MODAT,MODBY)");
                query2.Append(" VALUES ('" + tgl + "','" + menu + "','" + jam + "','0',GETDATE(),'ADMIN')");
            }

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                comm.ExecuteNonQuery();
            }
            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }

        }

        public static int RegCardDB(string nik, string nama, string cardid, string role)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
           
            string errorMessage = string.Empty;
            DataTable dataID = new DataTable();
            query.Append("SELECT CARDID from dbo.NFC_DATA WHERE CARDID= '" + cardid + "' AND  ENDDA = '9999-12-31' ");
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(dataID);
                dataID.TableName = "CARDUID";
            }
            finally
            {
                conn.Close();
            }

            int foundID = dataID.Rows.Count;
            if (foundID != 0)
            {
                return 1;
            }else
            {
                DataTable data = new DataTable();
                StringBuilder query2 = new StringBuilder();
                StringBuilder query3 = new StringBuilder();
                query2.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE NIK = '" + nik + "' AND  ENDDA = '9999-12-31' ");
                //string listUID = string.Empty;
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                    comm.ExecuteNonQuery();
                    SqlDataAdapter adapter = Database.GetAdapter(comm);
                    adapter.Fill(data);
                    data.TableName = "CARD";

                }

                finally
                {
                    //Finally Close the Connection...
                    conn.Close();
                }

                int found = data.Rows.Count;

                if (found != 0)
                {
                        query3.Append(" UPDATE dbo.NFC_DATA ");
                        query3.Append(" SET NIK = '" + nik + "', NAMA = '" + nama + "', CARDID= '" + cardid + "', ROLE= '" + role + "',BEGDA = GETDATE(),  ENDDA = '9999-12-31',MODAT=GETDATE(),MODBY='ADMIN' ");
                        query3.Append("WHERE NIK = '" + nik + "' AND  ENDDA = '9999-12-31' ");
                    
                }
                else
                {
                    query3.Append(" INSERT INTO dbo.NFC_DATA (NIK,NAMA,CARDID,ROLE,BEGDA,ENDDA,MODAT,MODBY)");
                    query3.Append(" VALUES ('" + nik + "','" + nama + "','" + cardid + "','" + role + "',GETDATE(),'9999-12-31',GETDATE(),'ADMIN')");
                }

                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query3.ToString(), conn);
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    //Finally Close the Connection...
                    conn.Close();
                }

                return 0;
            }
            
        }
        
        public static void DelDataDB(string nik, string nama, string cardid, string role)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE dbo.NFC_DATA ");
            query.Append(" SET ENDDA= GETDATE(),BEGDA= GETDATE(), MODAT=GETDATE(),MODBY='ADMIN' ");
            query.Append("WHERE NIK = '" + nik + "' AND CARDID ='" + cardid + "' ");

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }
        }

        public static void DelOrder(string tgl, string nik, string nama)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE dbo.NFC_ORDER ");
            query.Append(" SET ISDELETED = '1'");
            query.Append("WHERE NIK = '" + nik + "' AND BOKDA = '"+tgl+ "' AND NAMA = '" + nama + "'  ");

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();


            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }
        }

        public static void RegMemberDB(string nik, string nama)
        {
            SqlConnection conn = Database.GetConnectionNFC();

            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            string errorMessage = string.Empty;
            DataTable data = new DataTable();
            query.Append(" SELECT PERNR FROM outsource.USER_DATA WHERE PERNR = '" + nik + "' AND ENDDA = '9999-12-31' ");
            string listUID = string.Empty;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "MEMBER";

            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }

            int found = data.Rows.Count;

            if (found != 0)
            {
                //query2.Append("");
                query2.Append(" UPDATE outsource.USER_DATA ");
                query2.Append(" SET PERNR = '" + nik + "', CNAME = '" + nama + "' ");
                query2.Append("WHERE PERNR = '" + nik + "' AND ENDDA = '9999-12-31' ");
            }
            else
            {
                query2.Append(" INSERT INTO outsource.USER_DATA (BEGDA,ENDDA, REGDT,PERNR,ORGCD,PRORG,CNAME,VENDR,CHGDT,USRDT)");
                query2.Append(" VALUES (GETDATE(),'9999-12-31' ,GETDATE(), '" + nik + "','','','" + nama + "','',GETDATE(),'1906')");
            }

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                comm.ExecuteNonQuery();
            }
            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }
        }


        public static void ResetDataDB(string nik)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();

            query.Append(" UPDATE dbo.NFC_ORDER ");
            query.Append(" SET FLAGEAT = 0, FLAGPRINTED = 0, TIMEIN = CONVERT(varchar(10), GETDATE(), 108) ");
            query.Append("WHERE NIK = '" + nik + "' ");

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
            }

            finally
            {
                //Finally Close the Connection...
                conn.Close();
            }
        }

        public static void UpdateOrder(string pernr, string bookDate, string token, string status, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("[dbo].[usp_UpdateOrder]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, pernr);
            AddParameter(ref cmd, "@vBookDate", ParameterDirection.Input, bookDate);
            AddParameter(ref cmd, "@vToken", ParameterDirection.Input, token);
            AddParameter(ref cmd, "@vDeleteStatus", ParameterDirection.Input, status);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);

            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateAllOrder(string pernr, string token, string status, string modifier)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("[dbo].[usp_UpdateAllOrder]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, pernr);
            AddParameter(ref cmd, "@vToken", ParameterDirection.Input, token);
            AddParameter(ref cmd, "@vDeleteStatus", ParameterDirection.Input, status);
            AddParameter(ref cmd, "@vModifier", ParameterDirection.Input, modifier);

            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
