using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QUANLYDIEM
{
    public partial class FormChinh : Form
    {
        public FormChinh()
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

       

        private void FormChinh_Load(object sender, EventArgs e)
        {
            moketnoi();
            
        }

        private void btnQLK_Click(object sender, EventArgs e)
        {
            Form fr = new Khoa();
            fr.Show();
        }

        private void btnQLL_Click(object sender, EventArgs e)
        {
            Form fr = new Lop();
            fr.Show();
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            Form fr = new MonHoc();
            fr.Show();
        }

        private void btnQLSV_Click(object sender, EventArgs e)
        {
            Form fr = new SinhVien();
            fr.Show();
        }

        private void btnQLDSV_Click(object sender, EventArgs e)
        {
            Form fr = new FrmQLDSV();
            fr.Show();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tkd = new frmTKDiem();
            tkd.Show(); 
        }

        private void tKSinhViênThiLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tkhl = new frmHocLai();
            tkhl.Show();
        }

        private void quảnLýNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form nd = new frmQLND();
            nd.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form tkd = new frmTKDiem();
            tkd.Show();
        }

        private void mnuDSSV_Click(object sender, EventArgs e)
        {
            Form fr = new SinhVien();
            fr.Show();
        }

        private void mnuDSTL_Click(object sender, EventArgs e)
        {
            Form tkhl = new frmHocLai();
            tkhl.Show();
        }

        private void mnuDiem_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Form qlhl = new frmDiemHocLai();
            qlhl.Show();
        }

        private void menuBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnQLSV.Visible = menuBarToolStripMenuItem.Checked;
            btnQLL.Visible = menuBarToolStripMenuItem.Checked;
            btnQLDSV.Visible = menuBarToolStripMenuItem.Checked;
            btnQLK.Visible = menuBarToolStripMenuItem.Checked;
            btnQLMH.Visible = menuBarToolStripMenuItem.Checked;
            button1.Visible = menuBarToolStripMenuItem.Checked;
        }

        private void mnuQuanlinguoidung_Click(object sender, EventArgs e)
        {
            Form qlnd = new frmQLND();
            qlnd.Show();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuDX_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn đăng xuất tài khoản này?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DangNhap frm = new DangNhap();
                frm.Show();
                this.Hide();
            }
        }

        private void họcCảiThiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form qlnd = new ThiLai();
            qlnd.Show();
        }

        private void danhSáchĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form qlnd = new Danh_Sách_Ddiemr();
            qlnd.Show();
        }

        private void FormChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
            
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr = new SinhVien();
            fr.Show();
        }

        private void quảnLýKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr = new Khoa();
            fr.Show();
        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr = new Lop();
            fr.Show();
        }

        private void quảnLýĐiểmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr = new FrmQLDSV();
            fr.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fr = new MonHoc();
            fr.Show();
        }

        private void quảnLýĐiểmHọcCảiThiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form qlhl = new frmDiemHocLai();
            qlhl.Show();
        }

        private void mnuDSTL_Click_1(object sender, EventArgs e)
        {
            Form tkhl = new frmHocLai();
            tkhl.Show();
        }

        private void họcCảiThiệnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form qlnd = new ThiLai();
            qlnd.Show();
        }

        private void danhSáchĐiểmToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form qlnd = new Danh_Sách_Ddiemr();
            qlnd.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
             Form tkd = new frmTKDiem();
            tkd.Show();
        }

        private void menuBarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            btnQLSV.Visible = menuBarToolStripMenuItem.Checked;
            btnQLL.Visible = menuBarToolStripMenuItem.Checked;
            btnQLDSV.Visible = menuBarToolStripMenuItem.Checked;
            btnQLK.Visible = menuBarToolStripMenuItem.Checked;
            btnQLMH.Visible = menuBarToolStripMenuItem.Checked;
            button1.Visible = menuBarToolStripMenuItem.Checked;
        }

        private void điểmTrungBìnhKhóaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tbkh = new Form2();
            tbkh.Show();
        }

        private void mnuWindows_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
