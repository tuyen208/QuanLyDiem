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
    public partial class FrmQLDSV : Form
    {
        public FrmQLDSV()
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
        private void trangthai(bool them, bool sua, bool xoa, bool luu, bool huy)
        {
            btHuy.Visible = huy;
            btLuu.Visible = luu;
            btnSua.Visible = sua;
            btnNhap.Visible = them;
            btnXoa.Visible = xoa;
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
            cboMaSV.DisplayMember = "TenSV";
            cboMaSV.ValueMember = "MaSV";

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


        private void FrmQLDSV_Load(object sender, EventArgs e)
        {
            moketnoi();
            laytenlop();
            laytensv();
            hienthi();
            cboMaSV_SelectedIndexChanged(sender, e);
            cboLop_SelectedIndexChanged(sender, e);
            trangthai(true, true, true, false, false);
            groupBox5.Enabled = false;
            groupBox1.Enabled = false;
            panel1.Visible = false;

        }

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
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
                cboMaSV.DisplayMember = "TenSV";
                cboMaSV.ValueMember = "MaSV";
            }
        }

        private void cboMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            masv = cboMaSV.SelectedValue.ToString();
            hienthi();
            
        }

       

        private void btnNhap_Click(object sender, EventArgs e)
        {
            trangthai(false, false, false, true, true);
            groupBox5.Enabled = true;
            groupBox1.Enabled = true;
            chon = 1;
            txtDiemChu.Text = "";
            txtDiemHS4.Text = "";
            txtDiemTBKT.Text = "";
            txtDiemThi.Text = "";
            txtDiemTK.Text = "";
            txtGhiChu.Text = "";
            txtKT15P.Text = "";
            txtKT1tiet.Text = "";
            txtSTT.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            chon = 2;
            trangthai(false, false, false, true, true);
            groupBox5.Enabled = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string STT, ten, diem15, diem1, diemTBKT, diemthi,diemthilan2,  diemtb, diemchu, diemso, ghichu, hocki, sql = "";
            STT = txtSTT.Text.ToString();

            ten = cboMaSV.Text.ToString();
            diem15 = txtKT15P.Text.ToString();
            diem1 = txtKT1tiet.Text.ToString();
            diemTBKT = txtDiemTBKT.Text.ToString();
            diemthi = txtDiemThi.Text.ToString();
            diemthilan2 = txtDiemThiLan2.Text.ToString();
            diemtb = txtDiemTK.Text.ToString();
            diemchu = txtDiemChu.Text.ToString();
            diemso = txtDiemHS4.Text.ToString();
            ghichu = txtGhiChu.Text.ToString();
            hocki = cboHocKi.Text.ToString();


            if (chon == 1)
            {
                 if (txtSTT.Text == "")
                {
                    errorProvider1.SetError(txtSTT, "Mã lớp không để trống!");
                    txtSTT.Focus();
                }
                else if (cboHocKi.Text == "")
                {
                    errorProvider1.SetError(cboHocKi, "Học kỳ không để trống!");
                    cboHocKi.Focus();
                }
                else if (cboMonHoc.Text == "")
                {
                    errorProvider1.SetError(cboMonHoc, "Mã môn không để trống!");
                    cboMonHoc.Focus();
                }
                    
                else if (cboMaSV.Text == "")
                {
                    errorProvider1.SetError(cboMaSV, "Mã môn không để trống!");
                    cboMaSV.Focus();
                }
                    else if (cboLop.Text == "")
                {
                    errorProvider1.SetError(cboLop, "Mã lớp không để trống!");
                    cboLop.Focus();
                }
                else
                {

                    try
                    {
                        // string sql = "select STT,MaSv,TenSV,DiemKT1,DiemKT2,DiemTBKT,DiemThi,DiemTB,DiemChu,DiemSo,GhiChu,HocKi,MaMon,MaLop from KETQUA where MaSV = '" + masv + "'  ";
                        sql = "insert into KETQUA1 values ('" + STT + "','" + masv + "',N'" + ten + "','" + diem15 + "','" + diem1 + "','" + diemTBKT + "','" + diemthi + "','" + diemthilan2 + "','" + diemtb + "','" + diemchu + "','" + diemso + "',N'" + ghichu + "','" + hocki + "','" + cboMonHoc.Text + "','" + cboLop.Text + "' )";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        hienthi();
                        trangthai(true, true, true, false, false);
                        groupBox5.Enabled = false;
                        groupBox1.Enabled = false;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không Thể Cập Nhật !");
                    }
                }

            }
            if (chon == 2)
            {

                try
                {

                    sql = "update KETQUA1 set DiemKT1= '" + diem15 + "',DiemKT2='" + diem1 + "',DiemTBKT='" + diemTBKT + "',DiemThi='" + diemthi + "',DiemThiLai='" + diemthilan2 + "',DiemTB='" + diemtb + "',DiemChu='" + diemchu + "',DiemSo='" + diemso + "',GhiChu=N'" + ghichu + "' where STT='" + STT + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    hienthi();
                    trangthai(true, true, true, false, false);
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

        private void button1_Click(object sender, EventArgs e)
        {
            float DIEMKT15P, DIEMTBKT, DIEMKT1Tiet;

            if (txtKT15P.Text == "10" && txtKT1tiet.Text == "10")
            {
                txtDiemTBKT.Text = "10";
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
                }

                if (txtKT1tiet.Text == "")
                {
                    txtKT1tiet.Text = "0";
                }
                else
                {
                    DIEMKT1Tiet = float.Parse(this.txtKT1tiet.Text);
                }
                 DIEMKT15P = float.Parse(this.txtKT15P.Text);
                 DIEMKT1Tiet = float.Parse(this.txtKT1tiet.Text);
                if (DIEMKT15P > 10)
                {
                    MessageBox.Show("Nhap vào điểm trong khoảng 0-10, xin mời nhập lại."); txtKT15P.Focus();
                }

                 else if (DIEMKT1Tiet > 10)
                {
                    MessageBox.Show("Nhap vào điểm trong khoảng 0-10, xin mời nhập lại.");
                    txtKT15P.Focus();
                }
                else
                {
                DIEMKT15P = float.Parse(this.txtKT15P.Text);
                DIEMKT1Tiet = float.Parse(this.txtKT1tiet.Text);
                DIEMTBKT = (float)(DIEMKT15P + DIEMKT1Tiet * 2) / 3;
                txtDiemTBKT.Text = DIEMTBKT.ToString();
                }

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
                float DiemTK2= 10 ;
                if (txtDiemTBKT.Text == "10" && txtDiemThi.Text == "10")
                {
                    txtDiemTK.Text = "10";
                    txtDiemChu.Text = "A";
                    txtDiemHS4.Text = "4";
                    txtGhiChu.Text = "";
                }
                else
                {
                    if (txtDiemTBKT.Text == "0")
                    {
                        txtDiemTBKT.Text = "0";
                    }

                    DiemL1 = float.Parse(this.txtDiemThi.Text);
                    if (DiemL1 > 10)
                    {
                        MessageBox.Show("Nhap vào điểm trong khoảng 0-10, xin mời nhập lại.");
                        txtDiemThi.Focus();
                    }
                    else
                    {
                        DiemTBKT = float.Parse(txtDiemTBKT.Text.ToString());
                        DiemTK = (float)((DiemTBKT * 0.4) + ((float)DiemL1 * 0.6));
                        txtDiemTK.Text = DiemTK.ToString();
                        DiemTK2 = float.Parse(this.txtDiemTK.Text.ToString());
                    }
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
                                    txtGhiChu.Text = "Học Lại";
                                
                                }
                            }
                        }
                    }
                }
            }
           
        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string STT, sql = "";
                STT = txtSTT.Text.ToString();
                sql = "delete KETQUA1 where STT = '" + STT + "' ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                hienthi();
            }
            catch (Exception)
            {
                MessageBox.Show("Khong The Xoa!");
            }
            
        }

        private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTenMon.Clear();
            string select = "Select TenMon from MONHOC where MaMon='" + cboMonHoc.Text + "'";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtTenMon.Text = (reader.GetString(0));
            }
            reader.Dispose();
            cmd.Dispose();


        }

        private void cboHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboMonHoc.Items.Clear();
            string select = "Select MaMon from MONHOC where HocKi='" + cboHocKi.Text + "'";
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //Add vao cboMonHoc
            while (reader.Read())
            {

                cboMonHoc.Items.Add(reader.GetString(0));
            }
            //Tra tai nguyen 
            reader.Dispose();
            cmd.Dispose();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            trangthai(true, true, true, false, false);
            groupBox5.Enabled = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = true;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            Class1.Export2Excel(dgrDiem);
        }

        private void FrmQLDSV_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void dgrDiem_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            try
            {
                txtSTT.Text = dgrDiem.Rows[i].Cells[0].Value.ToString();
                cboMaSV.Text = dgrDiem.Rows[i].Cells[2].Value.ToString();
                txtHoTen.Text = dgrDiem.Rows[i].Cells[1].Value.ToString();
                txtKT15P.Text = dgrDiem.Rows[i].Cells[3].Value.ToString();
                txtKT1tiet.Text = dgrDiem.Rows[i].Cells[4].Value.ToString();
                txtDiemTBKT.Text = dgrDiem.Rows[i].Cells[5].Value.ToString();
                txtDiemThi.Text = dgrDiem.Rows[i].Cells[6].Value.ToString();
             
                txtDiemTK.Text = dgrDiem.Rows[i].Cells[8].Value.ToString();
                txtDiemChu.Text = dgrDiem.Rows[i].Cells[9].Value.ToString();
                txtDiemHS4.Text = dgrDiem.Rows[i].Cells[10].Value.ToString();
                txtGhiChu.Text = dgrDiem.Rows[i].Cells[11].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
