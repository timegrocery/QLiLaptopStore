using System;
using DAO;
using DTO;
using System.Data;
namespace BUS
{
    public class CustomerBUS
    {
        public static DataTable loadCustomerList()
        {
            return CustomerDAO.loadCustomerList();
        }
        public static void insertCustomer(CustomerDTO customer)
        {
            CustomerDAO.insertCustomer(customer);
        }
        public static void updateCustomer(CustomerDTO customer)
        {
            CustomerDAO.updateCustomer(customer);
        }
        public static void deleteCustomer(CustomerDTO customer)
        {
            CustomerDAO.deleteCustomer(customer);
        }

        public static DataTable searchByCustomerID(CustomerDTO customer)
        {
            return CustomerDAO.searchByCustomerID(customer);
        }

        public static DataTable searchByCUstomerName(CustomerDTO customer)
        {
            return CustomerDAO.searchByCustomerName(customer);
        }

        public static CustomerDTO searchCustomer(String customerID)
        {
            return CustomerDAO.searchCustomer(customerID);
        }
    }
}
