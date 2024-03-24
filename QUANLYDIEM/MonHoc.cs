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
    public partial class MonHoc : Form
    {
        public MonHoc()
        {
            InitializeComponent();
        }
        string malop;
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

        DataTable dtKHOA = new DataTable();
        private void laytenkhoa()
        {
            string sql = "select * from KHOA ";

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKHOA = ds.Tables[0];
            cboKhoa.DataSource = dtKHOA;
            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";

        }
        private void laytenmon()
        {
            string sql = "select * from LOP ";
            DataTable dtKHOA = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKHOA = ds.Tables[0];
            
            cboLop.DataSource = dtKHOA;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";

        }
        private void hienthi()
        {
            string sql = "select * from MONHOC where MaLop = '" + malop + "'  ";
            DataTable dtSV = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtSV = ds.Tables[0];
            dtgMON.DataSource = dtSV;

        }
        private void MonHoc_Load(object sender, EventArgs e)
        {
            moketnoi();
            laytenmon();
            laytenkhoa();
            hienthi();
            cboLoai_SelectedIndexChanged(sender, e);
            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            txtSTC.Enabled = false;
            txtHocki.Enabled = false;

        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            malop = cboLop.SelectedValue.ToString();
            hienthi();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtSTC.Enabled = true;
            txtHocki.Enabled = true;
            trangthai(false, false, false, true, true);
            chon = 1;
            txtMa.Focus();
            txtMa.Text = "";
            txtTen.Text = "";
            txtSTC.Text = "";
            txtHocki.Text = "";

            
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            trangthai(false, false, false, true, true);
            txtMa.Enabled = false;
            txtTen.Enabled = true;
            txtSTC.Enabled = true;
            txtHocki.Enabled = true;
            chon = 2;
            txtTen.Focus();
            
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string ma, ten, STC,hocki, sql = "";
            ma = txtMa.Text.ToString();
            ten = txtTen.Text.ToString();
            STC = txtSTC.Text.ToString();
            hocki = txtHocki.Text.ToString();

            if (chon == 1)
            {
               
                try
                {

                    sql = "insert into MONHOC values ('" + ma + "',N'" + ten + "','" + STC + "','" + malop + "','" + hocki + "' )";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
                    txtSTC.Enabled = false;
                    txtHocki.Enabled = false;
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

                    sql = "update MONHOC set TenMon=N'" + ten + "', SoTinChi=N'" + STC + "',HocKi = '"+hocki+"'  where MaMon='" + ma + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
                    txtSTC.Enabled = false;
                    txtHocki.Enabled = false;
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }


            }

        }
       
        private void dtgMON_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtMa.Text = dtgMON.Rows[i].Cells[0].Value.ToString();
                txtTen.Text = dtgMON.Rows[i].Cells[1].Value.ToString();
                txtSTC.Text = dtgMON.Rows[i].Cells[2].Value.ToString();
                txtHocki.Text = dtgMON.Rows[i].Cells[4].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string ma, sql = "";
            ma = txtMa.Text.ToString();
            sql = "delete MONHOC where MaMon = '" + ma + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            hienthi();
            trangthai(true, true, true, false, false);
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            txtSTC.Enabled = false;
            txtHocki.Enabled = false;
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboKhoa.SelectedIndex;
            if (index != -1)
            {
                DataRow dr = dtKHOA.Rows[index];
                string makhoa = dr["MaKhoa"].ToString();

                string sql = "select * from LOP where MaKhoa = '" + makhoa + "' ";
                DataTable dtLOP = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtLOP = ds.Tables[0];

                cboLop.DataSource = dtLOP;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

            }
        }

        private void MonHoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }
    }
}
