﻿using System;
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
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }

        //page load
        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            showNhanVien();
        }

        //Kết nối cơ sở dữ liệu
        protected SqlConnection connecting()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["database_btl"].ConnectionString;
            connection.Open();
            return connection;
        }

        //Hiện bảng nhân viên
        private void showNhanVien()
        {
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connecting();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "_pro_HienHinhNhanVien";
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = command;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv_NhanVien.DataSource = dt;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi kìa má ơi \n", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                throw;
            }
                
        }

        //Sự kiện khi chọn một dòng trong datagridview
        private void dgv_NhanVien_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell cells in dgv_NhanVien.SelectedCells)
            {
                cell = cells;
                break;
            }
            if(cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                txt_id.Text = row.Cells[0].Value.ToString();
                txt_name.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() == "False")
                {
                    txt_gt.Text = "Nữ";
                }
                else
                {
                    txt_gt.Text = "Nam";
                }
                txt_email.Text = row.Cells[5].Value.ToString();
                txt_date.Text = row.Cells[3].Value.ToString();
                txt_DiaChi.Text = row.Cells[4].Value.ToString();
            }
        }

        private int changeSex()
        {
            int gt;
            if (txt_gt.Text == "Nam")
            {
                gt = 1;
            }
            else
            {
                gt = 0;
            }
            return gt;
        }

        //Sự kiện bắt nút click [Cập nhật]
        private void btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connecting();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "pro_UpdateNhanVien";

                    command.Parameters.AddWithValue("@iMaNhanVien", int.Parse(txt_id.Text));
                    command.Parameters.AddWithValue("@sHoTen", txt_name.Text);
                    command.Parameters.AddWithValue("@bGioiTinh", changeSex());
                    command.Parameters.AddWithValue("@dNgaySinh", DateTime.Parse(txt_date.Text));
                    command.Parameters.AddWithValue("@tDiaChi", txt_DiaChi.Text);
                    command.Parameters.AddWithValue("@tEmail", txt_email.Text);
                    command.ExecuteNonQuery();
                }
                showNhanVien();
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi kìa má ơi \n" + err, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                throw;
            }                
        }

        //Sự kiện click cho nút [Thêm]
        private void btn_Add_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connecting();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pro_ThemNhanVien";

                command.Parameters.AddWithValue("@sHoTen", txt_name.Text);
                command.Parameters.AddWithValue("@bGioiTinh", changeSex());
                command.Parameters.AddWithValue("@dNgaySinh", DateTime.Parse(txt_date.Text));
                command.Parameters.AddWithValue("@tDiaChi", txt_DiaChi.Text);
                command.Parameters.AddWithValue("@tEmail", txt_email.Text);
                command.ExecuteNonQuery();
            }
            showNhanVien();
        }

        //Xóa một rows đã được chọn
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connecting();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pro_DeleteNhanVien";

                command.Parameters.AddWithValue("@iMaNhanVien", int.Parse(txt_id.Text));
                command.ExecuteNonQuery();
            }
            showNhanVien();
        }
    }
}
