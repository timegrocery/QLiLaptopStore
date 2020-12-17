using DTO;
using DAO;
using System.Data;
namespace BUS
{
    public class StatisticBUS
    {
        public static DataTable loadBillDetailList()
        {
            return StatisticDAO.loadBillDetailList();
        }
        public static DataTable loadImportBillDetailList()
        {
            return StatisticDAO.loadImportBillDetailList();
        }
        public static DataTable loadImportBillDate(ImportBill2 importbill1,ImportBill2 importbill2)
        {
            return StatisticDAO.loadImportBillDate(importbill1,importbill2);
        }
        public static DataTable loadBillCreationDate(BillDTO bill1, BillDTO bill2)
        {
            return StatisticDAO.loadBillCreationDate(bill1, bill2);
        }
        public static double sumImportBill()
        {
            return StatisticDAO.sumImportBillDetail();
        }
        public static double sumBill()
        {
            return StatisticDAO.sumBillDetail();
        }
        public static double sumImportBillByDay(ImportBill2 importbill1, ImportBill2 importbill2)
        {
            return StatisticDAO.sumImportBillCostByDay(importbill1,importbill2);
        }
        public static double sumBillByDay(BillDTO bill1, BillDTO bill2)
        {
            return StatisticDAO.sumBillByDay(bill1,bill2);
        }
        public static DataTable loadByImportBillCreationDate(ImportBill2 importbill1, ImportBill2 importbill2)
        {
            return StatisticDAO.loadByImportBillCreationDate(importbill1, importbill2);
        }
        public static DataTable loadByBillCreationDate(BillDTO bill1, BillDTO bill2)
        {
            return StatisticDAO.loadByBillCreationDate(bill1, bill2);
        }
        public static double sumImportBillCost()
        {
            return StatisticDAO.sumImportBillCost();
        }
        public static double sumBillCost()
        {
            return StatisticDAO.sumBillCost();
        }
        public static double sumImportBillCostByDay(ImportBill2 importbill1, ImportBill2 importbill2)
        {
            return StatisticDAO.sumImportBillCostByDay(importbill1, importbill2);
        }
        public static double sumBillByDay2(BillDTO bill1, BillDTO bill2)
        {
            return StatisticDAO.sumBillByDay(bill1, bill2);
        }
        public static double sumQuantityByDay(ImportBill2 importbill1, ImportBill2 importbill2)
        {
            return StatisticDAO.sumQuantityByDay(importbill1, importbill2);
        }
        public static double sumImportBillDetailByID(ImportBill2 importbill1)
        {
            return StatisticDAO.sumImportBillDetailByID(importbill1);
        }
        public static double sumBillByID(BillDTO bill)
        {
            return StatisticDAO.sumBillByID(bill);
        }

        public static double sumOrderBillByID(OrderBillDTO orderbill)
        {
            return StatisticDAO.sumOrderBillByID(orderbill);
        }
    }
}
