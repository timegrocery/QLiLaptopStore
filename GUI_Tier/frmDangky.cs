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
    public partial class frmDangky : Form
    {
        public frmDangky()
        {
            InitializeComponent();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtusername.Text=="")
                {
                    MessageBox.Show("Bạn chưa điền username !","Thông báo",MessageBoxButtons.OK);
                    errorProvider1.SetError(txtusername,"null");
                    txtusername.Focus();
                }
                else if (txtpass.Text != txtconfirm.Text)
                {
                    MessageBox.Show("Xác nhận mật khẩu chưa chính xác !", "Thông báo", MessageBoxButtons.OK);
                    txtpass.Focus();
                }
                else
                {
                    AccountDTO tk = new AccountDTO();
                    tk.username = txtusername.Text;
                    tk.password = txtpass.Text;
                    tk.jobID = cbxCongviec.SelectedValue.ToString();
                    AccountBUS.register(tk);
                    MessageBox.Show("Đăng ký thành công !", "Thông báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmDangky_Load(object sender, EventArgs e)
        {
            cbxCongviec.DataSource = JobBUS.Load_DSCV();
            cbxCongviec.DisplayMember = "ten_cv";
            cbxCongviec.ValueMember = "ma_cv";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = new DialogResult();
            rs = MessageBox.Show("Chắc chắn bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtpass.Clear();
            txtusername.Clear();
            txtconfirm.Clear();
        }
    }
}
