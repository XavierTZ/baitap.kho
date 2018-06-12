using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlybanMayTinh
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void lbTenDangNhap_Click(object sender, EventArgs e)
        {

        }

        //Connecting database
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["database_btl"].ConnectionString;
                    con.Open();
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = "checklogin";
                        com.CommandType = CommandType.StoredProcedure;
                        com.Connection = con;

                        string username = txtname.Text;
                        string password = txtpass.Text;

                        com.Parameters.AddWithValue("@username", username);
                        com.Parameters.AddWithValue("@pass", password);

                        SqlDataReader dr;
                        dr = com.ExecuteReader();
                        if (dr.HasRows != false)
                        {
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu bị sai", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("anh ơi em bị lỗi rồi: " + ex, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtname.Text = null;
            txtpass.Text = null;
        }

        private void login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn dừng đăng nhập", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = false;
            }
            else
            {
                txtname.Focus();
                e.Cancel = true;
            }
        }
    }
}
