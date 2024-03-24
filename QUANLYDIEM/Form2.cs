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
    public partial class Form2 : Form
    {
        public Form2()
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
        private void hienthi()
        {
            string sql = "select * from DiemTBKH  ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dataGridView2.DataSource = dtKQ;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            moketnoi();
            groupBox3.Visible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtTen.Text = "";
            txtDiem.Text = "";
            txtxeploai.Text = "";
            try
            {
                string sql = "select * from DiemTBHK1 where MaSV = '" + txtMaSV.Text + "'  ";
                DataTable dtKQ = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtKQ = ds.Tables[0];
                dataGridView1.DataSource = dtKQ;
                groupBox3.Visible = false;
                groupBox1.Visible = true;
            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class1.Export2Excel(dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string ma, sql = "";
                ma = txtMaSV.Text.ToString();
                sql = "delete from DiemTBKH  ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                hienthi();
            }
            catch (Exception)
            { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select (avg(DiemTBHocKi)) as diem from DiemTBHK1";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader rdr = cmd.ExecuteReader();

               while (rdr.Read())
                {

                    txtDiem.Text = (string)rdr["diem"].ToString();
                    float diem = float.Parse(txtDiem.Text);
                   
                    if (diem >= 3.6)
                    {

                        txtxeploai.Text = "Xuất sắc";

                    }
                    else
                        if (diem < 3.6 && diem >= 3.2)
                        {
                            txtxeploai.Text = "Giỏi";

                        }
                        else
                            if (diem < 3.2 && diem >= 2.5)
                            {
                                txtxeploai.Text = "Khá";

                            }
                            else
                                if (diem < 2.5 && diem >= 2.0)
                                {
                                    txtxeploai.Text = "Trung bình";

                                }
                                else
                                    if (diem < 2.0 && diem >= 1)
                                    {
                                        txtxeploai.Text = "Yếu";

                                    }
                                    else
                                        if (diem < 1)
                                        {

                                           
                                            txtxeploai.Text = "Kém";

                                        }


                }

                rdr.Dispose();
                cmd.Dispose();
            }
            catch (Exception)
            { }

            try
            {
                string sql = "insert into DiemTBKH values ('" + txtMaSV.Text + "',N'" + txtTen.Text + "','" + txtDiem.Text + "',N'" + txtxeploai.Text + "' )";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập Nhật Thành Công");
                groupBox3.Visible = true;
                groupBox1.Visible = true;
                
                hienthi();
            }
            catch (Exception)
            {
                MessageBox.Show(" K Cập Nhật Thành Công");
            }
            
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtTen.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();

            }
            catch (Exception)
            { }
        }
    }
}
