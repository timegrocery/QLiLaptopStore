using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
namespace GUI_Tier
{
    public partial class NhapHang : Form
    {
        public NhapHang()
        {
            InitializeComponent();
        }
        //tạo biến kiểm tra dang nhap va dang xuat
        public string str;
        public string STR
        {
            get { return str; }
            set { str = value; }
        }
        private bool RB(string str)
        {
            if (str == "")
                return false;
            return true;
        }
        //biến tính tổng giá trị của 1 phiếu nhập
        double thanhtien = 0;
        double TongTien = 0;
        //biến dùng để tạo 1 phiếu nhập mới
        bool check = true;

        //DTO
        ImportBill2 pnh = new ImportBill2();

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                TongTien = 0;
                ImportBill2 PN = new ImportBill2();
                PN.year = dtpPN.Value.Year;
                PN.month = dtpPN.Value.Month;
                PN.day = dtpPN.Value.Day;
                PN.importbillID = txtMaPN.Text;
                /*
                string ma = txtTenKHHD.Text;
                KhachHangDTO kHang = new KhachHangDTO();
                kHang = KhachHangBUS.Search_KH(ma);
                hd.ma_kh = kHang.ma_kh;
                */
                PN.supplierID = cbxNCC.SelectedValue.ToString();
                PN.staffID = cbxNVLPN.SelectedValue.ToString();
                ImportBillBUS.insertImportBill(PN);
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemsp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPN.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã phiếu !", "Thông báo");
                    txtMaPN.Focus();
                }
                else
                { 
                    pnh.importbillID = txtMaPN.Text;
                    ImportBillDTO ctpn = new ImportBillDTO();
                    ctpn.ImportBillID = txtMaPN.Text;
                    ctpn.ProductID = txtMSP.Text;
                    ctpn.quantity = int.Parse(txtSL.Text);
                    ctpn.cost = double.Parse(txtGSP.Text);
                    ctpn.total = ctpn.quantity * ctpn.cost;

                    ImportBillBUS.insertImportBillDetail(ctpn);
                    ProductDTO sp = new ProductDTO();
                    sp = ProductBUS.searchProductByID(ctpn.ProductID);
                    sp.quantity = sp.quantity + ctpn.quantity;
                    txtDVT.Text = sp.typeID;
                    ProductBUS.updateProduct(sp);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);

                    TongTien = TongTien + ctpn.total;
                    txtTongthanhtoan.Text = "" + TongTien + "";

                    load_data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTongthanhtoan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ImportBill2 pn = new ImportBill2();
                pn.importbillID = txtMaPN.Text;
                pn.totalcost = long.Parse(txtTongthanhtoan.Text);
                ImportBillBUS.updateImportBill(pn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NhapHang_Load(object sender, EventArgs e)
        {
            load_data();
        }
        void load_data()
        {
            ImportBill2 pn = new ImportBill2();
            pn.importbillID = txtMaPN.Text;
            dgvSanPham.DataSource= ProductBUS.loadProductList();
            cbxLSP.DataSource = cbxLoaiSP.DataSource = ProductTypeBUS.loadProductTypeList();
            dgvChitietPN.DataSource = ImportBillBUS.loadImportBillIDList(pnh);
            cbxLSP.DisplayMember = cbxLoaiSP.DisplayMember = "ten_loai_sp";
            cbxLSP.ValueMember = cbxLoaiSP.ValueMember = "ma_loai";
            cbxNVLPN.DataSource = StaffBUS.loadStaffList();
            cbxNVLPN.DisplayMember = "ten_nv";
            cbxNVLPN.ValueMember = "ma_nv";
            cbxNCC.DataSource = SupplierBUS.loadSupplierList();
            cbxNCC.DisplayMember = "ten_ncc";
            cbxNCC.ValueMember = "ma_ncc";
            groupBox2.Enabled = false;
        }
        #region Sản phẩm
        private void btnThemLoaisp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductTypeDTO loaisp = new ProductTypeDTO();
                if (!RB(txtMaLoaisp.Text))
                {
                    txtMaLoaisp.Focus();
                    errorProvider1.SetError(txtMaLoaisp, "Error!");
                }
                else
                {
                    loaisp.typeID = txtMaLoaisp.Text;
                    loaisp.productTypeName = txtTenLoaiSP.Text;
                    ProductTypeBUS.insertProductType(loaisp);
                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OK);
                    load_data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaLoaisp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductTypeDTO loaisp = new ProductTypeDTO();
                loaisp.typeID = cbxLoaiSP.SelectedValue.ToString();
                ProductTypeBUS.deleteProductType(loaisp);
                load_data();
                MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaLoaisp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductTypeDTO loaisp = new ProductTypeDTO();
                loaisp.typeID = txtMaLoaisp.Text;
                loaisp.productTypeName = txtTenLoaiSP.Text;
                ProductTypeBUS.updateProductType(loaisp);
                load_data();
                MessageBox.Show("Sửa thành công !", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /// <summary>
            /// thực biện tìm kiếm sản phẩm
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        
        }

        private void btnSuaSP_Click_1(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                sp.productID = txtMasp.Text;
                sp.productName = txtTenSP.Text;
                sp.quantity = int.Parse(txtSLSP.Text);
                sp.typeID = cbxLoaiSP.SelectedValue.ToString();
                sp.warrantyTime = int.Parse(txtTGBH.Text);
                sp.cost = long.Parse(txtGiasp.Text);
                sp.countUnit = txtDVTSP.Text;
                sp.producer = txtHSX.Text;
                ProductBUS.updateProduct(sp);
                load_data();
                MessageBox.Show("Sửa thành công !", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sửa không thành công !", "Thông báo", MessageBoxButtons.OK);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTimsp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                ProductTypeDTO loaisp = new ProductTypeDTO();
                if (cbxTimTheoSP.SelectedIndex == 0)
                {
                    sp.productID = txtTimTheosp.Text;
                    if (ProductBUS.searchProductByID(sp).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    else
                        dgvSanPham.DataSource = ProductBUS.searchProductByID(sp);
                }
                if (cbxTimTheoSP.SelectedIndex == 1)
                {
                    loaisp.productTypeName = txtTimTheosp.Text;
                    if (ProductBUS.searchProductByProductType(loaisp).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    else
                        dgvSanPham.DataSource = ProductBUS.searchProductByProductType(loaisp);
                }
                if (cbxTimTheoSP.SelectedIndex == 2)
                {
                    sp.productName = txtTimTheosp.Text;
                    if (ProductBUS.searchProductByName(sp).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    else
                        dgvSanPham.DataSource = ProductBUS.searchProductByName(sp);
                }
                if (cbxTimTheoSP.SelectedIndex == 3)
                {
                    sp.producer = txtTimTheosp.Text;
                    if (ProductBUS.searchProductByProducer(sp).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    else
                        dgvSanPham.DataSource = ProductBUS.searchProductByProducer(sp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxTimTheoSP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxTimTheoSP.SelectedIndex == 0)
                lblTimSP.Text = "Nhập mã sản phẩm:";
            if (cbxTimTheoSP.SelectedIndex == 1)
                lblTimSP.Text = "Nhập loại sản phẩm:";
            if (cbxTimTheoSP.SelectedIndex == 2)
                lblTimSP.Text = "Nhập tên sản phẩm:";
            if (cbxTimTheoSP.SelectedIndex == 3)
                lblTimSP.Text = "Nhập hãng sản xuất sản phẩm:";
        }
        #endregion

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMasp.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
                cbxLoaiSP.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
                txtDVTSP.Text = dgvSanPham.CurrentRow.Cells[3].Value.ToString();
                txtGiasp.Text = dgvSanPham.CurrentRow.Cells[4].Value.ToString();
                txtTGBH.Text = dgvSanPham.CurrentRow.Cells[5].Value.ToString();
                txtSLSP.Text = dgvSanPham.CurrentRow.Cells[6].Value.ToString();
                txtHSXSP.Text = dgvSanPham.CurrentRow.Cells[7].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                groupBox2.Enabled = true;
            else
                groupBox2.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult rs = new DialogResult();
            rs = MessageBox.Show("Bạn muốn quay lại màn hình đăng nhập ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.OK)
            {
                AccountDTO tk = new AccountDTO();
                tk.username = str;
                AccountBUS.logout(tk);
                QuanLy ql = new QuanLy();
                this.Hide();
                frmDangnhap dn = new frmDangnhap();
                dn.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        private void btn_xoaspn_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                txtMSP.Text = dgvChitietPN.CurrentRow.Cells[1].Value.ToString();
                sp.productID = txtMSP.Text;
                ImportBill2 pnm = new ImportBill2();
                pnm.importbillID = txtMaPN.Text;
                ImportBillBUS.deleteProductInImportBill(sp, pnm);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DocumentName = txtMaPN.Text + dtpPN.Text;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int x = 10, y = 30;
                e.Graphics.DrawString("PHIẾU ĐẶT HÀNG", new Font("Times New Roman", 20), Brushes.Black, new Point(250, x));
                e.Graphics.DrawString("Mã phiếu:" + txtMaPN.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                e.Graphics.DrawString("Tên nhân viên:" + cbxNVLPN.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                if (checkBox1.Checked)
                {
                    e.Graphics.DrawString("Tên nhà cung cấp:" + txtTenNCC.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                }
                else
                    e.Graphics.DrawString("Tên nhà cung cấp:" + cbxNCC.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                e.Graphics.DrawString("Ngày lập" + dtpPN.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                e.Graphics.DrawString("Tên sản phẩm", new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));

                e.Graphics.DrawString("Mã sản phẩm -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                e.Graphics.DrawString("Số lượng -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(150, x));
                e.Graphics.DrawString("Giá -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(270, x));
                e.Graphics.DrawString("Thành tiền -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(400, x));
                e.Graphics.DrawString("", new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                for (int i = 0; i < dgvChitietPN.RowCount - 1; i++)
                {
                    e.Graphics.DrawString(dgvChitietPN.Rows[i].Cells[2].Value.ToString(), new Font("Times New Roman", 13), Brushes.Black, new Point(30, x +=20));

                    e.Graphics.DrawString(dgvChitietPN.Rows[i].Cells[1].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                    e.Graphics.DrawString(dgvChitietPN.Rows[i].Cells[3].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(150, x));
                    e.Graphics.DrawString(dgvChitietPN.Rows[i].Cells[4].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.Black, new Point(270, x));
                    e.Graphics.DrawString(dgvChitietPN.Rows[i].Cells[5].Value.ToString(), new Font("Times New Roman", 13), Brushes.Black, new Point(400, x));
                    e.Graphics.DrawString("", new Font("Times New Roman", 13), Brushes.Black, new Point(30, x += 20));
                }
                e.Graphics.DrawString("Tổng: " + txtTongthanhtoan.Text, new Font("Times New Roman", 13), Brushes.Black, new Point(500, x += 20));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SupplierDTO ncc = new SupplierDTO();
            ncc.supplierID = txtMaNCC.Text;
            ncc.supplierName = txtTenNCC.Text;
            ncc.email = txtEmailNCC.Text;
            ncc.supplierPhone = txtSDTNCC.Text;
            SupplierBUS.insertSupplier(ncc);
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtEmailNCC.Clear();
            txtSDTNCC.Clear();
            load_data();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
