using DAO;
using DTO;
using System.Data;
namespace BUS
{
    public class ProductTypeBUS
    {
        public static DataTable loadProductTypeList()
        {
            return ProductTypeDAO.loadProductTypeList();
        }
        public static void insertProductType(ProductTypeDTO producttype)
        {
            ProductTypeDAO.insertProductType(producttype);
        }

        public static void updateProductType(ProductTypeDTO producttype)
        {
            ProductTypeDAO.updateProductType(producttype);
        }

        public static void deleteProductType(ProductTypeDTO producttype)
        {
            ProductTypeDAO.deleteProductType(producttype);
        }
    }
}
