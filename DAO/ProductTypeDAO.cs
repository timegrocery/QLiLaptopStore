using System;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class ProductTypeDAO
    {
        static MySqlConnection cnn = null;
        public static DataTable loadProductTypeList()
        {
            try
            {
                DataTable dt = new DataTable();
                string select = "SELECT * FROM db_nhom_sp";
                cnn = DataProvider.ConnectData();
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
        /// them vao csdl 
        /// </summary>
        /// <param name="producttype"></param>
        public static void insertProductType(ProductTypeDTO producttype)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("insert into db_nhom_sp values ('{0}','{1}');",producttype.typeID,producttype.productTypeName);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void updateProductType(ProductTypeDTO producttype)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("UPDATE db_nhom_sp SET ten_loai_sp='{1}' WHERE ma_loai='{0}';", producttype.typeID, producttype.productTypeName);
                DataProvider.Execute(cnn, update);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteProductType(ProductTypeDTO producttype)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string update = string.Format("DELETE FROM db_nhom_sp WHERE ma_loai='{0}';", producttype.typeID, producttype.productTypeName);
                DataProvider.Execute(cnn, update);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
