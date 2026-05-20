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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            UCQLSinhVien uCQLSinhVien = new UCQLSinhVien();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uCQLSinhVien);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UCQLSinhVien uCQLSinhVien = new UCQLSinhVien();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uCQLSinhVien);
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCQLLopHoc uCQLLH = new UCQLLopHoc();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(uCQLLH);
        }
    }
}
