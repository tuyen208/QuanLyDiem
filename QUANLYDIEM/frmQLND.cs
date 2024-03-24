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
    public partial class frmQLND : Form
    {
        public frmQLND()
        {
            InitializeComponent();
        }
        int chon = 0;
        SqlConnection conn;
        private void moketnoi()
        {
            string ketnoi = @"Data Source=DESKTOP-CHB69CQ;Initial Catalog=QUANLYDIEMSINHVIEN;Integrated Security=True";
            conn = new SqlConnection(ketnoi);
            conn.Open();
        }
        private void trangthai(bool them, bool sua, bool xoa, bool luu, bool huy)
        {
            btHuy.Visible = huy;
            btLuu.Visible = luu;
            btnSua.Visible = sua;
            btnThemmoi.Visible = them;
            btnXoa.Visible = xoa;
            
        }
        private void hienthi()
        {
            string sql = "select * from DANGNHAP  ";
            DataTable dtLOP = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtLOP = ds.Tables[0];
            dataGridView1.DataSource = dtLOP;
        }

        private void frmQLND_Load(object sender, EventArgs e)
        {
            moketnoi();
            hienthi();
            trangthai(true, true, true, false, false);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtMK.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtTaikhoan.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                cboQuyen.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            chon = 1;
            txtMK.Text = "";
            txtTaikhoan.Text = "";
            cboQuyen.Text= "";
            trangthai(false, false, false, true, true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            chon = 2;
            txtTaikhoan.Enabled = false;
            trangthai( false, false,false, true, true);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string tk, mk, quyen, sql = "";
            tk = txtTaikhoan.Text.ToString();
            mk = txtMK.Text.ToString();
            quyen = cboQuyen.Text.ToString();
            if (chon == 1)
            {
                 try
                {

                    sql = "insert into DANGNHAP values (N'" +tk + "',N'" + mk + "',N'" + quyen + "' )";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }
            }

            if (chon == 2)
            {

                try
                {

                    sql = "update DANGNHAP set MatKhau ='" + mk + "', Quyen=N'" + quyen + "' where TenTaiKhoan=N'" + tk + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                }

                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }


            }
        
              
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string tk = txtTaikhoan.Text.ToString();
            string sql = "delete DANGNHAP where TenTaiKhoan=N'" + tk + "'  ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            hienthi();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai(true, true, true, false, false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmQLND_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }
    }
}
