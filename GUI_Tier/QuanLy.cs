using System;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

using BUS;
using DTO;
namespace GUI_Tier
{
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }

        public string str;
        public string STR
        {
            get { return str; }
            set { str = value; }
        }//dùng để giử username của 1 tài khoản

        private void QuanLy_Load(object sender, EventArgs e)
        {
            load_data();
        }
        //load dữ liệu lên form
        private void load_data()
        {
            dgvNhanvien.DataSource = StaffBUS.loadStaffList();
            cbxCongViec.DataSource = JobBUS.Load_DSCV();
            dgvSanPham.DataSource = ProductBUS.loadProductList();
            cbxLoaiSP.DataSource = ProductTypeBUS.loadProductTypeList();
            dgvKhachHang.DataSource = CustomerBUS.loadCustomerList();
            dgvSPN.DataSource = dgvPhieuNhap.DataSource = ImportBillBUS.loadImportBillList();
            dgvChitietPN.DataSource = ImportBillBUS.loadImportBillDetailList();
            dgvSPX.DataSource = dgvDSHD.DataSource = BillBUS.loadBillList();
            dgvCTHD.DataSource = BillBUS.loadBillDetailList();
            dgvCTPDH.DataSource = OrderBillBUS.loadOrderBillDetailList();
            dgvDSPDH.DataSource = OrderBillBUS.loadOrderBillList();
            dgvDSSPX.DataSource = StatisticBUS.loadBillDetailList();
            dgvDSSPN.DataSource = StatisticBUS.loadImportBillDetailList();
            dgvNCC.DataSource = SupplierBUS.loadSupplierList();
            txtMaLoaisp.Clear();
            cbxLoaiSP.DisplayMember = "ten_loai_sp";
            cbxLoaiSP.ValueMember = "ma_loai";
            cbxCongViec.DisplayMember = "ten_cv";
            cbxCongViec.ValueMember = "ma_cv";
            txtTongChi.Text = StatisticBUS.sumImportBill().ToString();
            txtTongThu.Text = StatisticBUS.sumBill().ToString();
            txtTT.Text = StatisticBUS.sumBillCost().ToString();
            txtTC.Text = StatisticBUS.sumImportBillCost().ToString();
            txtThue.Text = ((StatisticBUS.sumBillCost() - StatisticBUS.sumImportBillCost()) * 1 / 10).ToString();
            txtLN.Text = (StatisticBUS.sumBillCost() - StatisticBUS.sumImportBillCost()).ToString();
            txtLN2.Text = ((StatisticBUS.sumBillCost() - StatisticBUS.sumImportBillCost()) - ((StatisticBUS.sumBillCost() - StatisticBUS.sumImportBillCost()) * 1 / 10)).ToString();
        }
        //kiểm tra ô textbox trống hay không
        private bool RB(string str)
        {
            if (str == "")
                return false;
            return true;
        }
        /// <summary>
        /// đưa ô dữ liệu đang chọn lê các textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Nhân viên
        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtManv.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[0].Value);
                txtTennv.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[1].Value);
                dateNgaysinh.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[2].Value);
                if (Convert.ToString(dgvNhanvien.CurrentRow.Cells[3].Value) == "Nam")
                {
                    rdbNam.Checked = true;
                    rdbNu.Checked = false;
                }
                else
                {
                    rdbNam.Checked = false;
                    rdbNu.Checked = true;
                }
                txtSdtnv.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[4].Value);
                txtDcnv.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[5].Value);
                txtEmailnv.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[6].Value);
                cbxCongViec.Text = Convert.ToString(dgvNhanvien.CurrentRow.Cells[7].Value);
            }
            catch (Exception ex)
            {

            }
        }
        //xóa các ô textbox
        private void button3_Click(object sender, EventArgs e)
        {
            txtManv.Focus();
            txtManv.Clear();
            txtDcnv.Clear();
            txtEmailnv.Clear();
            txtSdtnv.Clear();
            txtTennv.Clear();
            rdbNu.Checked = rdbNam.Checked = false;
        }
        /// <summary>
        /// Thêm sinh viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThemnv_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RB(txtManv.Text))
                {
                    txtManv.Focus();
                    errorProvider1.SetError(txtManv, "Erorr !");
                }
                else
                {
                    StaffDTO nv = new StaffDTO();
                    nv.staffName = txtTennv.Text;
                    nv.staffID = txtManv.Text;
                    nv.year = dateNgaysinh.Value.Year;
                    nv.month = dateNgaysinh.Value.Month;
                    nv.day = dateNgaysinh.Value.Day;
                    if (rdbNam.Checked)
                        nv.gender = "Nam";
                    else
                        nv.gender = "Nữ";
                    nv.address = txtDcnv.Text;
                    nv.jobid = cbxCongViec.SelectedValue.ToString();
                    nv.phone = txtSdtnv.Text;
                    nv.email = txtEmailnv.Text;
                    StaffBUS.insertStaff(nv);
                    load_data();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Thoát ứng dụng và đăng xuất tài khoản
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// sửa thông tin 1 nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuanv_Click(object sender, EventArgs e)
        {
            try
            {
                StaffDTO nv = new StaffDTO();
                nv.staffName = txtTennv.Text;
                nv.staffID = txtManv.Text;
                nv.year = dateNgaysinh.Value.Year;
                nv.month = dateNgaysinh.Value.Month;
                nv.day = dateNgaysinh.Value.Day;
                if (rdbNam.Checked)
                    nv.gender = "Nam";
                else
                    nv.gender = "Nữ";
                nv.address = txtDcnv.Text;
                nv.jobid = cbxCongViec.SelectedValue.ToString();
                nv.phone = txtSdtnv.Text;
                nv.email = txtEmailnv.Text;
                StaffBUS.updateStaff(nv);
                load_data();
                MessageBox.Show("Update dữ liệu thành công !", "Thông báo", MessageBoxButtons.OKCancel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// xóa thông tin 1 nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoanv_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    StaffDTO nv = new StaffDTO();
                    nv.staffID = dgvNhanvien.CurrentRow.Cells[0].Value.ToString();
                    StaffBUS.deleteStaff(nv);
                    load_data();
                    MessageBox.Show("Delete thành công !", "Thông báo", MessageBoxButtons.OK);
                }
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// xuất chuỗi label ra form tương ứng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTimkiem_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxTimkiem.SelectedIndex == 0)
            {
                label11.Text = "Nhập họ và tên nhân viên:";
            }
            if (cbxTimkiem.SelectedIndex == 1)
            {
                label11.Text = "Nhập mã nhân viên:";
            }
            if (cbxTimkiem.SelectedIndex == 2)
            {
                label11.Text = "Nhập công việc nhân viên:";
            }
        }
        /// <summary>
        /// tìm kiếm 1 nhân viên theo mã, theo tên, theo công việc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StaffDTO nv = new StaffDTO();
                JobDTO cv = new JobDTO();
                if (cbxTimkiem.SelectedIndex == 0)
                {
                    nv.staffName = txtTimkiem.Text;
                    dgvNhanvien.DataSource = StaffBUS.searchStaffByName(nv);
                    if (dgvNhanvien.RowCount < 2)
                        MessageBox.Show("Không có dữ liệu bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                }
                if (cbxTimkiem.SelectedIndex == 1)
                {
                    nv.staffID = txtTimkiem.Text;
                    dgvNhanvien.DataSource = StaffBUS.searchStaffByID(nv);
                    if (dgvNhanvien.RowCount < 2)
                        MessageBox.Show("Không có dữ liệu bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                }
                if (cbxTimkiem.SelectedIndex == 2)
                {
                    cv.jobName = txtTimkiem.Text;
                    dgvNhanvien.DataSource = StaffBUS.searchStaffByJob(cv);
                    if (dgvNhanvien.RowCount < 2)
                        MessageBox.Show("Không có dữ liệu bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //mở form đăng ký tài khoản mới
        private void đăngKýNhânViênMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangky dk = new frmDangky();
            dk.Show();
        }
        private void btnThoat_Click(object sender, EventArgs e)
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
#endregion
        #region Sản phẩm
        /// <summary>
        /// Thêm 1 sản phẩm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RB(txtMasp.Text))
                {
                    txtMasp.Focus();
                    errorProvider1.SetError(txtMasp, "Error");
                }
                else
                {
                    ProductDTO sp = new ProductDTO();
                    sp.productID = txtMasp.Text;
                    sp.productName = txtTenSP.Text;
                    sp.quantity = int.Parse(txtSLSP.Text);
                    sp.typeID = cbxLoaiSP.SelectedValue.ToString();
                    sp.warrantyTime = int.Parse(txtTGBH.Text);
                    sp.cost = long.Parse(txtGiasp.Text);
                    sp.countUnit = txtDVT.Text;
                    sp.producer = txtHSX.Text;
                    ProductBUS.insertProduct(sp);
                    load_data();
                    MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm không thành công !", "Thông báo", MessageBoxButtons.OK);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// load sản phẩm lên các ô text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMasp.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
                cbxLoaiSP.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
                txtDVT.Text = dgvSanPham.CurrentRow.Cells[3].Value.ToString();
                txtGiasp.Text = dgvSanPham.CurrentRow.Cells[4].Value.ToString();
                txtTGBH.Text = dgvSanPham.CurrentRow.Cells[5].Value.ToString();
                txtSLSP.Text = dgvSanPham.CurrentRow.Cells[6].Value.ToString();
                txtHSX.Text = dgvSanPham.CurrentRow.Cells[7].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// sửa thông tin của 1 sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuaSP_Click(object sender, EventArgs e)
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
                sp.countUnit = txtDVT.Text;
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
        /// <summary>
        /// xóa 1 sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    ProductDTO sp = new ProductDTO();
                    sp.productID = txtMasp.Text;
                    
                    BillBUS.deleteBillDetailProduct(sp);
                    OrderBillBUS.deleteProductInOrderBill(sp);
                    ImportBillBUS.deleteProductInImportBill(sp);
                    ProductBUS.deleteProduct(sp);
                    load_data();
                    MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OK);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa không thành công !", "Thông báo", MessageBoxButtons.OK);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// load danh mục tim kiếm tương ứng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTimTheoSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxTimTheoSP.SelectedIndex==0)
                lblTimSP.Text="Nhập mã sản phẩm:";
            if(cbxTimTheoSP.SelectedIndex==1)
                lblTimSP.Text="Nhập loại sản phẩm:";
            if(cbxTimTheoSP.SelectedIndex==2)
                lblTimSP.Text="Nhập tên sản phẩm:";
            if(cbxTimTheoSP.SelectedIndex==3)
                lblTimSP.Text = "Nhập hãng sản xuất sản phẩm:";
        }
        /// <summary>
        /// thực biện tìm kiếm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        #region Loại sản phẩm
        private void btnTimsp_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO sp = new ProductDTO();
                ProductTypeDTO loaisp = new ProductTypeDTO();
                if (cbxTimTheoSP.SelectedIndex == 0)
                {
                    sp.productID = txtTimTheosp.Text;
                    if (ProductBUS.searchProductByID(sp).Rows.Count==0)
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
        }

        private void cbxLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaLoaisp.Text = cbxLoaiSP.SelectedValue.ToString();
        }
        #endregion
        #region Khách hàng
        private void btnThemKH_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RB(txtMaKhachhang.Text))
                {
                    txtMaKhachhang.Focus();
                    errorProvider1.SetError(txtMaKhachhang,"Error!");
                }
                else
                {

                    CustomerDTO kh = new CustomerDTO();
                    kh.customerName = txtTenKhachHang.Text;
                    kh.customerID = txtMaKhachhang.Text;
                    kh.phoneNumber = txtsdtKH.Text;
                    kh.address = txtDiaChi.Text;
                    CustomerBUS.insertCustomer(kh);
                    load_data();
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    CustomerDTO kh = new CustomerDTO();
                    kh.customerID = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
                    CustomerBUS.deleteCustomer(kh);
                    load_data();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
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
                kh.customerName = txtTenKhachHang.Text;
                kh.customerID = txtMaKhachhang.Text;
                kh.phoneNumber = txtsdtKH.Text;
                kh.address = txtDiaChi.Text;
                CustomerBUS.updateCustomer(kh);
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                load_data();
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
                txtMaKhachhang.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
                txtTenKhachHang.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
                txtsdtKH.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cbxKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxKhachHang.SelectedIndex == 0)
                lblTimKH.Text = "Nhập mã khách hàng";
            if (cbxKhachHang.SelectedIndex == 1)
                lblTimKH.Text = "Nhập tên khách hàng";
        }


        private void btnTimKH_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerDTO kh = new CustomerDTO();
                if (cbxKhachHang.SelectedIndex == 0)
                {
                    kh.customerID = txtTimKH.Text;
                    if (CustomerBUS.searchByCustomerID(kh).Rows.Count==0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvKhachHang.DataSource=CustomerBUS.searchByCustomerID(kh);
                }
                if (cbxKhachHang.SelectedIndex == 1)
                {
                    kh.customerName = txtTimKH.Text;
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

        private void button4_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }
        #endregion
        #region Phiếu nhập
        private void button5_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }
        /// <summary>
        /// load thong tin phieu nhap len o text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ImportBill2 pn = new ImportBill2();
                pn.importbillID = txtMaPN.Text = dgvPhieuNhap.CurrentRow.Cells[0].Value.ToString();
                dgvChitietPN.DataSource = ImportBillBUS.loadImportBillIDList(pn);
                txtNVLPN.Text = dgvPhieuNhap.CurrentRow.Cells[1].Value.ToString();
                txtNCC.Text = dgvPhieuNhap.CurrentRow.Cells[2].Value.ToString();
                dtpPN.Text = dgvPhieuNhap.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Lưu thay đổi tổng giá trị PN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvChitietPN_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ImportBillDTO ctpn = new ImportBillDTO();
                ImportBill2 pn = new ImportBill2();
                ctpn.ID = int.Parse(dgvChitietPN.CurrentRow.Cells[0].Value.ToString());
                pn.importbillID = dgvChitietPN.CurrentRow.Cells[1].Value.ToString();
                ctpn.ProductID = dgvChitietPN.CurrentRow.Cells[2].Value.ToString();
                ctpn.cost = long.Parse(dgvChitietPN.CurrentRow.Cells[5].Value.ToString());
                ctpn.quantity = int.Parse(dgvChitietPN.CurrentRow.Cells[4].Value.ToString());
                ctpn.total = ctpn.cost * ctpn.quantity;
                pn.totalcost = StatisticBUS.sumImportBillDetailByID(pn);
                ImportBillBUS.updateImportBillDetail(ctpn);
                ImportBillBUS.updateImportBill(pn);
                btnSuaPN_Click(sender, e);
                load_data();
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// sua thong tin phieu nhap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuaPN_Click(object sender, EventArgs e)
        {
            try
            {
                ImportBillDTO ctpn = new ImportBillDTO();
                ImportBill2 pn = new ImportBill2();
                ctpn.ID = int.Parse(dgvChitietPN.CurrentRow.Cells[0].Value.ToString());
                pn.importbillID = dgvChitietPN.CurrentRow.Cells[1].Value.ToString();
                ctpn.ProductID = dgvChitietPN.CurrentRow.Cells[2].Value.ToString();
                ctpn.cost = long.Parse(dgvChitietPN.CurrentRow.Cells[5].Value.ToString());
                ctpn.quantity = int.Parse(dgvChitietPN.CurrentRow.Cells[4].Value.ToString());
                ctpn.total = ctpn.cost * ctpn.quantity;
                pn.totalcost = StatisticBUS.sumImportBillDetailByID(pn);
                ImportBillBUS.updateImportBillDetail(ctpn);
                ImportBillBUS.updateImportBill(pn);

                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxTimPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpTimNgayLPN.Visible = false;
            txtTimPN.Visible = true;
            if (cbxTimPN.SelectedIndex == 0)
            {
                lblTimPN.Text = "Nhập mã phiếu nhập:";
            }
            if (cbxTimPN.SelectedIndex == 1)
            {
                lblTimPN.Text = "Nhập tên nhân viên lập phiếu nhập:";
            }
            if (cbxTimPN.SelectedIndex == 2)
            {
                lblTimPN.Text = "Nhập ngày lập phiếu nhập:";
                dtpTimNgayLPN.Visible = true;
                txtTimPN.Visible = false;
            }
            if (cbxTimPN.SelectedIndex == 3)
            {
                lblTimPN.Text = "Nhập nhà cung cấp:";
            }
        }
        /// <summary>
        /// tìm kiếm phiếu nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimPN_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTimPN.SelectedIndex == 0)
                {
                    ImportBill2 pn = new ImportBill2();
                    pn.importbillID = txtTimPN.Text;
                    if (ImportBillBUS.searchImportBillByID(pn).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvPhieuNhap.DataSource = ImportBillBUS.searchImportBillByID(pn);
                }
                if (cbxTimPN.SelectedIndex == 1)
                {
                    StaffDTO nv = new StaffDTO();
                    nv.staffName = txtTimPN.Text;
                    if (ImportBillBUS.searchImportBillByCreatorName(nv).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvPhieuNhap.DataSource = ImportBillBUS.searchImportBillByCreatorName(nv);
                }
                if (cbxTimPN.SelectedIndex == 2)
                {
                    ImportBill2 pn = new ImportBill2();
                    pn.year = dtpTimNgayLPN.Value.Year;
                    pn.month = dtpTimNgayLPN.Value.Month;
                    pn.day = dtpTimNgayLPN.Value.Day;
                    if (ImportBillBUS.searchImportBillByCreationDate(pn).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvPhieuNhap.DataSource = ImportBillBUS.searchImportBillByCreationDate(pn);
                }
                if (cbxTimPN.SelectedIndex == 3)
                {
                    SupplierDTO ncc = new SupplierDTO();
                    ncc.supplierName = txtTimPN.Text;
                    if (ImportBillBUS.searchImportBillBySupplierID(ncc).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvPhieuNhap.DataSource = ImportBillBUS.searchImportBillBySupplierID(ncc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// xóa 1 phiếu nhập được chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoaPN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    ImportBill2 pn = new ImportBill2();
                    pn.importbillID = dgvChitietPN.CurrentRow.Cells[0].Value.ToString();
                    ImportBillBUS.deleteImportBill(pn);
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    load_data();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Hóa đơn
        private void cbxTimHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtbTimHD.Visible = false;
            txtTimHD.Visible = true;
            if (cbxTimHD.SelectedIndex == 0)
            {
                lblTimHD.Text = "Nhập mã hóa đơn:";
            }
            if (cbxTimHD.SelectedIndex == 1)
            {
                lblTimHD.Text = "Nhập tên nhân viên lập hóa đơn:";
            }
            if (cbxTimHD.SelectedIndex == 2)
            {
                lblTimHD.Text = "Nhập ngày lập hóa đơn:";
                dtbTimHD.Visible = true;
                txtTimHD.Visible = false;
            }
            if (cbxTimHD.SelectedIndex == 3)
            {
                lblTimHD.Text = "Nhập tên khách hàng:";

            }
        }
       
        /// <summary>
        /// load dữ liệu hóa đơn lên các ô text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDSHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BillDTO hd = new BillDTO();
                txtMaHD.Text = hd.billID = dgvDSHD.CurrentRow.Cells[0].Value.ToString();
                dgvCTHD.DataSource = BillBUS.loadBillListByID(hd);
                txtTenKH_HD.Text = dgvDSHD.CurrentRow.Cells[1].Value.ToString();
                txtNVLHD.Text = dgvDSHD.CurrentRow.Cells[2].Value.ToString();
                dtpLapHD.Text = dgvDSHD.CurrentRow.Cells[3].Value.ToString();
                txtDaThanhToan.Text = dgvDSHD.CurrentRow.Cells[4].Value.ToString();
                txtConLai.Text = dgvDSHD.CurrentRow.Cells[5].Value.ToString();
                txtTongtien.Text = dgvDSHD.CurrentRow.Cells[6].Value.ToString();
                if (bool.Parse(dgvDSHD.CurrentRow.Cells[7].Value.ToString()) == true)
                {
                    rdbThanhToan.Checked = true;
                    rdbChuaThanhToan.Checked = false;
                }
                else
                {
                    rdbThanhToan.Checked = false;
                    rdbChuaThanhToan.Checked = true;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }
        
        /// <summary>
        /// xóa hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    BillDTO hd = new BillDTO();
                    hd.billID = txtMaHD.Text;
                    BillBUS.deleteBill(hd);
                    load_data();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// sửa hóa đơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            try
            {
                BillDetailDTO cthd = new BillDetailDTO();
                BillDTO hd = new BillDTO();
                
                cthd.ID = int.Parse(dgvCTHD.CurrentRow.Cells[0].Value.ToString());
                cthd.billID = hd.billID = dgvCTHD.CurrentRow.Cells[1].Value.ToString();
                cthd.productID = dgvCTHD.CurrentRow.Cells[2].Value.ToString();
                cthd.quantity = int.Parse(dgvCTHD.CurrentRow.Cells[4].Value.ToString());
                cthd.totalcost = cthd.quantity * long.Parse(dgvCTHD.CurrentRow.Cells[5].Value.ToString());
                BillBUS.updateBillDetail(cthd);
                hd.totalcost = StatisticBUS.sumBillByID(hd);
                BillBUS.updateCost(hd);
                load_data();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //tìm hóa đơn
        private void btnTimHD_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTimHD.SelectedIndex == 0)
                {
                    BillDTO hd = new BillDTO();
                    hd.billID = txtTimHD.Text;
                    if (BillBUS.searchByBillID(hd).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    dgvDSHD.DataSource = BillBUS.searchByBillID(hd);
                }
                if (cbxTimHD.SelectedIndex == 1)
                {
                    StaffDTO nv = new StaffDTO();
                    nv.staffName = txtTimHD.Text;
                    if (BillBUS.searchBillByCreatorName(nv).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    dgvDSHD.DataSource = BillBUS.searchBillByCreatorName(nv);
                }
                if (cbxTimHD.SelectedIndex == 2)
                {
                    BillDTO hd = new BillDTO();
                    hd.year = dtbTimHD.Value.Year;
                    hd.month = dtbTimHD.Value.Month;
                    hd.day = dtbTimHD.Value.Day;
                    if (BillBUS.searchBillByCreationDate(hd).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    dgvDSHD.DataSource = BillBUS.searchBillByCreationDate(hd);
                }
                if (cbxTimHD.SelectedIndex == 3)
                {
                    CustomerDTO kh = new CustomerDTO();
                    kh.customerName = txtTimHD.Text;
                    if (BillBUS.searchBillByCustomerName(kh).Rows.Count == 0)
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    dgvDSHD.DataSource = BillBUS.searchBillByCustomerName(kh);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Phiếu đặt hàng
        private void button8_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }
        

        private void cbxTimPDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTimPDH.Visible = true;
            dtpTimPDH.Visible = false;
            if(cbxTimPDH.SelectedIndex==0)
                lblTimPDH.Text="Nhập mã phiếu đặt hàng";
            if(cbxTimPDH.SelectedIndex==1)
                lblTimPDH.Text="Nhập tên nhân viên lập đặt hàng";
            if (cbxTimPDH.SelectedIndex == 2)
            {
                lblTimPDH.Text = "Nhập ngày lập phiếu đặt hàng";
                txtTimPDH.Visible = false;
                dtpTimPDH.Visible = true;
            }
            if(cbxTimPDH.SelectedIndex==3)
                lblTimPDH.Text = "Nhập tên khách hàng";
        }
        

        private void XoaPDH_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = new DialogResult();
                rs = MessageBox.Show("Bạn chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (rs == DialogResult.OK)
                {
                    OrderBillDTO pdh = new OrderBillDTO();
                    pdh.orderbillID = dgvDSPDH.CurrentRow.Cells[0].Value.ToString();
                    OrderBillBUS.deleteOrderBill(pdh);
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    load_data();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDSPDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaPDH.Text = dgvDSPDH.CurrentRow.Cells[0].Value.ToString();
                txtNVLPDH.Text = dgvDSPDH.CurrentRow.Cells[1].Value.ToString();
                txtKhachDH.Text = dgvDSPDH.CurrentRow.Cells[2].Value.ToString();
                dtpNgayLPDH.Text = dgvDSPDH.CurrentRow.Cells[3].Value.ToString();
                txtTongTienPDH.Text = dgvDSPDH.CurrentRow.Cells[4].Value.ToString();
                OrderBillDTO pdh = new OrderBillDTO();
                pdh.orderbillID = dgvDSPDH.CurrentRow.Cells[0].Value.ToString();
                dgvCTPDH.DataSource = OrderBillBUS.loadOrderBillListByID(pdh);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaPDH_Click(object sender, EventArgs e)
        {
            try
            {
                OrderBillDetailDTO ctpdh = new OrderBillDetailDTO();
                OrderBillDTO pdh = new OrderBillDTO();
                ctpdh.ID = int.Parse(dgvCTPDH.CurrentRow.Cells[0].Value.ToString());
                pdh.orderbillID = dgvCTPDH.CurrentRow.Cells[1].Value.ToString();
                ctpdh.productID = dgvCTPDH.CurrentRow.Cells[2].Value.ToString();
                ctpdh.quantity = int.Parse(dgvCTPDH.CurrentRow.Cells[4].Value.ToString());
                ctpdh.totalcost = ctpdh.quantity*long.Parse(dgvCTPDH.CurrentRow.Cells[5].Value.ToString());
                OrderBillBUS.updateOrderBillDetail(ctpdh);
                pdh.totalcost = StatisticBUS.sumOrderBillByID(pdh);
                OrderBillBUS.updateCost(pdh);
                load_data();
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TìmPDH_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTimPDH.SelectedIndex == 0)
                {
                    OrderBillDTO pdh = new OrderBillDTO();
                    pdh.orderbillID = txtTimPDH.Text;
                    if (OrderBillBUS.searchOrderBillByID(pdh).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    }
                    dgvDSPDH.DataSource = OrderBillBUS.searchOrderBillByID(pdh);
                }
                if (cbxTimPDH.SelectedIndex == 1)
                {
                    StaffDTO nv = new StaffDTO();
                    nv.staffName = txtTimPDH.Text;
                    if (OrderBillBUS.searchByCreatorName(nv).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    }
                    dgvDSPDH.DataSource = OrderBillBUS.searchByCreatorName(nv);
                }
                if (cbxTimPDH.SelectedIndex == 2)
                {
                    OrderBillDTO pdh = new OrderBillDTO();
                    pdh.year = dtpNgayLPDH.Value.Year;
                    pdh.month = dtpNgayLPDH.Value.Month;
                    pdh.day = dtpNgayLPDH.Value.Day;
                    if (OrderBillBUS.searchOrderBillByCreationDate(pdh).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    }
                    dgvDSPDH.DataSource = OrderBillBUS.searchOrderBillByCreationDate(pdh);
                }
                if (cbxTimPDH.SelectedIndex == 3)
                {
                    CustomerDTO kh = new CustomerDTO();
                    kh.customerName = txtTimPDH.Text;
                    if (OrderBillBUS.searchByCustomerName(kh).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin bạn cần tìm", "Thông báo", MessageBoxButtons.OK);
                    }
                    dgvDSPDH.DataSource = OrderBillBUS.searchByCustomerName(kh);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Thống kê
        private void button2_Click(object sender, EventArgs e)
        {
            btnThoat_Click(sender, e);
        }
        
        /// <summary>
        /// xem chi tiet thong ke theo ngay cua san pham xuat va nhap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                ImportBill2 pn1 = new ImportBill2();
                ImportBill2 pn2 = new ImportBill2();
                pn1.year = dtbbetween1.Value.Year;
                pn1.month = dtbbetween1.Value.Month;
                pn1.day = dtbbetween1.Value.Day;
                pn2.year = dtbbetween2.Value.Year;
                pn2.month = dtbbetween2.Value.Month;
                pn2.day = dtbbetween2.Value.Day;
                if (StatisticBUS.loadImportBillDate(pn1, pn2).Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy !", "Thông báo", MessageBoxButtons.OK); return;
                }
                dgvDSSPN.DataSource = StatisticBUS.loadImportBillDate(pn1,pn2);
                txtTongChi.Text = StatisticBUS.sumImportBillByDay(pn1, pn2).ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnXem2_Click(object sender, EventArgs e)
        {
            try
            {
                BillDTO hd1 = new BillDTO();
                BillDTO hd2 = new BillDTO();
                hd1.year = dtpHD1.Value.Year;
                hd1.month = dtpHD1.Value.Month;
                hd1.day = dtpHD1.Value.Day;
                hd2.year = dtpHD2.Value.Year;
                hd2.month = dtpHD2.Value.Month;
                hd2.day = dtpHD2.Value.Day;
                if (StatisticBUS.loadBillCreationDate(hd1, hd2).Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy !", "Thông báo", MessageBoxButtons.OK); return;
                }
                dgvDSSPN.DataSource = StatisticBUS.loadBillCreationDate(hd1, hd2);
                txtTongThu.Text = StatisticBUS.sumBillByDay(hd1, hd2).ToString();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        /// <summary>
        /// xem chi tiet thong ke theo ngay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                ImportBill2 pn1 = new ImportBill2();
                ImportBill2 pn2 = new ImportBill2();
                BillDTO hd1 = new BillDTO();
                BillDTO hd2 = new BillDTO();
                hd1.year = pn1.year = dtpDT1.Value.Year;
                hd1.month = pn1.month = dtpDT1.Value.Month;
                hd1.day = pn1.day = dtpDT1.Value.Day;
                hd2.year = pn2.year = dtpDT2.Value.Year;
                hd2.month = pn2.month = dtpDT2.Value.Month;
                hd2.day = pn2.day = dtpDT2.Value.Day;
                if (StatisticBUS.loadByImportBillCreationDate(pn1, pn2).Rows.Count == 0&&StatisticBUS.loadByBillCreationDate(hd1,hd2).Rows.Count==0)
                {
                    MessageBox.Show("Không tìm thấy !", "Thông báo", MessageBoxButtons.OK); return;
                }
                dgvSPN.DataSource = StatisticBUS.loadByImportBillCreationDate(pn1, pn2);
                dgvSPX.DataSource = StatisticBUS.loadByBillCreationDate(hd1, hd2);
                txtTT.Text = StatisticBUS.sumBillByDay2(hd1, hd2).ToString();
                txtTC.Text = StatisticBUS.sumImportBillCostByDay(pn1, pn2).ToString();
                txtThue.Text = ((StatisticBUS.sumBillByDay2(hd1, hd2) - StatisticBUS.sumImportBillCostByDay(pn1, pn2)) * 1 / 10).ToString();
                txtLN.Text = (StatisticBUS.sumBillByDay2(hd1, hd2) - StatisticBUS.sumImportBillCostByDay(pn1, pn2)).ToString();
                txtLN2.Text = ((StatisticBUS.sumBillByDay2(hd1, hd2) - StatisticBUS.sumImportBillCostByDay(pn1, pn2)) - ((StatisticBUS.sumBillByDay2(hd1, hd2) - StatisticBUS.sumImportBillCostByDay(pn1, pn2)) * 1 / 10)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTimNCC.SelectedIndex == 0)
            {
                lblTimNCC.Text = "Nhập mã nhà cung cấp:";
            }
            if (cbxTimNCC.SelectedIndex == 1)
            {
                lblTimNCC.Text = "Nhập tên nhà cung cấp:";
            }
        }
        /// <summary>
        /// thêm mới nhà cung cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                SupplierDTO ncc = new SupplierDTO();
                ncc.supplierName = txtTenNCC.Text;
                ncc.supplierID = txtMaNCC.Text;
                ncc.email = txtEmailNCC.Text;
                ncc.supplierPhone = txtSDTNCC.Text;
                SupplierBUS.insertSupplier(ncc);
                MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OKCancel);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// xóa nhà cung cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                SupplierDTO ncc = new SupplierDTO();
                ncc.supplierID = dgvNCC.CurrentRow.Cells[0].Value.ToString();
                SupplierBUS.deleteSupplier(ncc);
                MessageBox.Show("Xóa thành công !", "Thông báo", MessageBoxButtons.OKCancel);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// sửa nhà cung cấp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click_1(object sender, EventArgs e)
        {
            try
            {
                SupplierDTO ncc = new SupplierDTO();
                ncc.supplierName = txtTenNCC.Text;
                ncc.supplierID = txtMaNCC.Text;
                ncc.email = txtEmailNCC.Text;
                ncc.supplierPhone = txtSDTNCC.Text;
                SupplierBUS.updateSupplier(ncc);
                MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButtons.OKCancel);
                load_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaNCC.Text = dgvNCC.CurrentRow.Cells[0].Value.ToString();
                txtTenNCC.Text = dgvNCC.CurrentRow.Cells[1].Value.ToString();
                txtEmailNCC.Text = dgvNCC.CurrentRow.Cells[3].Value.ToString();
                txtSDTNCC.Text = dgvNCC.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbxTimNCC.SelectedIndex == 0)
                {
                    SupplierDTO ncc = new SupplierDTO();
                    ncc.supplierID = txtTimNCC.Text;
                    if (SupplierBUS.searchSupplierByID(ncc).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvNCC.DataSource = SupplierBUS.searchSupplierByID(ncc);
                }
                if (cbxTimNCC.SelectedIndex == 1)
                {
                    SupplierDTO ncc = new SupplierDTO();
                    ncc.supplierName = txtTimNCC.Text;
                    if (SupplierBUS.searchSupplierByName(ncc).Rows.Count == 0)
                    {
                        MessageBox.Show("Không có thông tin cần tìm", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    dgvNCC.DataSource = SupplierBUS.searchSupplierByName(ncc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void saoLưuDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog file = new FolderBrowserDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                string str = "Server=localhost;Database=banthietbidientu;Port=3307;User ID=root;Password=; CHARSET=utf8";
                MySqlConnection cnn = new MySqlConnection(str);
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                string backup = "BACKUP DATABASE banthietbidientu TO DISK ='" + file.SelectedPath + "\\banthietbidientu-" + DateTime.Now.Ticks.ToString()+".bak'";
                MySqlCommand cmd = new MySqlCommand(backup, cnn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sao lưu thành công !", "Thông báo", MessageBoxButtons.OK);
                cnn.Close();
            }
        }

        private void tabBCXuat_Click(object sender, EventArgs e)
        {

        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }        
}
