using System;
using MySqlConnector;
using System.Data;

namespace DAO
{
    public class DataProvider
    {
        public static MySqlConnection ConnectData()
        {
            try
            {
                //string strcnn = "Server=localhost;Database=btbdt;Port=3306;User ID=root;Password=; CHARSET=utf8";
                string strcnn = "Server=localhost; Port=3306; Database=btbdt; Uid=root; Pwd=;CHARSET=utf8";
                MySqlConnection cnn = new MySqlConnection(strcnn);
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <param name="strMysql"></param>
        /// <param name="cnn"></param>
        /// <returns></returns>
        public static DataTable loadDatabase(string strMysql, MySqlConnection cnn)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(strMysql, cnn);
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Thực thi câu lệnh mysql
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="strMysql"></param>
        public static void Execute(MySqlConnection cnn, string strMysql)
        {
            MySqlCommand cmd = new MySqlCommand(strMysql, cnn);
            cmd.ExecuteReader();
        }
    }
}
