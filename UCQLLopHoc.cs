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
    public partial class UCQLLopHoc : UserControl
    {
        databaseDataContext db = new databaseDataContext();
        public UCQLLopHoc()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void grbThongTinSinhVien_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_DSLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UCQLSinhVien_Load(object sender, EventArgs e)
        {
            List<LopHoc> dssv = db.LopHocs.ToList();
            dgv_DSLH.DataSource = dssv;
        }
    }
}
