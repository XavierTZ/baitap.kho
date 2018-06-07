using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlybanMayTinh
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void lbTenDangNhap_Click(object sender, EventArgs e)
        {
            
        }

        //Connecting database
        private string url = @"Data Source=DESKTOP-8RD0MBP\OXIZON;Initial Catalog=baitaplon;Integrated Security=True"; 
        public void connection()
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = url;
                    command(con);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        //command 
        private string comd = "";
        public void command(SqlConnection connection)
        {
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = comd;
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = connection;

                string username = txtname.Text;
                string password = txtpass.Text;

                connection.Open();
                com.Parameters.AddWithValue("", username);
                com.Parameters.AddWithValue("", password);
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {

        }
    }
}
