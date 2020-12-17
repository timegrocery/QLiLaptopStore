using DAO;
using DTO;
using System.Data;

namespace BUS
{
    public class BillBUS
    {
        public static DataTable loadBillList()
        {
            return BillDAO.loadBillList();
        }
        public static DataTable loadBillDetailList()
        {
            return BillDetailDAO.loadBillDetailList();
        }
        public static DataTable loadBillListByID(BillDTO bill)
        {
            return BillDetailDAO.loadBillDetailByID(bill);
        }
        public static void deleteBill(BillDTO bill)
        {
            BillDAO.deleteBill(bill);
        }
        public static void updateBillDetail(BillDetailDTO billdetail)
        {
            BillDetailDAO.updateBillDetail(billdetail);
        }
        public static DataTable searchByBillID(BillDTO bill)
        {
            return BillDAO.searchByBillID(bill);
        }
        public static DataTable searchBillByCreationDate(BillDTO bill)
        {
            return BillDAO.searchBillByCreationDate(bill);
        }
        public static DataTable searchBillByCustomerName(CustomerDTO customer)
        {
            return BillDAO.searchBillByCustomerName(customer);
        }
        public static DataTable searchBillByCreatorName(StaffDTO staff)
        {
            return BillDAO.searchBillByCreatorName(staff);
        }

        public static void insertBill(BillDTO bill)
        {
            BillDAO.insertBill(bill);
        }

        public static void insertBillDetail(BillDetailDTO billdetail)
        {
            BillDetailDAO.insertBillDetail(billdetail);
        }
        public static void updateCost(BillDTO bill)
        {
            BillDAO.updateCost(bill);
        }

        public static void deleteBillDetailProduct(BillDTO bill)
        {
            BillDetailDAO.deleteBillDetailProduct(bill);
        }

        public static void deleteBillDetailProduct(ProductDTO product)
        {
            BillDetailDAO.deleteBillDetailProduct(product);
        }
        public static void deleteBillDetailProduct(ProductDTO product, BillDTO bill)
        {
            BillDetailDAO.deleteBillDetailProduct(product,bill);
        }
    }
}
