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
    public partial class frmDiemHocLai : Form
    {
        public frmDiemHocLai()
        {
            InitializeComponent();
        }
        int chon = 0;
        string masv;
        SqlConnection conn;
        private void moketnoi()
        {
            string ketnoi = @"Data Source=DESKTOP-CHB69CQ;Initial Catalog=QUANLYDIEMSINHVIEN;Integrated Security=True";
            conn = new SqlConnection(ketnoi);
            conn.Open();

        }
        private void trangthai( bool sua, bool luu, bool huy)
        {
            btHuy.Visible = huy;
            btLuu.Visible = luu;
            btnSua.Visible = sua;
           
        }

        DataTable dtLOP = new DataTable();
        private void laytenlop()
        {
            string sql = "select * from LOP ";

            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtLOP = ds.Tables[0];

            cboLop.DataSource = dtLOP;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
        }

        private void laytensv()
        {
            string sql = "select MaSV,TenSV from SINHVIEN ";
            DataTable dtSV = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtSV = ds.Tables[0];
            cboMaSV.DataSource = dtSV;
            cboMaSV.DisplayMember = "MaSV";
            cboMaSV.ValueMember = "TenSV";

        }
        private void hienthi()
        {
            string sql = "select * from KETQUA1 where MaSV = '" + masv + "'  ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dgrDiem.DataSource = dtKQ;
        }
        
        private void frmDiemHocLai_Load(object sender, EventArgs e)
        {
            moketnoi();
            laytenlop();
            laytensv();
            hienthi();
            cboMaSV_SelectedIndexChanged_1(sender, e);
            cboLop_SelectedIndexChanged_1(sender, e);
            trangthai( true, false, false);
            groupBox5.Enabled = false;
            groupBox1.Visible= false;
            
        }
        private void btLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void cboMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboLop_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int index1 = cboLop.SelectedIndex;
            if (index1 != -1)
            {
                DataRow dr = dtLOP.Rows[index1];
                string malop = dr["MaLop"].ToString();

                string sql = "select * from SINHVIEN where MaLop = '" + malop + "' ";
                DataTable dtSV = new DataTable();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dtSV = ds.Tables[0];
                cboMaSV.DataSource = dtSV;
                cboMaSV.DisplayMember = "MaSV";
                cboMaSV.ValueMember = "TenSV";
            }
        }

        private void cboMaSV_SelectedIndexChanged_1(object sender, EventArgs e)
        {
             masv = cboMaSV.SelectedValue.ToString();
             hienthi();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            chon = 2;
            trangthai(false, true, true);
            groupBox5.Enabled = true;
            groupBox1.Visible= false;
            groupBox2.Enabled = false;

        }
        private void btLuu_Click_1(object sender, EventArgs e)
        {
            string STT, ten, diem15, diem1, diemTBKT, diemthi, diemthilan2, diemtb, diemchu, diemso, ghichu, hocki, sql = "";
            STT = txtSTT.Text.ToString();

            ten = cboMaSV.Text.ToString();
            diem15 = txtKT15P1.Text.ToString();
            diem1 = txtKT1T1.Text.ToString();
            diemTBKT = txtTBKT.Text.ToString();
            diemthi = txtDiemThi.Text.ToString();
            diemthilan2 = txtDiemThiLan2.Text.ToString();
            diemtb = txtDiemTB.Text.ToString();
            diemchu = txtDiemChu.Text.ToString();
            diemso = txtDiemHS4.Text.ToString();
            ghichu = txtGhiChu.Text.ToString();
            hocki = cboHocKi.Text.ToString();
            if (chon == 2)
            {

                try
                {

                    sql = "update KETQUA1 set DiemKT1= '" + diem15 + "',DiemKT2='" + diem1 + "',DiemTBKT='" + diemTBKT + "',DiemThi='" + diemthi + "',DiemThiLai='" + diemthilan2 + "',DiemTB='" + diemtb + "',DiemChu='" + diemchu + "',DiemSo='" + diemso + "',GhiChu=N'" + ghichu + "' where STT='" + STT + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai( true, false, false);
                    groupBox5.Enabled = false;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Khong The Cap Nhat!");
                }


            }
        }

        private void dgrDiem_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtSTT.Text = dgrDiem.Rows[i].Cells[0].Value.ToString();
                cboMaSV.Text = dgrDiem.Rows[i].Cells[1].Value.ToString();
                txtHoTen.Text = dgrDiem.Rows[i].Cells[2].Value.ToString();
                txtKT15P1.Text = dgrDiem.Rows[i].Cells[3].Value.ToString();
                txtKT1T1.Text = dgrDiem.Rows[i].Cells[4].Value.ToString();
                txtTBKT.Text = dgrDiem.Rows[i].Cells[5].Value.ToString();
                txtDiemThi.Text = dgrDiem.Rows[i].Cells[6].Value.ToString();
                txtDiemThiLan2.Text = dgrDiem.Rows[i].Cells[7].Value.ToString();
                txtDiemTB.Text = dgrDiem.Rows[i].Cells[8].Value.ToString();
                txtDiemChu.Text = dgrDiem.Rows[i].Cells[9].Value.ToString();
                txtDiemHS4.Text = dgrDiem.Rows[i].Cells[10].Value.ToString();
                txtGhiChu.Text = dgrDiem.Rows[i].Cells[11].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float DIEMKT15P, DIEMTBKT, DIEMKT1Tiet;

            if (txtKT15P.Text == "10" && txtKT1tiet.Text == "10")
            {
                txtDiemTBKT1.Text = "10";
            }
            else
            {
                if (txtKT15P.Text == "")
                {
                    txtKT15P.Text = "0";
                }
                else
                {
                    DIEMKT15P = float.Parse(this.txtKT15P.Text);
                    if (DIEMKT15P > 10)
                    { MessageBox.Show("Nhap vào điểm trong khoảng 0-10, xin mời nhập lại."); txtKT15P.Focus(); }
                }

                if (txtKT1tiet.Text == "")
                {
                    txtKT1tiet.Text = "0";
                }
                else
                {
                    DIEMKT1Tiet = float.Parse(this.txtKT1tiet.Text);
                    if (DIEMKT1Tiet > 10)
                    {
                        MessageBox.Show("Nhap vào điểm trong khoảng 0-10, xin mời nhập lại.");
                        txtKT15P.Focus();
                    }
                }

                DIEMKT15P = float.Parse(this.txtKT15P.Text);
                DIEMKT1Tiet = float.Parse(this.txtKT1tiet.Text);
                DIEMTBKT = (float)(DIEMKT15P + DIEMKT1Tiet * 2) / 3;
                txtDiemTBKT1.Text = DIEMTBKT.ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float DiemL1;

            if (txtDiemThi.Text == "  ")
            {
                txtDiemThi.Text = " 0 ";
            }

            float DiemTK, DiemTBKT;
            float DiemTK2 = 10;
            if (txtDiemTBKT1.Text == "10" && txtDiemThi.Text == "10")
            {
                
                txtDiemThiLan2.Text = "10";
               
            }
            else
            {
                if (txtDiemTBKT1.Text == "0")
                {
                    txtDiemTBKT1.Text = "0";
                }
                DiemL1 = float.Parse(this.txtDiemTLL2.Text);
                DiemTBKT = float.Parse(txtDiemTBKT1.Text.ToString());
                DiemTK = (float)((DiemTBKT * 0.4) + ((float)DiemL1 * 0.6));
                txtDiemThiLan2.Text = DiemTK.ToString();
                DiemTK2 = float.Parse(this.txtDiemThiLan2.Text.ToString());
            }

            if (DiemTK2 <= 10 && DiemTK2 >= 8.5)
            {
                txtDiemChu.Text = "A";
                txtDiemHS4.Text = "4";
                txtGhiChu.Text = "";
            }
            else
            {
                if (DiemTK2 < 8.5 && DiemTK2 >= 7)
                {
                    txtDiemChu.Text = "B";
                    txtDiemHS4.Text = "3";
                    txtGhiChu.Text = "";
                }
                else
                {
                    if (DiemTK2 < 7 && DiemTK2 >= 5.5)
                    {
                        txtDiemChu.Text = "C";
                        txtDiemHS4.Text = "2";
                        txtGhiChu.Text = "";
                    }
                    else
                    {
                        if (DiemTK2 < 5.5 && DiemTK2 >= 4)
                        {
                            txtDiemChu.Text = "D";
                            txtDiemHS4.Text = "1";
                            txtGhiChu.Text = "";
                        }
                        else
                        {
                            if (DiemTK2 < 4 && DiemTK2 >= 0)
                            {
                                txtDiemChu.Text = "F";
                                txtDiemHS4.Text = "0";
                                txtGhiChu.Text = "Thi Lại";
                            }
                        }
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string sql = "select * from KETQUA1 where MaSV = '" + cboMaSV.Text + "' and DiemChu = '" + comboBox1.Text + "'  ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dgrDiem.DataSource = dtKQ;
            
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai( true, false, false);
            groupBox5.Enabled = false;
            groupBox2.Enabled = true;
        }

        private void frmDiemHocLai_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    
        
    }

}
