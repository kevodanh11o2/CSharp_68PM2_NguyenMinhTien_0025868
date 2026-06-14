using System;
using System.Linq;
using System.Windows.Forms;

namespace CSharp_68PM2_NguyenMinhTien_0025868
{
    public partial class DSSV : Form
    {
        private readonly int _idLop;

        public DSSV(int idLop, string maLop, string tenLop)
        {
            InitializeComponent();
            _idLop = idLop;
            this.Text = $"Danh sách sinh viên – Lớp {maLop}: {tenLop}";
        }

        private void DSSV_Load(object sender, EventArgs e)
        {
            var db = new databaseDataContext();

            dgvSinhVien.DataSource = db.SinhViens
                .Where(sv => sv.lop == _idLop)
                .Select(sv => new
                {
                    MaSV     = sv.mssv,
                    HoVaTen  = sv.hoten,
                    NgaySinh = sv.ngaysinh,
                    GioiTinh = sv.gioitinh
                })
                .ToList();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}