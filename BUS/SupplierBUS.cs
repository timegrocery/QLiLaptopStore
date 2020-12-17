using DAO;
using DTO;
using System.Data;
namespace BUS
{
    public class SupplierBUS
    {
        public static DataTable loadSupplierList()
        {
            return SupplierDAO.loadSupplierList();
        }
        public static void insertSupplier(SupplierDTO supplier)
        {
            SupplierDAO.insertSupplier(supplier);
        }

        public static void updateSupplier(SupplierDTO supplier)
        {
            SupplierDAO.updateSupplier(supplier);
        }

        public static void deleteSupplier(SupplierDTO supplier)
        {
            SupplierDAO.deleteSupplier(supplier);
        }

        public static DataTable searchSupplierByID(SupplierDTO supplier)
        {
            return SupplierDAO.searchSupplierByID(supplier);
        }
        public static DataTable searchSupplierByName(SupplierDTO supplier)
        {
            return SupplierDAO.searchSupplierByName(supplier);
        }
    }
}
