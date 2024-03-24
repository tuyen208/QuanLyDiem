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
    public partial class frmHocLai : Form
    {
        public frmHocLai()
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
            string sql = "select * from KETQUA1 where DiemChu = N'F' ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dataGridView1.DataSource = dtKQ;
        }

        private void frmHocLai_Load(object sender, EventArgs e)
        {
            moketnoi();
            hienthi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = "select * from KETQUA1 where MaSV = '" + txtMaSV.Text + "' and DiemChu = N'F'  ";
            DataTable dtKQ = new DataTable();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            dtKQ = ds.Tables[0];
            dataGridView1.DataSource = dtKQ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1.Export2Excel(dataGridView1);
        }

        private void frmHocLai_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
