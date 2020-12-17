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
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
        }
        public string str;
        public string STR
        {
            get { return str; }
            set { str = value; }
        }
        CustomerDTO KhachHang = new CustomerDTO();
        OrderBillDTO pdh = new OrderBillDTO();
        BillDTO hd = new BillDTO();
        ProductDTO sp = new ProductDTO();
        double thanhtien = 0;
        double thanhtienhd = 0;
        double TongTien = 0;
        int soluong=0;
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            load_data();
        }

        //load du lieu
        private void load_data()
        {
            dgvDMSanPham_PDH.DataSource = dgvDanhMucSP_HD.DataSource = ProductBUS.loadProductList();
            dgvKhachHang.DataSource = CustomerBUS.loadCustomerList();
            cbxNhanvien.DataSource = StaffBUS.loadStaffList();
            cbxNhanvien.DisplayMember = "ten_nv";
            cbxNhanvien.ValueMember = "ma_nv";
            cbxNVLHD.DataSource = StaffBUS.loadStaffList();
            cbxNVLHD.DisplayMember = "ten_nv";
            cbxNVLHD.ValueMember = "ma_nv";
            dgvCTPDH.DataSource = OrderBillBUS.loadOrderBillListByID(pdh);
            dgvCTHD.DataSource = BillBUS.loadBillListByID(hd);
            cbxLoaiSP.DataSource = ProductTypeBUS.loadProductTypeList();
            cbxLoaiSP.DisplayMember = "ten_loai_sp";
            cbxLoaiSP.ValueMember = "ma_loai";
            cbxlsp.DataSource = ProductTypeBUS.loadProductTypeList();
            cbxlsp.DisplayMember = "ten_loai_sp";
            cbxlsp.ValueMember = "ma_loai";
        }
        #region Khach hang
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTimKhachHang.SelectedIndex == 0)
                lblKhachHang.Text = "Nhập mã khách hàng";
            if (cbxTimKhachHang.SelectedIndex == 1)
                lblKhachHang.Text = "Nhập tên khách hàng";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerDTO kh = new CustomerDTO();
                if (cbxTimKhachHang.SelectedIndex == 0)
                {
                    kh.customerID = txtTimKhachHang.Text;
                    if (CustomerBUS.searchByCustomerID(kh).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvKhachHang.DataSource = CustomerBUS.searchByCustomerID(kh);
                }
                if (cbxTimKhachHang.SelectedIndex == 1)
                {
                    kh.customerName = txtTimKhachHang.Text;
                    if (CustomerBUS.searchByCUstomerName(kh).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvKhachHang.DataSource = CustomerBUS.searchByCUstomerName(kh);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_ThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaKH.Text=="")
                {
                    txtMaKH.Focus();
                }
                else
                {

                    CustomerDTO kh = new CustomerDTO();
                    KhachHang.customerName = kh.customerName = txtTenKH.Text;
                    KhachHang.customerID = kh.customerID = txtMaKH.Text;
                    kh.phoneNumber = txtDTKH.Text;
                    kh.address = txtDCKH.Text;
                    CustomerBUS.insertCustomer(kh);
                    load_data();
                    txtMaKhachHang_PDH.Text=txtTenKHHD.Text = txtTenKH.Text;
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerDTO kh = new CustomerDTO();
                kh.customerName = txtTenKH.Text;
                kh.customerID = txtMaKH.Text;
                kh.phoneNumber = txtDTKH.Text;
                kh.address = txtDCKH.Text;
                CustomerBUS.updateCustomer(kh);
                load_data();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaKH.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
                txtTenKH.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
                txtDCKH.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                txtDTKH.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        private void dgvDMSanPham_PDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaSanPham.Text = dgvDMSanPham_PDH.CurrentRow.Cells[0].Value.ToString();
                txtTenSanPham.Text = dgvDMSanPham_PDH.CurrentRow.Cells[2].Value.ToString();
                txtDG.Text = dgvDMSanPham_PDH.CurrentRow.Cells[4].Value.ToString();
                cbxLoaiSP.Text = dgvDMSanPham_PDH.CurrentRow.Cells[1].Value.ToString();
                sp.productID = dgvDMSanPham_PDH.CurrentRow.Cells[0].Value.ToString();
                sp.productName = dgvDMSanPham_PDH.CurrentRow.Cells[2].Value.ToString();
                sp.typeID = cbxLoaiSP.SelectedValue.ToString();
                sp.countUnit = dgvDMSanPham_PDH.CurrentRow.Cells[3].Value.ToString();
                sp.cost = double.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[4].Value.ToString());
                sp.warrantyTime = int.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[5].Value.ToString());
                soluong = int.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[6].Value.ToString());
                sp.producer = dgvDMSanPham_PDH.CurrentRow.Cells[7].Value.ToString();
                txtTT.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSoLuong.Text != "")
                {
                    if (int.Parse(txtSoLuong.Text) > int.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[6].Value.ToString()))
                    {
                        MessageBox.Show("Bạn nhập quá số lượng !", "Thông báo", MessageBoxButtons.OK);
                        txtSoLuong.Focus();
                    }
                    else
                    {
                        double s = double.Parse(txtDG.Text) * double.Parse(txtSoLuong.Text);
                        txtTT.Text = s.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn phải nhập số nguyên !", "Thông báo", MessageBoxButtons.OK);
                txtSoLuong.Focus();
            }
        }

        private void btnTimPDH_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO Sp = new ProductDTO();
                Sp.productID = txtMaSanPham_PDH.Text;
                dgvDMSanPham_PDH.DataSource = ProductBUS.searchProductByID(Sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMa_PDH.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã phiếu !", "Thông báo");
                    txtMa_PDH.Focus();
                }
                else
                {
                    pdh.orderbillID = txtMa_PDH.Text;
                    thanhtien += double.Parse(txtTT.Text);
                    OrderBillDetailDTO ctpdh = new OrderBillDetailDTO();
                    ctpdh.orderbillID = txtMa_PDH.Text;
                    ctpdh.productID = txtMaSanPham.Text;
                    ctpdh.quantity = int.Parse(txtSoLuong.Text);
                    ctpdh.totalcost = double.Parse(txtTT.Text);
                    OrderBillBUS.insertOrderBillDetail(ctpdh);
                    sp.quantity = soluong - int.Parse(txtSoLuong.Text);
                    ProductBUS.updateProduct(sp);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);

                    TongTien = TongTien + double.Parse(txtTT.Text.Trim());
                    txtTongthanhtoan.Text = ""+TongTien+"";

                    load_data();
                    txtMaSP.Clear();
                    txtTT.Clear();
                    txtTenSP_PDH.Clear();
                    txtSoLuong.Clear();
                    txtDG.Clear();
                    txtTenSP_PDH.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnThemCTHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMHD.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã phiếu !", "Thông báo");
                    txtMHD.Focus();
                }
                else
                {
                    hd.billID = txtMHD.Text;
                    thanhtienhd += double.Parse(txtThanhTien.Text);
                    BillDetailDTO cthd = new BillDetailDTO();
                    cthd.billID = txtMHD.Text;
                    cthd.productID = sp.productID = txtMaSP.Text;
                    cthd.quantity = sp.quantity = int.Parse(txtSL.Text);
                    cthd.totalcost = double.Parse(txtThanhTien.Text);
                    BillBUS.insertBillDetail(cthd);
                    sp.quantity = soluong - int.Parse(txtSL.Text);
                    ProductBUS.updateProduct(sp);
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);

                    TongTien = TongTien + double.Parse(txtThanhTien.Text.Trim());
                    txtTTTHD.Text = "" + TongTien + "";

                    load_data();
                    txtMaSP.Clear();
                    txtThanhTien.Clear();
                    txtTenSP.Clear();
                    txtSL.Clear();
                    txtDonGia.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDanhMucSP_HD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaSP.Text = dgvDanhMucSP_HD.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvDanhMucSP_HD.CurrentRow.Cells[2].Value.ToString();
                txtDonGia.Text = dgvDanhMucSP_HD.CurrentRow.Cells[4].Value.ToString();
                cbxlsp.Text = dgvDMSanPham_PDH.CurrentRow.Cells[1].Value.ToString();
                sp.productID = dgvDMSanPham_PDH.CurrentRow.Cells[0].Value.ToString();
                sp.productName = dgvDMSanPham_PDH.CurrentRow.Cells[2].Value.ToString();
                sp.typeID = cbxLoaiSP.SelectedValue.ToString();
                sp.countUnit = dgvDMSanPham_PDH.CurrentRow.Cells[3].Value.ToString();
                sp.cost = double.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[4].Value.ToString());
                sp.warrantyTime = int.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[5].Value.ToString());
                soluong = int.Parse(dgvDMSanPham_PDH.CurrentRow.Cells[6].Value.ToString());
                sp.producer = dgvDMSanPham_PDH.CurrentRow.Cells[7].Value.ToString();
                txtThanhTien.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSL.Text != "")
                {
                    if (int.Parse(txtSL.Text) > int.Parse(dgvDanhMucSP_HD.CurrentRow.Cells[6].Value.ToString()))
                    {
                        MessageBox.Show("Bạn nhập quá số lượng !", "Thông báo", MessageBoxButtons.OK);
                        txtSoLuong.Focus();
                    }
                    else
                    {
                        double s = double.Parse(txtDonGia.Text) * double.Parse(txtSL.Text);
                        txtThanhTien.Text = s.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn phải nhập số nguyên !", "Thông báo", MessageBoxButtons.OK);
                txtSoLuong.Focus();
            }
        }

        private void btnThem_HD_Click(object sender, EventArgs e)
        {
            try
            {
                TongTien = 0;
                BillDTO hd = new BillDTO();
                hd.year = dtpNgayLapHD.Value.Year;
                hd.month = dtpNgayLapHD.Value.Month;
                hd.day = dtpNgayLapHD.Value.Day;
                hd.billID = txtMHD.Text;

                string ma = txtTenKHHD.Text;
                CustomerDTO kHang = new CustomerDTO();
                kHang = CustomerBUS.searchCustomer(ma);
                hd.customerID = kHang.customerID;

                hd.staffID = cbxNVLHD.SelectedValue.ToString();
                BillBUS.insertBill(hd);
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                load_data();
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
                pdh.totalcost = double.Parse(txtTongthanhtoan.Text);
                OrderBillBUS.updateCost(pdh);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTTTHD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                hd.totalcost = double.Parse(txtTTTHD.Text);
                BillBUS.updateCost(hd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemPDH_Click(object sender, EventArgs e)
        {
            try
            {
                TongTien = 0;

                OrderBillDTO pdh = new OrderBillDTO();
                pdh.year = dtpNgayLap_PDH.Value.Year;
                pdh.month = dtpNgayLap_PDH.Value.Month;
                pdh.day = dtpNgayLap_PDH.Value.Day;
                pdh.orderbillID = txtMa_PDH.Text;
                
                string ma = txtMaKhachHang_PDH.Text;
                CustomerDTO kHang = new CustomerDTO();
                kHang = CustomerBUS.searchCustomer(ma);
                pdh.customerID = kHang.customerID;
                
                pdh.staffID = cbxNhanvien.SelectedValue.ToString();
                OrderBillBUS.insertOrderBill(pdh);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnxoaspn_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                txtMaSanPham.Text = dgvCTPDH.CurrentRow.Cells[1].Value.ToString();
                sp.productID = txtMaSanPham.Text;
                OrderBillDTO pdhn = new OrderBillDTO();
                pdhn.orderbillID = txtMa_PDH.Text;
                OrderBillBUS.delete_sppdh(sp,pdhn);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                txtMaSP.Text = dgvCTHD.CurrentRow.Cells[1].Value.ToString();
                sp.productID = txtMaSP.Text;
                BillDTO hdm = new BillDTO();
                hdm.billID = txtMHD.Text;
                BillBUS.deleteBillDetailProduct(sp,hdm);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInPDH_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DocumentName = txtMa_PDH.Text+dtpNgayLap_PDH.Text;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string ma = txtMaKhachHang_PDH.Text;
                CustomerDTO kHang = new CustomerDTO();
                kHang = CustomerBUS.searchCustomer(ma);

                int x = 10,y=30;
                e.Graphics.DrawString("PHIẾU ĐẶT HÀNG",new Font("Times New Roman",20),Brushes.AliceBlue,new Point(250,x));
                e.Graphics.DrawString("Mã phiếu:" + txtMa_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x+=20));
                e.Graphics.DrawString("Tên nhân viên:" + cbxNhanvien.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Mã khách hàng:" + txtMaKhachHang_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Tên khách hàng:" + kHang.customerName, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Ngày lập" + dtpNgayLap_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                e.Graphics.DrawString("Tên sản phẩm", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                e.Graphics.DrawString("Mã sản phẩm -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Số lượng -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(150, x));
                e.Graphics.DrawString("Giá -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(270, x));
                e.Graphics.DrawString("Thành tiền", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(400, x));

                for (int i = 0; i < dgvCTPDH.RowCount-1; i++)
                {
                    e.Graphics.DrawString(dgvCTPDH.Rows[i].Cells[2].Value.ToString(), new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                    e.Graphics.DrawString(dgvCTPDH.Rows[i].Cells[1].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                    e.Graphics.DrawString(dgvCTPDH.Rows[i].Cells[3].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(150, x));
                    e.Graphics.DrawString(dgvCTPDH.Rows[i].Cells[4].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(270, x));
                    e.Graphics.DrawString(dgvCTPDH.Rows[i].Cells[5].Value.ToString(), new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(400, x));

                    e.Graphics.DrawString("", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                }
                e.Graphics.DrawString("Tổng: " + txtTongthanhtoan.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(500, x += 20));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIn_HD_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument2;
            printDocument2.DocumentName = txtMHD.Text+dtpNgayLapHD.Text;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string ma = txtTenKHHD.Text;
                CustomerDTO kHang = new CustomerDTO();
                kHang = CustomerBUS.searchCustomer(ma);

                int x = 10, y = 30;
                e.Graphics.DrawString("HÓA ĐƠN", new Font("Times New Roman", 20), Brushes.AliceBlue, new Point(250, x));
                e.Graphics.DrawString("Mã phiếu:" + txtMa_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Tên nhân viên:" + cbxNhanvien.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Mã khách hàng:" + txtMaKhachHang_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Tên khách hàng:" + kHang.customerName, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Ngày lập" + dtpNgayLap_PDH.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                e.Graphics.DrawString("Tên sản phẩm", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                e.Graphics.DrawString("Mã sản phẩm -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                e.Graphics.DrawString("Số lượng -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(150, x));
                e.Graphics.DrawString("Giá -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(270, x));
                e.Graphics.DrawString("Thành tiền", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(400, x));
                for (int i = 0; i < dgvCTHD.RowCount - 1; i++)
                {
                    e.Graphics.DrawString(dgvCTHD.Rows[i].Cells[2].Value.ToString(), new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));

                    e.Graphics.DrawString(dgvCTHD.Rows[i].Cells[1].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                    e.Graphics.DrawString(dgvCTHD.Rows[i].Cells[3].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(150, x));
                    e.Graphics.DrawString(dgvCTHD.Rows[i].Cells[4].Value.ToString() + " -- ", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(270, x));
                    e.Graphics.DrawString(dgvCTHD.Rows[i].Cells[5].Value.ToString(), new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(400, x));

                    e.Graphics.DrawString("", new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(30, x += 20));
                }
                e.Graphics.DrawString("Tổng: " + txtTTTHD.Text, new Font("Times New Roman", 13), Brushes.AliceBlue, new Point(500, x += 20));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
