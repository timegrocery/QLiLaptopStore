using System;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class SupplierDAO
    {
        static MySqlConnection cnn = null;
        public static DataTable loadSupplierList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT * FROM db_nha_cung_cap";
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
        /// them ncc
        /// </summary>
        /// <param name="supplier"></param>
        public static void insertSupplier(SupplierDTO supplier)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_nha_cung_cap(ma_ncc,ten_ncc,sdt_ncc,email) values ('{0}','{1}','{2}','{3}')", supplier.supplierID, supplier.supplierName, supplier.supplierPhone, supplier.email);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// update nha cung cap
        /// </summary>
        /// <param name="supplier"></param>
        public static void updateSupplier(SupplierDTO supplier)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("UPDATE db_nha_cung_cap SET ten_ncc='{1}',sdt_ncc='{2}',email='{3}' WHERE ma_ncc='{0}'", supplier.supplierID, supplier.supplierName, supplier.supplierPhone, supplier.email);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// delete NCC
        /// </summary>
        /// <param name="ncc"></param>
        public static void deleteSupplier(SupplierDTO ncc)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("DELETE FROM db_nha_cung_cap WHERE ma_ncc='{0}'", ncc.supplierID);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo ten ncc
        /// </summary>
        /// <returns></returns>
        public static DataTable searchSupplierByName(SupplierDTO ncc)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = string.Format("SELECT * FROM db_nha_cung_cap WHERE ten_ncc = '{0}'",ncc.supplierName);
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
        /// tìm theo ma ncc
        /// </summary>
        /// <returns></returns>
        public static DataTable searchSupplierByID(SupplierDTO ncc)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = string.Format("SELECT * FROM db_nha_cung_cap WHERE ma_ncc = '{0}'", ncc.supplierID);
                dt = DataProvider.loadDatabase(select, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
