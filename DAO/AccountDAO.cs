using System;
using DTO;
using MySqlConnector;
using System.Data;

namespace DAO
{
    public class AccountDAO
    {
        static MySqlConnection cnn;
        /// <summary>
        /// thêm 1 tài khoản vào csdl
        /// </summary>
        /// <param name="tk"></param>
        public static void insertAccount(AccountDTO tk)
        {
            try
            {
                string Insert = string.Format("INSERT INTO db_taikhoan(username,pass,ma_cv,trang_thai) VALUES ('{0}','{1}','{2}','false');", tk.username, tk.password, tk.jobID);
                cnn = DataProvider.ConnectData();
                DataProvider.Execute(cnn, Insert);
                cnn.Close();
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// kiem tra ten tai khoan, mat khau va trang thai khi dang nhap vao
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string login(AccountDTO account)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string load = string.Format("SELECT db_taikhoan.ma_cv FROM db_taikhoan inner join db_chucvu on db_taikhoan.ma_cv = db_chucvu.ma_cv WHERE username='{0}' and pass='{1}'and trang_thai='false'", account.username, account.password);
                MySqlCommand cmd = new MySqlCommand(load,cnn);
                account.jobID = cmd.ExecuteScalar().ToString();
                cnn.Close();
                if (account.jobID!="")
                {
                    //string select = string.Format("SELECT * FROM db_taikhoan,db_chucvu WHERE username='{0}' and pass='{1}'and db_taikhoan.ma_cv = '{2}' and trang_thai='{3}';UPDATE db_taikhoan SET trang_thai = true WHERE username='{0}'", tk.username, tk.password, tk.ma_cv, tk.trang_thai);
                    //DataProvider.Execute(cnn, select);
                    //cnn.Close();
                    return account.jobID;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void logout(AccountDTO tk)
        {
            cnn = DataProvider.ConnectData();
            string select = string.Format("UPDATE db_taikhoan SET trang_thai = false WHERE username='{0}'",tk.username);
            DataProvider.Execute(cnn, select);
            cnn.Close();
        }
    }
}
