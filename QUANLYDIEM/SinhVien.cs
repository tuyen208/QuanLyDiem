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
    public partial class SinhVien : Form
    {
        public SinhVien()
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
        private void laytenlop()
        {
            string sql = "select * from LOP ";
            DataTable dtLOP = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtLOP = ds.Tables[0];
            
            cboSV.DataSource = dtLOP;
            cboSV.DisplayMember = "TenLop";
            cboSV.ValueMember = "MaLop";
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
        private void hienthi()
        {
            string sql = "select * from SINHVIEN where MaLop = '" + malop + "'  ";
            DataTable dtLOP = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtLOP = ds.Tables[0];
            dtgSV.DataSource = dtLOP;
        }

        
        private void SinhVien_Load(object sender, EventArgs e)
        {
            moketnoi();
            laytenlop();
            laytenkhoa();
            hienthi();
            cboSV_SelectedIndexChanged(sender, e);

            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            txtDiaChi.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtGioiTinh.Enabled = false;
        }

        private void cboSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            malop = cboSV.SelectedValue.ToString();
            hienthi();
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

                cboSV.DataSource = dtLOP;
                cboSV.DisplayMember = "TenLop";
                cboSV.ValueMember = "MaLop";

            }
        }

        private void dtgSV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtMa.Text = dtgSV.Rows[i].Cells[0].Value.ToString();
                txtTen.Text = dtgSV.Rows[i].Cells[1].Value.ToString();
                txtDiaChi.Text = dtgSV.Rows[i].Cells[2].Value.ToString();
                txtNgaySinh.Text = dtgSV.Rows[i].Cells[3].Value.ToString();
                txtGioiTinh.Text = dtgSV.Rows[i].Cells[4].Value.ToString();

            }
            catch (Exception)
            {
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            txtMa.Enabled = true;
            txtTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtGioiTinh.Enabled = true;
            trangthai(false, false, false, true, true);

            chon = 1;
            txtMa.Focus();
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtNgaySinh.Text = "";
            txtGioiTinh.Text = "";

        }

        private void btSua_Click(object sender, EventArgs e)
        {
            chon = 2;

            trangthai(false, false, false, true, true);
            txtMa.Enabled = false;
            txtTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtGioiTinh.Enabled = true;

            txtTen.Focus();
            
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string ma, ten, diachi, ngaysinh, gioitinh, sql = "";
            ma = txtMa.Text.ToString();
            ten = txtTen.Text.ToString();
            diachi = txtDiaChi.Text.ToString();
            ngaysinh = txtNgaySinh.Text.ToString();
            gioitinh = txtGioiTinh.Text.ToString();


            if (chon == 1)
            {
                
                try
                {

                    sql = "insert into SINHVIEN values ('" + ma + "',N'" + ten + "',N'" + diachi + "','" + ngaysinh + "',N'" + gioitinh + "','" + malop + "' )";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtNgaySinh.Enabled = false;
                    txtGioiTinh.Enabled = false;
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

                    sql = "update SINHVIEN set TenSV=N'" + ten + "', DiaChi=N'" + diachi + "', NgaySinh='" + ngaysinh + "', GioiTinh=N'" + gioitinh + "' where MaSV='" + ma + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
                    txtMa.Enabled = false;
                    txtTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtNgaySinh.Enabled = false;
                    txtGioiTinh.Enabled = false;

                }
                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }


            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            string ma, sql = "";
            ma = txtMa.Text.ToString();
            sql = "delete SINHVIEN where MaSV = '" + ma + "' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            hienthi();
                trangthai(true, true, true, false, false);
                MessageBox.Show("Xóa Dữ Liệu Thành Công !");
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai(true, true, true, false, false);
            txtMa.Enabled = false;
            txtTen.Enabled = false;
            txtDiaChi.Enabled = false;
            txtNgaySinh.Enabled = false;
            txtGioiTinh.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
                
        }

        private void SinhVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
       
    }
}
