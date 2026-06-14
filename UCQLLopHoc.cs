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
        
        private int pageSize = 10;
        private int currentPage = 1;
        private int totalPage = 1;
        private string searchKeyword = "";

        public UCQLLopHoc()
        {
            InitializeComponent();
        }

        private void UCQLLopHoc_Load(object sender, EventArgs e)
        {
            textBox4.ReadOnly = true;
            LoadData();
        }

        private void LoadData()
        {
            databaseDataContext db = new databaseDataContext();
            var query = db.LopHocs.AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(lh =>
                    lh.malop.ToLower().Contains(searchKeyword) ||
                    lh.tenlop.ToLower().Contains(searchKeyword) ||
                    (lh.ghichu != null && lh.ghichu.ToLower().Contains(searchKeyword))
                );
            }

            int totalRecords = query.Count();
            totalPage = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPage == 0) totalPage = 1;

            if (currentPage > totalPage) currentPage = totalPage;
            if (currentPage < 1) currentPage = 1;

            lblTrang.Text = $"Trang {currentPage}/{totalPage}";

            dgv_DSLH.DataSource = query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(lh => new {
                    lh.id,
                    lh.malop,
                    lh.tenlop,
                    lh.ghichu
                })
                .ToList();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLop = textBox1.Text.Trim();
            string tenLop = textBox2.Text.Trim();
            string ghiChu = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã lớp học và Tên lớp học.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (db.LopHocs.Any(lh => lh.malop.ToLower() == maLop.ToLower()))
            {
                MessageBox.Show("Mã lớp học này đã tồn tại.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                LopHoc lh = new LopHoc
                {
                    malop = maLop,
                    tenlop = tenLop,
                    ghichu = ghiChu
                };

                db.LopHocs.InsertOnSubmit(lh);
                db.SubmitChanges();
                MessageBox.Show("Thêm lớp học thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một lớp học để sửa.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLop = textBox1.Text.Trim();
            string tenLop = textBox2.Text.Trim();
            string ghiChu = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(maLop) || string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã lớp học và Tên lớp học.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (db.LopHocs.Any(lh => lh.id != id && lh.malop.ToLower() == maLop.ToLower()))
            {
                MessageBox.Show("Mã lớp học này đã được sử dụng bởi lớp khác.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                LopHoc lh = db.LopHocs.FirstOrDefault(l => l.id == id);
                if (lh == null)
                {
                    MessageBox.Show("Không tìm thấy lớp học trong cơ sở dữ liệu.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lh.malop = maLop;
                lh.tenlop = tenLop;
                lh.ghichu = ghiChu;

                db.SubmitChanges();
                MessageBox.Show("Cập nhật lớp học thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox4.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xóa.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LopHoc lh = db.LopHocs.FirstOrDefault(l => l.id == id);
            if (lh == null)
            {
                MessageBox.Show("Không tìm thấy lớp học trong cơ sở dữ liệu.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lh.SinhViens.Any())
            {
                MessageBox.Show("Không thể xóa lớp học này vì đang có sinh viên tham gia.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này không?", "Xác nhận",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    db.LopHocs.DeleteOnSubmit(lh);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa lớp học thành công!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLamMoi_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            txtTimKiem.Clear();
            searchKeyword = "";
            currentPage = 1;
            LoadData();
        }

        private void dgv_DSLH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DSLH.Rows[e.RowIndex];
                textBox4.Text = row.Cells["id"].Value?.ToString();
                textBox1.Text = row.Cells["malop"].Value?.ToString();
                textBox2.Text = row.Cells["tenlop"].Value?.ToString();
                textBox3.Text = row.Cells["ghichu"].Value?.ToString();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            searchKeyword = txtTimKiem.Text.Trim().ToLower();
            currentPage = 1;
            LoadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPage)
            {
                currentPage++;
                LoadData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentPage = totalPage;
            LoadData();
        }

        private void dgv_DSLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox4.Text, out int idLop))
            {
                string maLop = textBox1.Text;
                string tenLop = textBox2.Text;
                using (DSSV frm = new DSSV(idLop, maLop, tenLop))
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp học để xem danh sách sinh viên.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
