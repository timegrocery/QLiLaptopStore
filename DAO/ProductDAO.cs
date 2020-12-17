using System;
using DTO;
using System.Data;
using MySqlConnector;
namespace DAO
{
    public class ProductDAO
    {
        static MySqlConnection cnn = null;
        /// <summary>
        /// load danh sách từ csdl lên form
        /// </summary>
        /// <returns></returns>
        public static DataTable loadProductList()
        {
            try
            {
                DataTable dt = new DataTable();
                string select = "SELECT ma_sp,db_nhom_sp.ten_loai_sp,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat FROM db_sanpham inner join db_nhom_sp on db_sanpham.ma_loai=db_nhom_sp.ma_loai";
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
        /// <param name="sp"></param>
        public static void insertProduct(ProductDTO sp)
        {
            try
            {
                string insert = string.Format("insert into db_sanpham(ma_sp,ma_loai,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat) values ('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}');",
                    sp.productID,sp.typeID,sp.productName,sp.countUnit,sp.cost,sp.warrantyTime,sp.quantity,sp.producer);
                cnn = DataProvider.ConnectData();
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// update csdl
        /// </summary>
        /// <param name="sp"></param>
        public static void updateProduct(ProductDTO sp)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("UPDATE `db_sanpham` SET `ma_loai`='{0}', " +
                    "`ten_sp`='{1}', " +
                    "`don_vi_tinh`='{2}', " +
                    "`gia_sp`='{3}', " +
                    "`thoi_gian_bh`='{4}', " +
                    "`soluong`='{5}', " +
                    "`hang_san_xuat`='{6}' WHERE `ma_sp`='{7}';",
                    sp.typeID, sp.productName, sp.countUnit, sp.cost, sp.warrantyTime, sp.quantity, sp.producer, sp.productID);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// delete du lieu
        /// </summary>
        /// <param name="sp"></param>
        public static void deleteProduct(ProductDTO sp)
        {
            try
            {
                cnn = DataProvider.ConnectData();
                string insert = string.Format("DELETE FROM `db_sanpham` WHERE ma_sp='{0}' ;",sp.productID);
                DataProvider.Execute(cnn, insert);
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo mã sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static DataTable searchProductByID(ProductDTO product)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string masp= string.Format("SELECT ma_sp,db_nhom_sp.ten_loai_sp,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat FROM db_sanpham inner join db_nhom_sp on db_sanpham.ma_loai=db_nhom_sp.ma_loai WHERE db_sanpham.ma_sp='{0}';",product.productID);
                dt = DataProvider.loadDatabase(masp, cnn);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Tìm theo tên sản phẩm
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static DataTable searchProductByName(ProductDTO sp)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string masp = string.Format("SELECT ma_sp,db_nhom_sp.ten_loai_sp,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat FROM db_sanpham inner join db_nhom_sp on db_sanpham.ma_loai=db_nhom_sp.ma_loai WHERE db_sanpham.ten_sp='{0}';", sp.productName);
                dt = DataProvider.loadDatabase(masp, cnn);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// tìm theo hãng sản xuất
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static DataTable searchProductByProducer(ProductDTO product)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string masp = string.Format("SELECT ma_sp,db_nhom_sp.ten_loai_sp,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat FROM db_sanpham inner join db_nhom_sp on db_sanpham.ma_loai=db_nhom_sp.ma_loai WHERE db_sanpham.hang_san_xuat='{0}';", product.producer);
                dt = DataProvider.loadDatabase(masp, cnn);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //tìm theo loại
        public static DataTable searchProductByProductType(ProductTypeDTO producttype)
        {
            try
            {
                DataTable dt = new DataTable();
                cnn = DataProvider.ConnectData();
                string masp = string.Format("SELECT ma_sp,db_nhom_sp.ten_loai_sp,ten_sp,don_vi_tinh,gia_sp,thoi_gian_bh,soluong,hang_san_xuat FROM db_sanpham inner join db_nhom_sp on db_sanpham.ma_loai=db_nhom_sp.ma_loai WHERE db_nhom_sp.ten_loai_sp='{0}';", producttype.productTypeName);
                dt = DataProvider.loadDatabase(masp, cnn);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Tỉm Sản Phẩm
        public static ProductDTO searchProductByID(string idtosearch)
        {
            try
            {
                DataTable dt = new DataTable();
                ProductDTO sp = new ProductDTO();
                cnn = DataProvider.ConnectData();
                string searchsp = string.Format("SELECT * FROM `db_sanpham` WHERE db_sanpham.ma_sp='" + idtosearch + "'");
                dt = DataProvider.loadDatabase(searchsp, cnn);
                cnn.Close();
                sp.productID = dt.Rows[0][0].ToString();
                sp.typeID = dt.Rows[0][1].ToString();
                sp.productName = dt.Rows[0][2].ToString();
                sp.countUnit = dt.Rows[0][3].ToString();
                sp.cost =  double.Parse(dt.Rows[0][4].ToString());
                sp.warrantyTime = int.Parse(dt.Rows[0][5].ToString());
                sp.quantity = int.Parse(dt.Rows[0][6].ToString());
                sp.producer = dt.Rows[0][7].ToString();
                return sp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
