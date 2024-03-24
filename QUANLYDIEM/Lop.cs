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
    public partial class Lop : Form
    {
        public Lop()
        {
            InitializeComponent();
        }
        string makhoa;
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
            btSua.Visible = sua;
            btThem.Visible = them;
            btXoa.Visible = xoa;
        }
        private void laytenlop()
        {
            string sql = "select * from KHOA ";
            DataTable dtKHOA = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKHOA = ds.Tables[0];

            cboLOP.DataSource = dtKHOA;
            cboLOP.DisplayMember = "TenKhoa";
            cboLOP.ValueMember = "MaKhoa";
        }
        private void hienthi()
        {
            string sql = "select * from LOP where MaKhoa = '" + makhoa + "'  ";
            DataTable dtLOP = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtLOP = ds.Tables[0];
            dtgLOP.DataSource = dtLOP;
        }

        private void Lop_Load(object sender, EventArgs e)
        {
            moketnoi();
            laytenlop();
            hienthi();
            cboLOP_SelectedIndexChanged(sender, e);
            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            
        }

        private void cboLOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            makhoa = cboLOP.SelectedValue.ToString();
            hienthi();
        }

        private void dtgLOP_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

            int i = e.RowIndex;
            try
            {
                txtMa.Text = dtgLOP.Rows[i].Cells[0].Value.ToString();
                txtTen.Text = dtgLOP.Rows[i].Cells[1].Value.ToString();
                
            }
            catch (Exception)
            {
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            trangthai(false, false, false, true, true);
            chon = 1;
            txtMa.Focus();
            txtMa.Text = "";
            txtTen.Text = "";
            txtMa.Enabled = true;
            txtTen.Enabled = true;
            
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            trangthai(false, false, false, true, true);
            txtMa.Enabled = false;
            chon = 2;
            
            txtTen.Enabled = true;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string ma, ten,  sql = "";
            ma = txtMa.Text.ToString();
            ten = txtTen.Text.ToString();
            

            if (chon == 1)
            {
               
                try
                {

                    sql = "insert into LOP values ('" + ma + "',N'" + ten + "','" + makhoa + "' )";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
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

                    sql = "update LOP set TenLop=N'" + ten + "' where MaLop='" + ma + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }


            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string ma, sql = "";
            ma = txtMa.Text.ToString();
            sql = "delete LOP where MaLop = '" + ma + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            hienthi();
            trangthai(true, true, true, false, false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
        }

        private void Lop_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }
    }
}
