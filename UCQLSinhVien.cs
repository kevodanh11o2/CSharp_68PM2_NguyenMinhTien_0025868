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
            var dssv = db.SinhViens.Select(s => new {
                s.mssv,
                s.hoten,
                s.ngaysinh,
                s.gioitinh,
                s.lop
               
            }).ToList();
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
            
            
            
            // Use the class‑level context (db) for insertion
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

        private void grbThongTinSinhVien_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_DSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_DSSV.Rows[e.RowIndex];

                mssv.Text = row.Cells["mssv"].Value?.ToString();
                hovaten.Text = row.Cells["hoten"].Value?.ToString();

                if (row.Cells["ngaysinh"].Value != null && row.Cells["ngaysinh"].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["ngaysinh"].Value);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now;
                }

                cboGioiTinh.Text = row.Cells["gioitinh"].Value?.ToString() ?? "Nam";

                if (row.Cells["lop"].Value != null && row.Cells["lop"].Value != DBNull.Value)
                {
                    cboLop.SelectedValue = Convert.ToInt32(row.Cells["lop"].Value);
                }
                else
                {
                    cboLop.SelectedIndex = -1;
                }
            }
        }

        private void dgv_DSSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra dòng được chọn
            if (dgv_DSSV.CurrentRow == null || dgv_DSSV.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã sinh viên (khóa chính) từ dòng đang chọn
            string maSinhVien = dgv_DSSV.CurrentRow.Cells["mssv"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                MessageBox.Show("Mã sinh viên không hợp lệ.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tìm bản ghi trong DB
            SinhVien sv = db.SinhViens.FirstOrDefault(s => s.mssv == maSinhVien);
            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên trong cơ sở dữ liệu.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật các trường từ giao diện
            sv.hoten = hovaten.Text.Trim();
            sv.ngaysinh = dtpNgaySinh.Value;
            sv.gioitinh = cboGioiTinh.Text;
            sv.lop = (cboLop.SelectedValue != null) ?
                     Convert.ToInt32(cboLop.SelectedValue) : (int?)null;

            // Lưu lại
            try
            {
                db.SubmitChanges();
                MessageBox.Show("Cập nhật thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete selected sinhvien
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra dòng được chọn
            if (dgv_DSSV.CurrentRow == null || dgv_DSSV.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã sinh viên
            string maSinhVien = dgv_DSSV.CurrentRow.Cells["mssv"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maSinhVien))
            {
                MessageBox.Show("Mã sinh viên không hợp lệ.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tìm bản ghi
            SinhVien sv = db.SinhViens.FirstOrDefault(s => s.mssv == maSinhVien);
            if (sv == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên trong cơ sở dữ liệu.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xóa và lưu
            try
            {
                db.SinhViens.DeleteOnSubmit(sv);
                db.SubmitChanges();
                MessageBox.Show("Xóa thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
