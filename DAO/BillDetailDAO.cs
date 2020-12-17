using System;
using DTO;
using System.Data;
using MySqlConnector;

namespace DAO
{
    public class BillDetailDAO
    {
        static MySqlConnection cnn = null;
        /// <summary>
        /// load danh sách chi tiết hóa đơn
        /// </summary>
        /// <returns></returns>
        public static DataTable loadBillDetailList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT ma_hd,db_sanpham.ma_sp,db_sanpham.ten_sp,so_luong,db_sanpham.gia_sp,thanh_tien FROM chi_tiet_hd inner join db_sanpham on chi_tiet_hd.ma_sp = db_sanpham.ma_sp";
                dt = DataProvider.loadDatabase(select,cnn);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// load danh sách theo mã hóa đơn
        /// </summary>
        /// <returns></returns>
        public static DataTable loadBillDetailByID(BillDTO bill)
        {
            try
            {
                DataTable detail = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = string.Format("SELECT ma_hd,db_sanpham.ma_sp,db_sanpham.ten_sp,so_luong,db_sanpham.gia_sp,thanh_tien FROM chi_tiet_hd inner join db_sanpham on chi_tiet_hd.ma_sp = db_sanpham.ma_sp WHERE chi_tiet_hd.ma_hd = '{0}'", bill.billID);
                detail = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return detail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// sữa thông tin chi tiết hóa đơn
        /// </summary>
        /// <param name="billdetail"></param>
        public static void updateBillDetail(BillDetailDTO billdetail)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format(" UPDATE chi_tiet_hd SET ma_sp = '{2}', so_luong='{3}' WHERE ma_hd = '{1}' and id = '{0}'; UPDATE chi_tiet_hd SET thanh_tien='{4}' WHERE id = '{0}'; ", billdetail.ID, billdetail.billID, billdetail.productID, billdetail.quantity,billdetail.totalcost);
                DataProvider.Execute(cnn, update);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertBillDetail(BillDetailDTO cthd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into chi_tiet_hd(ma_hd,ma_sp,so_luong,thanh_tien) values ('{0}','{1}','{2}','{3}');",
                                                            cthd.billID,cthd.productID,cthd.quantity,cthd.totalcost);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteBillDetailProduct(BillDTO hd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_hd WHERE ma_hd='{0}';", hd.billID);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(id,cnn);
                DataProvider.Execute(cnn, id);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteBillDetailProduct(ProductDTO sp)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_hd WHERE ma_sp='{0}';", sp.productID);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(id, cnn);
                DataProvider.Execute(cnn, id);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteBillDetailProduct(ProductDTO sp, BillDTO hd)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_hd WHERE ma_hd='{0}' AND ma_sp='{1}';", hd.billID, sp.productID);
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(id, cnn);
                DataProvider.Execute(cnn, id);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
