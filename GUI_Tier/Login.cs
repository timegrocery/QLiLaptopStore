using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BUS;
namespace GUI_Tier
{
    public partial class frmDangnhap : Form
    {
        public frmDangnhap()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult rs = new DialogResult();
            rs = MessageBox.Show("Chắc chắn bạn muốn thoát ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rs == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// load du lieu len form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// bat su kien dang nhap tai khoan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AccountDTO tk = new AccountDTO();
                tk.username = txtusername.Text;
                tk.password = txtpassword.Text;
                //tk.ma_cv = cbxCV.SelectedValue.ToString();
                tk.status = true;
                if (AccountBUS.login(tk)!="")
                {
                    string quyenhan = AccountBUS.login(tk);
                    switch (quyenhan)
                    {
                        case "BH":
                            this.Hide();
                            frmBanHang bh = new frmBanHang();
                            bh.Show();
                            bh.STR = txtusername.Text;
                            break;
                        case "NH":
                            this.Hide();
                            NhapHang nh = new NhapHang();
                            nh.Show();
                            nh.STR = txtusername.Text;
                            break;
                        case "QL":
                            this.Hide();
                            QuanLy ql = new QuanLy();
                            ql.Show();
                            ql.STR = txtusername.Text;
                            break;
                        default:
                            MessageBox.Show("Error!", "Thông báo", MessageBoxButtons.OK);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn có người đã đăng nhập hoặc bạn nhập sai thông tin !","Thông báo",MessageBoxButtons.OK);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
