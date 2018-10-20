using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;


namespace Biofarma.NFC.Data
{
    public class InsertData
    {

        public static void insertDB(string nik, string nama,string [] token, string[][] array)
        {

            SqlConnection conn = Database.GetConnection();
            
            //string errorMessage = string.Empty;
            int row = 0;
            int maxrow = array.Length;
            string bokda;
            string jam;
            
            for(row=0;row<maxrow;row++)
            {
                StringBuilder query = new StringBuilder();
                StringBuilder query2 = new StringBuilder();
                bokda = array[row][0];
                jam = array[row][2];
                string[] bokdasplit = bokda.Split('/');
                string[] bokdathn = bokdasplit[2].Split(' ');
                string bokdafix = bokdathn[0] +'-'+ bokdasplit[1] + '-' + bokdasplit[0];

                
                DataTable data = new DataTable();
                query.Append(" SELECT NIK FROM dbo.NFC_ORDER WHERE BOKDA = '" + bokdafix + "' AND NIK = '" + nik + "' ");
                
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
                if(data.Rows.Count == 0)
                {
                    query2.Append(" INSERT INTO dbo.NFC_ORDER (BOKDA,NIK,NAMA,TOKEN,FLAGPRINTED,FLAGEAT,ISDELETED,JAM) ");
                    query2.Append(" VALUES ('" + bokdafix + "', '" + nik + "', '" + nama + "', '" + token[row] + "', '0','0','0','" + jam + "' )  ");
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
        public static String insert(string cardUID, string CardValue, string modifier)
        {
            
            SqlConnection conn = Database.GetConnection();
            
            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            string errorMessage = string.Empty;
            DataTable data = new DataTable();
            DataTable data2 = new DataTable();
            //query.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE BOKDA = CAST(GETDATE() AS DATE) ");
            query.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE CARDID = '"+cardUID+"' AND ISDELETED = 0 ");
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
           
            if (data.Rows.Count != 0)
            {
                string nik = data.Rows[0]["NIK"].ToString();
                query2.Append(" SELECT TOKEN FROM dbo.NFC_ORDER WHERE NIK = '" + nik + "' AND bokda =CAST(GETDATE() AS DATE) AND ISDELETED = 0 AND FLAGPRINTED = 0 ");
                conn.Open();
                SqlCommand comm = new SqlCommand(query2.ToString(), conn);
                comm.ExecuteNonQuery();

                //DataTable data = new DataTable();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data2);
                data2.TableName = "TOKEN";
                if (data2.Rows.Count != 0)
                {
                    string token = data2.Rows[0]["TOKEN"].ToString(); ;
                    return token;
                }else
                {
                    string token = "Tidak Booking";
                    return token;
                }
            }else
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
                query2.Append(" SET DATE = '" + tgl + "', MENU = '" + menu + "', JAM= '" + jam+ "',ISDELETED='0',MODAT=GETDATE(),MODBY='ADMIN' ");
                query2.Append("WHERE DATE = '" + tgl + "' AND ISDELETED = 0 ");
            }else
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

        public static void RegCardDB(string nik, string nama, string cardid, string role)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            string errorMessage = string.Empty;
            DataTable data = new DataTable();
            query.Append(" SELECT NIK FROM dbo.NFC_DATA WHERE NIK = '" + nik + "' AND ISDELETED = 0 ");
            string listUID = string.Empty;
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
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
                query2.Append(" UPDATE dbo.NFC_DATA ");
                query2.Append(" SET NIK = '" + nik + "', NAMA = '" + nama + "', CARDID= '" + cardid + "', ROLE= '" + role + "' ,ISDELETED='0',MODAT=GETDATE(),MODBY='ADMIN' ");
                query2.Append("WHERE NIK = '" + nik + "' AND ISDELETED = 0 ");
            }
            else
            {
                query2.Append(" INSERT INTO dbo.NFC_DATA (NIK,NAMA,CARDID,ROLE,ISDELETED,MODAT,MODBY)");
                query2.Append(" VALUES ('" + nik + "','" + nama + "','" + cardid+ "','" + role + "','0',GETDATE(),'ADMIN')");
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
        public static void DelDataDB(string nik, string nama, string cardid, string role)
        {
            SqlConnection conn = Database.GetConnection();

            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE dbo.NFC_DATA ");
            query.Append(" SET ISDELETED='1',MODAT=GETDATE(),MODBY='ADMIN' ");
            query.Append("WHERE NIK = '" + nik + "' AND CARDID ='" +cardid+ "' ");

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
                query2.Append("");
                //query2.Append(" UPDATE dbo.NFC_DATA ");
                //query2.Append(" SET NIK = '" + nik + "', NAMA = '" + nama + "', CARDID= '" + cardid + "', ROLE= '" + role + "' ,ISDELETED='0',MODAT=GETDATE(),MODBY='ADMIN' ");
                //query2.Append("WHERE NIK = '" + nik + "' AND ISDELETED = 0 ");
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
    }
}
