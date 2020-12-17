using System.Data;
using MySqlConnector;
namespace DAO
{
    public class JobDAO
    {
        static MySqlConnection cnn = null;
        public static DataTable Load_DSCV()
        {
            DataTable dt = new DataTable();
            string select = "SELECT * FROM db_chucvu";
            cnn = DataProvider.ConnectData();
            dt = DataProvider.loadDatabase(select, cnn);
            cnn.Close();
            return dt;
        }
    }
}
