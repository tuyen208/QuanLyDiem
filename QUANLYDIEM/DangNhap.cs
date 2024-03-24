using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QUANLYDIEM
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void moketnoi()
        {
            string ketnoi = @"Data Source=DESKTOP-CHB69CQ;Initial Catalog=QUANLYDIEMSINHVIEN;Integrated Security=True";
            conn = new SqlConnection(ketnoi);
            conn.Open();

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string select = "Select * From DANGNHAP where TenTaiKhoan='" + txtTenDN.Text + "' and MatKhau='" + txtMatKhau.Text + "' and Quyen='Admin'";
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    MessageBox.Show("Đăng nhập vào hệ thống !", "Thông báo !");
                    //Form frm1 = new Form1();
                    //frm1.Show();
                    Form frm = new FormChinh();
                    frm.Show();

                    this.Hide();

                    cmd.Dispose();
                    reader.Close();
                    reader.Dispose();
                }

                else 
                {
                    cmd.Dispose();
                    reader.Close();
                    reader.Dispose();
                    string select1 = "Select * From DANGNHAP where TenTaiKhoan='" + txtTenDN.Text + "' and MatKhau='" + txtMatKhau.Text + "' and Quyen='HS'";
                    SqlCommand cmd1 = new SqlCommand(select1, conn);
                    SqlDataReader reader1;
                    reader1 = cmd1.ExecuteReader();

                    if (reader1.Read())
                    {
                        MessageBox.Show("Đăng nhập vào hệ thống !", "Thông báo !");
                        //Form frm1 = new Form1();
                        //frm1.Show();
                        FormChinh frm = new FormChinh();
                        frm.Show();



                        this.Hide();

                        frm.mnuQuanlinguoidung.Enabled = false;
                        frm.mnuWindows.Enabled = false;
                        

                        frm.btnQLDSV.Hide();
                        frm.button1.Hide();
                        frm.btnQLK.Hide();
                        frm.btnQLL.Hide();
                        frm.btnQLMH.Hide();
                        frm.btnQLSV.Hide();


                        cmd.Dispose();
                        reader.Close();
                        reader.Dispose();
                        cmd1.Dispose();
                        reader1.Close();
                        reader1.Dispose();

                    }
                    else
                    {
                        cmd.Dispose();
                        reader.Close();
                        reader.Dispose();
                        cmd1.Dispose();
                        reader1.Close();
                        reader1.Dispose();

                        string select2 = "Select * From DANGNHAP where TenTaiKhoan='" + txtTenDN.Text + "' and MatKhau='" + txtMatKhau.Text + "' and Quyen='GV'";
                        SqlCommand cmd2 = new SqlCommand(select2, conn);
                        SqlDataReader reader2;
                        reader2 = cmd2.ExecuteReader();

                        if (reader2.Read())
                        {
                            MessageBox.Show("Đăng nhập vào hệ thống !", "Thông báo !");
                            //Form frm1 = new Form1();
                            //frm1.Show();
                            FormChinh frm = new FormChinh();
                            frm.Show();

                            this.Hide();

                            frm.mnuQuanlinguoidung.Enabled = false;
                            frm.mnuWindows.Enabled = false;


                            frm.btnQLK.Hide();
                            frm.btnQLL.Hide();
                            frm.btnQLMH.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai !");
                        }
                        txtMatKhau.Text = "";
                        txtTenDN.Text = "";
                        cmd1.Dispose();
                        reader1.Close();
                        reader1.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            moketnoi();
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
