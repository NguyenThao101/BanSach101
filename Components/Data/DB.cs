using Microsoft.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections;
using System.Data;

namespace BanSach.Components.Data
{
    public class DB
    {
       
            readonly IConfiguration _configuration;
            public DB(IConfiguration configuration)
            {
                _configuration = configuration;
                Set_Connect(configuration.GetConnectionString("DefaultConnection") ?? "");

            }
            private static string CONN_STRING = "";

            public static void Set_Connect_DB(string Key_Config)
            {
                CONN_STRING = Key_Config;
            }
            public static void Set_Connect(string _conn)
            {
                CONN_STRING = _conn;
            }

            public static string Get_Connect()
            {
                return CONN_STRING;
            }
        private static DataTable ExecuteSPReader(string StoredProcedure, string tableName, params DictionaryEntry[] ParamName)
        {

            SqlConnection Connection = new SqlConnection(CONN_STRING);
            SqlCommand comm = new SqlCommand(StoredProcedure);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 0;
            foreach (DictionaryEntry paramV in ParamName)
            {
                comm.Parameters.AddWithValue(paramV.Key.ToString(), paramV.Value);
            }
            SqlDataAdapter resultDA = new SqlDataAdapter();
            resultDA.SelectCommand = comm;
            resultDA.SelectCommand.Connection = Connection;
            /*Connection.Open();*/
            DataSet resultDS = new DataSet();
            try
            {
                Connection.Open();
                resultDA.Fill(resultDS, tableName);
            }
            catch (Exception ex)
            {
                // Xử lý exception ở đây, ví dụ:
                Console.WriteLine($"Lỗi khi mở kết nối: {ex.Message}");
                return null; // hoặc xử lý theo nhu cầu của bạn
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            /*try
            {
                resultDA.Fill(resultDS, tableName);
            }
            catch { return null; }
            finally
            {
                Connection.Close();
            }*/
            return resultDS.Tables[0];
        }

        public DataTable GetOrderbyDate(DateTime fromDate, DateTime toDate)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime), fromDate);
            parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime), toDate);

            DictionaryEntry[] myArr = new DictionaryEntry[parameters.Count];
            parameters.CopyTo(myArr, 0);

            return ExecuteSPReader("GetOrderbyDate", "BillData", myArr);
        }


    }
}
