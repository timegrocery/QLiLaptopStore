using System;
using DTO;
using MySqlConnector;
using System.Data;
namespace DAO
{
    public class ImportBillDetailDAO
    {
        static MySqlConnection cnn = null;
        /// <summary>
        /// load toàn danh sách phiếu nhập
        /// </summary>
        /// <returns></returns>
        public static DataTable loadImportBillDetailList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT db_chi_tiet_pn.ma_phieu_nhap,db_sanpham.ma_sp,db_sanpham.ten_sp,so_luong,gianhap,tong FROM db_chi_tiet_pn inner join db_phieu_nhap on db_chi_tiet_pn.ma_phieu_nhap = db_phieu_nhap.ma_phieu_nhap inner join db_sanpham on db_chi_tiet_pn.ma_sp = db_sanpham.ma_sp";
                dt = DataProvider.loadDatabase(select,cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// load danh sách theo mã phiếu nhập
        /// </summary>
        /// <returns></returns>
        public static DataTable loadImportBillIDList(ImportBill2 pn)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = string.Format("SELECT db_chi_tiet_pn.ma_phieu_nhap,db_sanpham.ma_sp,db_sanpham.ten_sp,so_luong,gianhap,tong FROM db_chi_tiet_pn inner join db_phieu_nhap on db_chi_tiet_pn.ma_phieu_nhap = db_phieu_nhap.ma_phieu_nhap inner join db_sanpham on db_chi_tiet_pn.ma_sp = db_sanpham.ma_sp WHERE db_chi_tiet_pn.ma_phieu_nhap='{0}'",pn.importbillID);
                dt = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateImportBillDetail(ImportBillDTO ctpn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("UPDATE db_chi_tiet_pn SET ma_sp='{1}',so_luong='{2}',gianhap='{3}',tong='{4}' WHERE id='{0}'",
                                                    ctpn.ID,ctpn.ProductID,ctpn.quantity,ctpn.cost,ctpn.total);
                DataProvider.Execute(cnn,update);
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void insertImportBillDetail(ImportBillDTO ctpn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_chi_tiet_pn(ma_sp,ma_phieu_nhap,so_luong,gianhap,tong) values('{0}','{1}','{2}','{3}','{4}');",
                                                                        ctpn.ProductID,ctpn.ImportBillID,ctpn.quantity,ctpn.cost,ctpn.total);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        public static void deleteProductInImportBill(ImportBill2 pn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM db_chi_tiet_pn WHERE ma_phieu_nhap='{0}';", pn.importbillID);
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

        public static void deleteProductInImportBill(ProductDTO sp)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM db_chi_tiet_pn WHERE ma_sp='{0}';", sp.productID);
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

        public static void deleteProductInImportBill(ProductDTO sp ,ImportBill2 pn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string id = string.Format("Delete FROM db_chi_tiet_pn WHERE ma_phieu_nhap='{0}' AND ma_sp='{1}';", pn.importbillID, sp.productID);
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
