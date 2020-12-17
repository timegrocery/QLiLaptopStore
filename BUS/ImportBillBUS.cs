using System.Data;
using DAO;
using DTO;
namespace BUS
{
    public class ImportBillBUS
    {
        public static DataTable loadImportBillList()
        {
            return ImportBillDAO.loadImportBillList();
        }
        public static DataTable loadImportBillDetailList()
        {
            return ImportBillDetailDAO.loadImportBillDetailList();
        }
        public static DataTable loadImportBillIDList(ImportBill2 importbill)
        {
            return ImportBillDetailDAO.loadImportBillIDList(importbill);
        }
        public static void updateImportBillDetail(ImportBillDTO importbilldetail)
        {
            ImportBillDetailDAO.updateImportBillDetail(importbilldetail);
        }
        public static DataTable searchImportBillByID(ImportBill2 importbill)
        {
            return ImportBillDAO.searchImportBillByID(importbill);
        }

        public static DataTable searchImportBillByCreatorName(StaffDTO staff)
        {
            return ImportBillDAO.searchImportBillByCreatorName(staff);
        }
        public static DataTable searchImportBillByCreationDate(ImportBill2 importbill)
        {
            return ImportBillDAO.searchImportBillByCreationDate(importbill);
        }
        public static DataTable searchImportBillBySupplierID(SupplierDTO supplier)
        {
            return ImportBillDAO.searchImportBillBySupplierID(supplier);
        }
        public static void deleteImportBill(ImportBill2 importbill)
        {
            ImportBillDAO.deleteImportBill(importbill);
        }
        public static void insertImportBill(ImportBill2 importbill)
        {
            ImportBillDAO.insertImportBill(importbill);
        }
        public static void insertImportBillDetail(ImportBillDTO importbilldetail)
        {
            ImportBillDetailDAO.insertImportBillDetail(importbilldetail);
        }
        public static void updateImportBill(ImportBill2 importbill)
        {
            ImportBillDAO.updateImportBill(importbill);
        }
        public static void deleteProductInImportBill(ImportBill2 importbill)
        {
            ImportBillDetailDAO.deleteProductInImportBill(importbill);
        }
        public static void deleteProductInImportBill(ProductDTO product)
        {
            ImportBillDetailDAO.deleteProductInImportBill(product);
        }
        public static void deleteProductInImportBill(ProductDTO product, ImportBill2 importbill)
        {
            ImportBillDetailDAO.deleteProductInImportBill(product,importbill);
        }
    }
}
