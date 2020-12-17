using System;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class BillOrderDetailDAO
    {
        static MySqlConnection cnn = null;
        /// <summary>
        /// load danh sach phiếu đặt hàng
        /// </summary>
        /// <returns></returns>
        public static DataTable loadOrderBillDetailList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT ma_pdh,chi_tiet_pdh.ma_sp,db_sanpham.ten_sp,chi_tiet_pdh.soluong,db_sanpham.gia_sp,thanhtien FROM chi_tiet_pdh inner join db_sanpham on chi_tiet_pdh.ma_sp = db_sanpham.ma_sp";
                dt = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// load theo mã phiếu đặt hàng
        /// </summary>
        /// <returns></returns>
        public static DataTable loadOrderBillListByID(OrderBillDTO pdh)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = string.Format("SELECT ma_pdh,chi_tiet_pdh.ma_sp,db_sanpham.ten_sp,chi_tiet_pdh.soluong,db_sanpham.gia_sp,thanhtien FROM chi_tiet_pdh inner join db_sanpham on chi_tiet_pdh.ma_sp = db_sanpham.ma_sp WHERE ma_pdh='{0}'", pdh.orderbillID);
                dt = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// sửa chi tiết phiếu đặt hàng
        /// </summary>
        /// <param name="ctpdh"></param>
        public static void updateOrderBillDetail(OrderBillDetailDTO ctpdh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("UPDATE chi_tiet_pdh SET ma_sp='{1}',soluong={2},thanhtien='{3}' WHERE id = '{0}'",ctpdh.ID,ctpdh.productID,ctpdh.quantity,ctpdh.totalcost);
                DataProvider.Execute(cnn, update);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertOrderBillDetail(OrderBillDetailDTO ctpdh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into chi_tiet_pdh(ma_sp,ma_pdh,soluong,thanhtien) values('{0}','{1}','{2}','{3}');",ctpdh.productID,ctpdh.orderbillID,ctpdh.quantity,ctpdh.totalcost);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteProductInOrderBill(OrderBillDTO pdh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_pdh WHERE ma_pdh='{0}';", pdh.orderbillID);
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

        public static void deleteProductInOrderBill(ProductDTO sp)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_pdh WHERE ma_sp='{0}';", sp.productID);
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

        public static void deleteProductInOrderBill(ProductDTO sp, OrderBillDTO pdh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM chi_tiet_pdh WHERE ma_pdh='{0}' AND ma_sp='{1}';",pdh.orderbillID, sp.productID);
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
