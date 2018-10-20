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
    public class GetData
    {

        public static DataTable GetMenuDB()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();


            query.Append(" SELECT * FROM dbo.NFC_MENU WHERE ISDELETED = 0 ");
            string errorMessage = string.Empty;

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "Menu";
                return data;
            }
            finally
            {
                conn.Close();
            }


        } 
        public static DataTable SearchNikDB(string nik)
        {
           
            SqlConnection conn = Database.GetConnectionNFC();
            StringBuilder query = new StringBuilder();


            query.Append(" SELECT CNAME FROM bioumum.USER_DATA WHERE PERNR = '"+nik+"' and ENDDA = '9999-12-31' ");
            
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "NAMA";
                return data;

               
            }
            finally
            {
                conn.Close();
            }

        }
        public static DataTable GetUserDataDB()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();


            //query.Append(" SELECT * FROM dbo.NFC_DATA WHERE ISDELETED = '0' ");
            //query.Append(" SELECT TA.PERNR, TA.CNAME, COALESCE(n.CARDID, 'Belum Teregister') CARDID,  ");
            //query.Append("COALESCE(n.ROLE, 'Belum Teregister') ROLE ");
            //query.Append(" from ");
            //query.Append(" (select PERNR, CNAME ");
            //query.Append(" FROM[BIOFARMA].[bioumum].[USER_DATA] ");
            //query.Append(" union ");
            //query.Append(" select PERNR, CNAME ");
            //query.Append(" FROM[BIOFARMA].[outsource].[USER_DATA]) Ta ");
            //query.Append(" full join PRODUCTION.dbo.NFC_DATA n ");
            //query.Append(" on ta.PERNR = n.NIK ");
            //query.Append(" where PERNR is not null ");
            //query.Append(" order by IDCard desc ");
            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand cmd = new SqlCommand("application.usp_GetNfcDATA", conn);     
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "DATA";
                return data;


            }
            finally
            {
                conn.Close();
            }
        }
        public static DataTable LoginCardDB(string cardUID)
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();


            query.Append(" SELECT NIK,NAMA,ROLE FROM dbo.NFC_DATA WHERE CARDID= '"+cardUID+"' AND ISDELETED = '0' ");

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "DATA";
                return data;


            }
            finally
            {
                conn.Close();
            }
        }
    }
}
