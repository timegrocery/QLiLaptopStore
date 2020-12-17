using DAO;
using DTO;
using System.Data;
namespace BUS
{
    public class OrderBillBUS
    {
        public static DataTable loadOrderBillList()
        {
            return OrderBillDAO.loadOrderBillList();
        }
        public static DataTable loadOrderBillDetailList()
        {
            return BillOrderDetailDAO.loadOrderBillDetailList();
        }
        public static void insertOrderBill(OrderBillDTO orderbill)
        {
            OrderBillDAO.insertOrderBill(orderbill);
        }
        public static void updateCost(OrderBillDTO orderbill)
        {
            OrderBillDAO.updateCost(orderbill);
        }
        public static void deleteOrderBill(OrderBillDTO orderbill)
        {
            OrderBillDAO.deleteOrderBill(orderbill);
        }
        public static DataTable loadOrderBillListByID(OrderBillDTO orderbill)
        {
            return BillOrderDetailDAO.loadOrderBillListByID(orderbill);
        }
        public static void updateOrderBillDetail(OrderBillDetailDTO orderbilldetail)
        {
            BillOrderDetailDAO.updateOrderBillDetail(orderbilldetail);
        }

        public static DataTable searchOrderBillByID(OrderBillDTO orderbill)
        {
            return OrderBillDAO.searchOrderBillByID(orderbill);
        }
        public static DataTable searchOrderBillByCreationDate(OrderBillDTO orderbill)
        {
            return OrderBillDAO.searchOrderBillByCreationDate(orderbill);
        }
        public static DataTable searchByCustomerName(CustomerDTO customername)
        {
            return OrderBillDAO.searchByCUstomerName(customername);
        }
        public static DataTable searchByCreatorName(StaffDTO staff)
        {
            return OrderBillDAO.searchByCreatorName(staff);
        }
        public static void insertOrderBillDetail(OrderBillDetailDTO orderbilldetail)
        {
            BillOrderDetailDAO.insertOrderBillDetail(orderbilldetail);
        }
        public static void deleteProductInOrderBill(OrderBillDTO orderbill)
        {
            BillOrderDetailDAO.deleteProductInOrderBill(orderbill);
        }
        public static void deleteProductInOrderBill(ProductDTO product)
        {
            BillOrderDetailDAO.deleteProductInOrderBill(product);
        }

        public static void delete_sppdh(ProductDTO product, OrderBillDTO orderbill)
        {
            BillOrderDetailDAO.deleteProductInOrderBill(product,orderbill);
        }
    }
}
