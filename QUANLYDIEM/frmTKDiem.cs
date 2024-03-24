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
    public partial class frmTKDiem : Form
    {
        public frmTKDiem()
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
            string sql = "select * from DiemTBHK1  ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dataGridView2.DataSource = dtKQ;
        }

        private void frmTKDiem_Load(object sender, EventArgs e)
        {
            moketnoi();
            hienthi();
            txtten.Visible = false;
            groupBox3.Visible = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtten.Text = "";
            txtCC.Text = "";
            txtDiemHK.Text = "";
            
            txtxeploai.Text = "";
            try
            {
                string sql = "select * from KETQUA1 where MaSV = '" + txtMaSV.Text + "' and HocKi = '" + cboHocKi.Text + "'  ";
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

        private void btnXemDiem_Click(object sender, EventArgs e)
        {
            
            try
                {
            string sql = "select (Cast((Sum(KETQUA1.DiemSo*MONHOC.SoTinChi))as float(1))/Cast((SUM(MONHOC.SoTinChi))as float(1))) as diem from KETQUA1,MONHOC where MaSV='" + txtMaSV.Text + "' and KETQUA1.HocKi= " + cboHocKi.Text + "and KETQUA1.MaMon = MONHOC.MaMon and MONHOC.TenMon not in (select TenMon from MONHOC where TenMon like 'Giáo%' and HocKi='" + cboHocKi.Text + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                
                    txtDiemHK.Text = (string)rdr["diem"].ToString();
                    float diem = float.Parse(txtDiemHK.Text);
                    float diem1 = float.Parse(cboHocKi.Text);
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
                                            
                                                txtCC.Text = "Bị Cảnh Cáo ";
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
                string sql = "insert into DiemTBHK1 values ('" + txtMaSV.Text + "',N'" + txtten.Text + "','" + cboHocKi.Text + "','" + txtDiemHK.Text + "',N'" + txtxeploai.Text + "',N'" + txtCC.Text + "' )";
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

        private void btnKhenThuong_Click(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1.Export2Excel(dataGridView2);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtten.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
               
            }
            catch (Exception)
            { }
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "insert into DiemTBHK1 values ('" + txtMaSV.Text + "',N'" + txtten.Text + "','" + cboHocKi.Text + "','" + txtDiemHK.Text + "',N'" + txtxeploai.Text + "',N'" + txtCC.Text + "' )";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cập Nhật Thành Công");
            }
            catch (Exception)
            {
                MessageBox.Show(" K Cập Nhật Thành Công");
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string ma, sql = "";
                ma = txtMaSV.Text.ToString();
                sql = "delete from DiemTBHK1  ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                hienthi();
            }
            catch (Exception)
            { }
        }

        private void txtMaSV1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        

       

    }
}
