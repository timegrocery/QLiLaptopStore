using System;
using System.Data;
using DTO;
using MySqlConnector;
namespace DAO
{
    public class ImportBillDAO
    {
        static MySqlConnection cnn = null;
        public static DataTable loadImportBillList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT ma_phieu_nhap,db_nhanvien.ten_nv,db_nha_cung_cap.ten_ncc,ngay_lap_pn,tong_tien FROM db_phieu_nhap inner join db_nhanvien on db_phieu_nhap.ma_nv = db_nhanvien.ma_nv inner join db_nha_cung_cap on db_phieu_nhap.ma_ncc = db_nha_cung_cap.ma_ncc";
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
        /// xoa thong tin 1 phieu nhap
        /// </summary>
        /// <param name="importBill"></param>
        public static void deleteImportBill(ImportBill2 importBill)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string select = string.Format("delete from db_chi_tiet_pn where ma_phieu_nhap='{0}';delete from db_phieu_nhap where ma_phieu_nhap='{0}';", importBill.importbillID);
                DataProvider.Execute(cnn, select);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo mã phiếu nhập
        /// </summary>
        /// <param name="importbill"></param>
        /// <returns></returns>
        public static DataTable searchImportBillByID(ImportBill2 importbill)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string IDtoSearch = string.Format("SELECT ma_phieu_nhap,db_nhanvien.ten_nv,db_nha_cung_cap.ten_ncc,ngay_lap_pn,tong_tien FROM db_phieu_nhap inner join db_nhanvien on db_phieu_nhap.ma_nv = db_nhanvien.ma_nv inner join db_nha_cung_cap on db_phieu_nhap.ma_ncc = db_nha_cung_cap.ma_ncc WHERE db_phieu_nhap.ma_phieu_nhap='{0}'", importbill.importbillID);
                DataTable dt = new DataTable();
                dt = DataProvider.loadDatabase(IDtoSearch, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo nv lập phiếu
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static DataTable searchImportBillByCreatorName(StaffDTO staff)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string idtosearch = string.Format("SELECT ma_phieu_nhap,db_nhanvien.ten_nv,db_nha_cung_cap.ten_ncc,ngay_lap_pn,tong_tien FROM db_phieu_nhap inner join db_nhanvien on db_phieu_nhap.ma_nv = db_nhanvien.ma_nv inner join db_nha_cung_cap on db_phieu_nhap.ma_ncc = db_nha_cung_cap.ma_ncc WHERE db_nhanvien.ten_nv='{0}'", staff.staffName);
                DataTable dt = new DataTable();
                dt = DataProvider.loadDatabase(idtosearch, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm ngày lập phiếu
        /// </summary>
        /// <param name="importbill"></param>
        /// <returns></returns>
        public static DataTable searchImportBillByCreationDate(ImportBill2 importbill)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string Search_ma = string.Format("SELECT ma_phieu_nhap,db_nhanvien.ten_nv,db_nha_cung_cap.ten_ncc,ngay_lap_pn,tong_tien FROM db_phieu_nhap inner join db_nhanvien on db_phieu_nhap.ma_nv = db_nhanvien.ma_nv inner join db_nha_cung_cap on db_phieu_nhap.ma_ncc = db_nha_cung_cap.ma_ncc WHERE db_phieu_nhap.ngay_lap_pn='{0}/{1}/{2}'", importbill.year, importbill.month, importbill.day);
                DataTable dt = new DataTable();
                dt = DataProvider.loadDatabase(Search_ma, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo nhà cung cấp
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static DataTable searchImportBillBySupplierID(SupplierDTO supplier)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string idtosearch = string.Format("SELECT ma_phieu_nhap,db_nhanvien.ten_nv,db_nha_cung_cap.ten_ncc,ngay_lap_pn,tong_tien FROM db_phieu_nhap inner join db_nhanvien on db_phieu_nhap.ma_nv = db_nhanvien.ma_nv inner join db_nha_cung_cap on db_phieu_nhap.ma_ncc = db_nha_cung_cap.ma_ncc WHERE db_nha_cung_cap.ten_ncc='{0}'", supplier.supplierName);
                DataTable dt = new DataTable();
                dt = DataProvider.loadDatabase(idtosearch, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// thêm danh 1 phiếu nhập
        /// </summary>
        /// <param name="pn"></param>
        public static void insertImportBill(ImportBill2 pn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_phieu_nhap(ma_phieu_nhap,ma_nv,ma_ncc,ngay_lap_pn,tong_tien) values ('{0}','{1}','{2}','{3}/{4}/{5}','{6}');",
                                                                        pn.importbillID,pn.staffID,pn.supplierID,pn.year,pn.month,pn.day,pn.totalcost);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateImportBill(ImportBill2 pn)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("UPDATE db_phieu_nhap SET tong_tien='{1}' WHERE ma_phieu_nhap='{0}';",pn.importbillID,pn.totalcost);
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
