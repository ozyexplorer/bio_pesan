using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace PesanMakan.Data
{
    public class GetData : Database
    {
        public static DataTable GetListOrder(string pernr)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("[dbo].[usp_GetListOrder]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, pernr);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                return data;
            }
            finally
            {
                conn.Close();
            }
        }


        public static string GetGambar(string tanggal)
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT GAMBAR FROM dbo.NFC_MENU WHERE DATE = '" + tanggal + "'");
            DataTable data = new DataTable();
            string hasil;
            
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "gambar";

              
            }
            finally
            {
                conn.Close();
            }

            if (data.Rows.Count == 0)
            {
                hasil = "";
            }else
            {
                hasil = data.Rows[0]["GAMBAR"].ToString();
                
            }
            return hasil;
        }

        public static DataTable GetOrderByUser(string User,string Nik)
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();
            
            query.Append(" SELECT * FROM dbo.NFC_ORDER WHERE ISDELETED = 0 AND FLAGEAT = 0 AND FLAGPRINTED = 0 AND NAMA = '" + User + "' AND NIK = '" + Nik + "' ");
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
        


        public static DataTable GetListMenuBooking(string pernr)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("[dbo].[usp_GetListMenuBooking]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vPersonelNumber", ParameterDirection.Input, pernr);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetReconcile()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("[dbo].[usp_GetReconcile]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                return data;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetNik(string email)
        {

            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();
            //string data;
            query.Append(" SELECT PERNR FROM BIOFARMA.bioumum.USER_EMAIL WHERE EMAIL = '" + email + "'");
            //string errorMessage = string.Empty;

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "email";

                return data;
            }
            finally
            {
                conn.Close();
            }

        }


        public static string CheckToken(string token)
        {
            SqlConnection conn = Database.GetConnection();
            SqlConnection conn2 = Database.GetConnection();
            SqlConnection conn3 = Database.GetConnection();

            DataTable data = new DataTable();
            DataTable data2 = new DataTable();

            StringBuilder query = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            StringBuilder query3 = new StringBuilder();
            string status;
            query.Append(" SELECT * FROM dbo.NFC_ORDER WHERE TOKEN = '"+token+ "' AND BOKDA= CAST(GETDATE() AS DATE)  ");
            string errorMessage = string.Empty;

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                comm.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "Token";

                //string tgl = data.Rows[0]["BOKDA"].ToString();
            }
            finally
            {
                conn.Close();
            }


            if (data.Rows.Count != 0)
            {
                query2.Append(" SELECT * FROM dbo.NFC_RECONCILE WHERE NIK = '" + data.Rows[0]["NIK"].ToString() + "' AND BOKDA= CAST(GETDATE() AS DATE)  ");

                try
                {
                    conn2.Open();
                    SqlCommand comm2 = new SqlCommand(query2.ToString(), conn2);
                    comm2.ExecuteNonQuery();
                    SqlDataAdapter adapter2 = Database.GetAdapter(comm2);
                    adapter2.Fill(data2);
                    data2.TableName = "NIK";

                }
                finally
                {
                    conn2.Close();
                }



                if (data2.Rows.Count != 0)
                {
                    status = "Token Sama";
                }
                else
                {
                    query3.Append("[dbo].[usp_InsertReconcile]");

                    SqlCommand cmd = GetStoredProcedureCommand(conn3, query3.ToString());
                    AddParameter(ref cmd, "@vTgl", ParameterDirection.Input, data.Rows[0]["BOKDA"]);
                    AddParameter(ref cmd, "@vNik", ParameterDirection.Input, data.Rows[0]["NIK"].ToString());
                    AddParameter(ref cmd, "@vName", ParameterDirection.Input, data.Rows[0]["NAMA"].ToString());
                    AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

                    try
                    {
                        conn3.Open();
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn3.Close();
                    }

                    status = "Token Sesuai";//data token ada
                }
            }
            else
            {
                status = "Token Tidak Sesuai";//data tidak ada
            }

            return status;
        }

        public static DataTable GetRecapOrderDB(string Period)
        {
            SqlConnection conn = Database.GetConnection();
            SqlConnection conn2 = Database.GetConnection();
            //StringBuilder query = new StringBuilder();
            int i=0;
            string status = "";
            string[] prSplit = Period.Split('-');
            int y = Convert.ToInt32(prSplit[0]);
            int m = Convert.ToInt32(prSplit[1]);
            DataTable dt = new DataTable();

            dt.Columns.Add(("Tanggal"));
            dt.Columns.Add(("Hari"));
            dt.Columns.Add(("Total_Booking"));
            dt.Columns.Add(("Total_Konfirmasi"));
            dt.Columns.Add(("Persentase"));
            dt.Columns.Add(("Status"));
            
            int dom = DateTime.DaysInMonth(y, m);
            string date;
            for (i = 1; i <= dom; i++)
            {
                if (i < 10)
                {
                    date = "0" + i;
                }else
                {
                    date = i.ToString();
                }

                string PeriodFix = Period + "-" + date;
                DateTime tgl = DateTime.Parse(Period + "-" + i);
                DayOfWeek dow = tgl.DayOfWeek; //enum
                string hari = dow.ToString(); //string
                string query =" SELECT * FROM dbo.NFC_ORDER WHERE BOKDA LIKE '" + PeriodFix + "%' and ISDELETED = '0' ";
                
                
                try
                {
                    conn.Open();
                    DataTable data = new DataTable();
                    SqlCommand comm = new SqlCommand(query.ToString(), conn);
                    comm.ExecuteNonQuery();
                    SqlDataAdapter adapter = Database.GetAdapter(comm);
                    adapter.Fill(data);
                    data.TableName = "MENU";
                    
                    object[] obj = new object[6];
                    
                    if (data.Rows.Count != 0)
                    {
                        int total_booking = data.Rows.Count;
                        string query2 = " SELECT * FROM dbo.NFC_ORDER WHERE BOKDA LIKE '" + PeriodFix + "%' and ISDELETED = '0' and FLAGEAT = '1' ";

                        try
                        {
                            conn2.Open();
                            DataTable data2 = new DataTable();
                            SqlCommand comm2 = new SqlCommand(query2.ToString(), conn2);
                            comm2.ExecuteNonQuery();
                            SqlDataAdapter adapter2 = Database.GetAdapter(comm2);
                            adapter2.Fill(data2);
                            
                            int total_konfirmasi = data2.Rows.Count;

                            if (hari == "Sunday")
                            {
                                hari = "Minggu";
                            }
                            else if (hari == "Monday")
                            {
                                hari = "Senin";
                            }
                            else if (hari == "Tuesday")
                            {
                                hari = "Selasa";
                            }
                            else if (hari == "Wednesday")
                            {
                                hari = "Rabu";
                            }
                            else if (hari == "Thursday")
                            {
                                hari = "Kamis";
                            }
                            else if (hari == "Friday")
                            {
                                hari = "Jum'at";
                            }
                            else if (hari == "Saturday")
                            {
                                hari = "Sabtu";
                            }


                            string tanggal = DateTime.Now.ToString("dd/MM/yyyy");

                            string[] tglSplit = tanggal.Split('/');
                            int ds = Convert.ToInt32(tglSplit[0]);
                            int ms = Convert.ToInt32(tglSplit[1]);
                            int ys = Convert.ToInt32(tglSplit[2]);
                            int dsfix = ds + 7;

                            string tanggaldb = data.Rows[0]["BOKDA"].ToString();
                            string tanggaldbfix = tanggaldb.Replace(" 00.00.00", "");
                            string[] tanggalSplitDb = tanggaldbfix.Split('/');
                            
                            int dsdb = Convert.ToInt32(tanggalSplitDb[0]);
                            int msdb = Convert.ToInt32(tanggalSplitDb[1]);
                            int ysdb = Convert.ToInt32(tanggalSplitDb[2]);


                            //24/9/2018
                            //22/9/2018

                            if (ysdb >= ys && msdb >= ms && dsdb>dsfix)
                            {
                                status = "No Fix";
                            }
                            else if (ysdb >= ys && msdb >= ms && dsdb <= dsfix)
                            {
                                if (ysdb == ys && msdb == ms && dsdb <= dsfix)
                                {
                                    status = "Fix";
                                }else{
                                    status = "No Fix";
                                }
                            }
                            else if (ysdb > ys && msdb <= ms && dsdb <= dsfix)
                            {
                                status = "No Fix";
                            }
                            else if (ysdb > ys && msdb <= ms && dsdb >= dsfix)
                            {
                                status = "No Fix";
                            }
                            else
                            {
                                status = "Fix";
                            }

                            decimal persentase = (total_konfirmasi / total_booking) * 100;
                            obj[0] = data.Rows[0]["BOKDA"].ToString();
                            obj[1] = hari;
                            obj[2] = total_booking;
                            obj[3] = total_konfirmasi;
                            obj[4] = persentase+"%";
                            obj[5] = status;
                            dt.Rows.Add(obj);
                          
                        }
                        finally
                        {
                            conn2.Close();
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return dt;
            
        }

        public static int TotalMember()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();

            string sMonth = DateTime.Now.ToString("MM");
            DataTable data = new DataTable();
            int hasil;
        
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("application.usp_GetNfcDATA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adapter = Database.GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "DATA";
            }
            finally
            {
                conn.Close();
            }

            if (data.Rows.Count == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = data.Rows.Count;

            }
            return hasil;
        }



        public static int TotalBooking()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();

            string sMonth = DateTime.Now.ToString("MM");
            DataTable data = new DataTable();
            int hasil;

            query.Append(" SELECT * FROM dbo.NFC_ORDER WHERE BOKDA LIKE '%-" + sMonth + "-%' and ISDELETED = '0' ");
            
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "totalbooking";
            }
            finally
            {
                conn.Close();
            }

            if (data.Rows.Count == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = data.Rows.Count;

            }
            return hasil;
        }


        public static int TotalKonfirmasi()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();

            string sMonth = DateTime.Now.ToString("MM");
            DataTable data = new DataTable();
            int hasil;

            query.Append(" SELECT * FROM dbo.NFC_ORDER WHERE BOKDA LIKE '%-" + sMonth + "-%' and ISDELETED = '0'  and FLAGEAT = '1' ");

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query.ToString(), conn);
                SqlDataAdapter adapter = Database.GetAdapter(comm);
                adapter.Fill(data);
                data.TableName = "totalkonfirmasi";
            }
            finally
            {
                conn.Close();
            }

            if (data.Rows.Count == 0)
            {
                hasil = 0;
            }
            else
            {
                hasil = data.Rows.Count;

            }
            return hasil;
        }

        public static DataTable MenuCheckDB(string period)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("dbo.usp_GetMenu");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vPeriod", ParameterDirection.Input, period);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
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


            query.Append(" SELECT CNAME FROM bioumum.USER_DATA WHERE PERNR = '" + nik + "' and ENDDA = '9999-12-31' ");

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


        public static DataTable GetResetData()
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlCommand cmd = new SqlCommand("application.usp_GetResetDATA", conn);
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

        public static DataTable GetSearchDataNama(string nama)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("application.usp_GetSearchDATA");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());

            AddParameter(ref cmd, "@vNama", ParameterDirection.Input, nama);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
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


            query.Append(" SELECT NIK,NAMA,ROLE FROM dbo.NFC_DATA WHERE CARDID= '" + cardUID + "' AND  ENDDA = '9999-12-31' ");

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
        public static DataTable LoginUserNameDB(string nik)
        {
            SqlConnection conn = Database.GetConnection();
            StringBuilder query = new StringBuilder();


            query.Append(" SELECT NIK,NAMA,ROLE FROM dbo.NFC_DATA WHERE NIK= '" + nik + "' AND  ENDDA >= GETDATE() ");

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

    public class ApplicationCatalogGet : GetData
    {
        public static DateTime? GetDateTimeServer()
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();
            query.Append("[application].[usp_GetDateTimeServer]");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                data.TableName = "Data";

                DateTime? serverDateTime = Convert.ToDateTime(data.Rows[0]["DTIME"] ?? null);

                return serverDateTime;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public class UserCatalogGet : GetData
    {
        public static DataTable GetUserDataByNIK(string personalNumber)
        {
            SqlConnection conn = GetConnection();
            StringBuilder query = new StringBuilder();

            query.Append("application.usp_GetUserInfo");
            string errorMessage = string.Empty;

            SqlCommand cmd = GetStoredProcedureCommand(conn, query.ToString());
            AddParameter(ref cmd, "@vPersonalNumber", ParameterDirection.Input, personalNumber);
            AddParameter(ref cmd, "@vMessageText", ParameterDirection.Output, errorMessage);

            try
            {
                conn.Open();
                DataTable data = new DataTable();
                SqlDataAdapter adapter = GetAdapter(cmd);
                adapter.Fill(data);
                return data;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
