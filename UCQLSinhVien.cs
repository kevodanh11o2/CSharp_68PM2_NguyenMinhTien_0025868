using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharp_68PM2_NguyenMinhTien_0025868
{
    public partial class UCQLSinhVien : UserControl
    {
        databaseDataContext db = new databaseDataContext();
        public UCQLSinhVien()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UCQLSinhVien_Load(object sender, EventArgs e)
        {
           LoadData();
        }
        private void LoadData()
        {
            List<SinhVien> dssv = db.SinhViens.ToList();
            dgv_DSSV.DataSource = dssv;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string HoVaten = hovaten.Text;
            string MaSinhVien = mssv.Text;
            DateTime NgaySinh = dtpNgaySinh.Value;
            string GioiTinh = cboGioiTinh.Text;
            
            SinhVien sv = new SinhVien();
            sv.gioitinh = GioiTinh;
            sv.ngaysinh = NgaySinh;
            sv.hoten = HoVaten;
            sv.mssv = MaSinhVien;
            
            
            
            databaseDataContext db = new databaseDataContext();
            db.SinhViens.InsertOnSubmit(sv);
            db.SubmitChanges(); 
            LoadData();



        }
        
        private void mssv_TextChanged(object sender, EventArgs e)
        {

        }

        private void hovaten_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
