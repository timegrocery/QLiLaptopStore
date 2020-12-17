using System;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class BillDAO
    {
        static MySqlConnection cnn = null;
        /// <summary>
        /// load danh sach hóa đơn
        /// </summary>
        /// <returns></returns>
        public static DataTable loadBillList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT ma_hd,db_nhanvien.ten_nv,db_khach_hang.ten_kh,ngay_lap,tonggiatri,thanh_toan FROM  db_hoa_don inner join db_nhanvien on db_hoa_don.ma_nv = db_nhanvien.ma_nv inner join db_khach_hang on db_hoa_don.ma_kh = db_khach_hang.ma_kh";
                dt = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return dt;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteBill(BillDTO hd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string delete = string.Format("DELETE FROM chi_tiet_hd WHERE ma_hd = '{0}'; DELETE FROM db_hoa_don WHERE ma_hd = '{0}'; ", hd.billID);
                DataProvider.Execute(cnn,delete);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo mã hóa đơn
        /// </summary>
        /// <param name="hd"></param>
        public static DataTable searchByBillID(BillDTO hd)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string Search_MaHD = string.Format("SELECT ma_hd,db_nhanvien.ten_nv,db_khach_hang.ten_kh,ngay_lap,da_thanh_toan,con_lai,tonggiatri,thanh_toan FROM  db_hoa_don inner join db_nhanvien on db_hoa_don.ma_nv = db_nhanvien.ma_nv inner join db_khach_hang on db_hoa_don.ma_kh = db_khach_hang.ma_kh WHERE ma_hd = '{0}'", hd.billID);
                dt = DataProvider.loadDatabase(Search_MaHD, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// tìm theo ngày lập hóa đơn
        /// </summary>
        /// <param name="hd"></param>
        public static DataTable searchBillByCreationDate(BillDTO hd)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string Search_NgayLapHD = string.Format("SELECT ma_hd,db_nhanvien.ten_nv,db_khach_hang.ten_kh,ngay_lap,da_thanh_toan,con_lai,tonggiatri,thanh_toan FROM  db_hoa_don inner join db_nhanvien on db_hoa_don.ma_nv = db_nhanvien.ma_nv inner join db_khach_hang on db_hoa_don.ma_kh = db_khach_hang.ma_kh WHERE ngay_lap = '{0}/{1}/{2}'", hd.year,hd.month,hd.day);
                dt = DataProvider.loadDatabase(Search_NgayLapHD,cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// tìm theo tên khách hàng
        /// </summary>
        /// <param name="hd"></param>
        public static DataTable searchBillByCustomerName(CustomerDTO kh)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string Search_TenKHinHD = string.Format("SELECT ma_hd,db_nhanvien.ten_nv,db_khach_hang.ten_kh,ngay_lap,da_thanh_toan,con_lai,tonggiatri,thanh_toan FROM  db_hoa_don inner join db_nhanvien on db_hoa_don.ma_nv = db_nhanvien.ma_nv inner join db_khach_hang on db_hoa_don.ma_kh = db_khach_hang.ma_kh WHERE db_khach_hang.ten_kh = '{0}'", kh.customerName);
                dt = DataProvider.loadDatabase(Search_TenKHinHD,cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// tìm theo tên nhân viên lập hóa đơn
        /// </summary>
        /// <param name="hd"></param>
        public static DataTable searchBillByCreatorName(StaffDTO nv)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string Search_TenKHinHD = string.Format("SELECT ma_hd,db_nhanvien.ten_nv,db_khach_hang.ten_kh,ngay_lap,da_thanh_toan,con_lai,tonggiatri,thanh_toan FROM  db_hoa_don inner join db_nhanvien on db_hoa_don.ma_nv = db_nhanvien.ma_nv inner join db_khach_hang on db_hoa_don.ma_kh = db_khach_hang.ma_kh WHERE db_nhanvien.ten_nv = '{0}'", nv.staffName);
                dt = DataProvider.loadDatabase(Search_TenKHinHD, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertBill(BillDTO hd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_hoa_don(ma_hd,ma_nv,ma_kh,ngay_lap,da_thanh_toan,con_lai,tonggiatri,thanh_toan) values ('{0}','{1}','{2}','{3}/{4}/{5}','{6}','{7}','{8}',{9});",
                                                                hd.billID,hd.staffID,hd.customerID,hd.year,hd.month,hd.day,hd.paid,hd.left,hd.totalcost,hd.pay);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateCost(BillDTO hd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("UPDATE db_hoa_don SET tonggiatri = '{1}' WHERE ma_hd='{0}'",hd.billID,hd.totalcost);
                DataProvider.Execute(cnn, update);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
