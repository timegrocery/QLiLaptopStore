using DAO;
using DTO;
using System.Data;
namespace BUS
{
    public class ProductBUS
    {
        public static DataTable loadProductList()
        {
            return ProductDAO.loadProductList();
        }
        public static void insertProduct(ProductDTO product)
        {
            ProductDAO.insertProduct(product);
        }

        public static void updateProduct(ProductDTO product)
        {
            ProductDAO.updateProduct(product);
        }
        public static void deleteProduct(ProductDTO product)
        {
            ProductDAO.deleteProduct(product);
        }
        public static DataTable searchProductByID(ProductDTO product)
        {
            return ProductDAO.searchProductByID(product);
        }
        public static DataTable searchProductByProducer(ProductDTO product)
        {
            return ProductDAO.searchProductByProducer(product);
        }
        public static DataTable searchProductByProductType(ProductTypeDTO producttype)
        {
            return ProductDAO.searchProductByProductType(producttype);
        }
        public static DataTable searchProductByName(ProductDTO product)
        {
            return ProductDAO.searchProductByName(product);
        }

        public static ProductDTO searchProductByID(string productid)
        {
            return ProductDAO.searchProductByID(productid);
        }

    }
}
