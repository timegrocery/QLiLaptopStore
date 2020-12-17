using System;
using System.Collections.Generic;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class CustomerDAO
    {
        static MySqlConnection cnn = null;
        public static DataTable loadCustomerList()
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string select = "SELECT * FROM db_khach_hang";
                dt = DataProvider.loadDatabase(select,cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void insertCustomer(CustomerDTO kh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_khach_hang(ma_kh,ten_kh,diachi,sdt) value ('{0}','{1}', '{2}','{3}');", kh.customerID, kh.customerName, kh.address, kh.phoneNumber);
                DataProvider.Execute(cnn,insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateCustomer(CustomerDTO kh)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("UPDATE db_khach_hang SET ten_kh='{1}',diachi='{2}',sdt='{3}'WHERE ma_kh='{0}';", kh.customerID, kh.customerName, kh.address, kh.phoneNumber);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteCustomer(CustomerDTO kh)
        {
            try
            {
                
                BillDTO bill = new BillDTO();
                OrderBillDTO orderbill = new OrderBillDTO();
                List<string> list = new List<string>();
                string customertodelete = string.Format("DELETE FROM db_khach_hang WHERE ma_kh='{0}'", kh.customerID);
                string billtodelete = string.Format("SELECT ma_hd FROM db_hoa_don WHERE ma_kh = '{0}'",kh.customerID);
                string orderbilltodelete = string.Format("SELECT ma_pdh FROM db_phieu_dat_hang WHERE ma_kh = '{0}'", kh.customerID);
                list = deleteInformation(billtodelete);
                for (int i = 0; i < list.Count; i++)
                {
                    bill.billID = list[0];
                    BillDetailDAO.deleteBillDetailProduct(bill);
                    BillDAO.deleteBill(bill);
                }
                list = deleteInformation(orderbilltodelete);
                for (int i = 0; i < list.Count; i++)
                {
                    orderbill.orderbillID = list[0];
                    BillOrderDetailDAO.deleteProductInOrderBill(orderbill);
                    OrderBillDAO.deleteOrderBill(orderbill);
                }
                cnn = DataProvider.ConnectData();
                DataProvider.Execute(cnn, customertodelete);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// xóa các liên kết của khách hàng trong csdl
        /// </summary>
        private static List<string> deleteInformation(string sqlquery)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                DataTable dt = new DataTable();
                List<string> ds = new List<string>();
                dt = DataProvider.loadDatabase(sqlquery,cnn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ds.Add(dt.Rows[i].ItemArray.GetValue(0).ToString());
                }
                cnn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static DataTable searchByCustomerID(CustomerDTO customer)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string Search_ma = string.Format("SELECT * FROM db_khach_hang WHERE ma_kh='{0}'", customer.customerID);
                dt = DataProvider.loadDatabase(Search_ma,cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable searchByCustomerName(CustomerDTO kh)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string namesearch = string.Format("SELECT * FROM db_khach_hang WHERE ten_kh='{0}'", kh.customerName);
                dt = DataProvider.loadDatabase(namesearch, cnn);
                cnn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CustomerDTO searchCustomer(String MaKhachHang)
        {
            try
            {
                DataTable dt = new DataTable();
                CustomerDTO customer = new CustomerDTO();
                cnn = DataProvider.ConnectData();
                string Search_DTOKH = string.Format("SELECT *From db_khach_hang WHERE ma_kh = '" + MaKhachHang +"'");
                dt = DataProvider.loadDatabase(Search_DTOKH, cnn);
                cnn.Close();
                customer.customerID = dt.Rows[0][0].ToString();
                customer.customerName = dt.Rows[0][1].ToString();
                customer.address = dt.Rows[0][2].ToString();
                customer.phoneNumber = dt.Rows[0][3].ToString();
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
