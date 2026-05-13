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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emailSinhVien = "tien0025868@gmail.com";
            string mssv = "0025868";

            string emailNhap = txtEmail.Text.Trim();
            string matkhauNhap = txtPassword.Text.Trim();

            if (emailNhap==emailSinhVien && matkhauNhap==mssv)
            {
                MessageBox.Show("Đăng nhập thành công.");

            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại ");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
