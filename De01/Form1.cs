using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace De01
{
    public partial class frmSinhvien : Form
    {
        public frmSinhvien()
        {
            InitializeComponent();
        }
        private SVContextDB context = new SVContextDB();

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            try
            {
                List<SinhVien> listsinhvien = context.SinhViens.ToList();
                BindGrid(context.SinhViens.ToList());
                FillFacultyCombobox(context.Lops.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFacultyCombobox(List<Lop> listlop)
        {
            this.cboLop.DataSource = listlop;
            this.cboLop.DisplayMember = "TenLop";
            this.cboLop.ValueMember = "MaLop";
        }
        private void BindGrid(List<SinhVien> listsinhvien)
        {
            lvSinhvien.Items.Clear();
            foreach (var item in listsinhvien)
            {
                ListViewItem lv = new ListViewItem(item.MaSV);
                lv.SubItems.Add(item.HotenSV);
                lv.SubItems.Add(item.Ngaysinh.ToString());
                lv.SubItems.Add(item.Lop.TenLop.ToString());
                lvSinhvien.Items.Add(lv);
            }
        }

        private void lvSinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhvien.SelectedItems.Count > 0)
            {
                txtMaSV.Text = lvSinhvien.SelectedItems[0].SubItems[0].Text;
                txtHotenSV.Text = lvSinhvien.SelectedItems[0].SubItems[1].Text;
                dtNgaysinh.Text = lvSinhvien.SelectedItems[0].SubItems[2].Text;
                cboLop.Text = lvSinhvien.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            SVContextDB contextdb = new SVContextDB();
            SinhVien sv = contextdb.SinhViens.FirstOrDefault(s => s.MaSV == txtMaSV.Text);
            if (sv == null)
            {
                SinhVien sinhVien = new SinhVien()
                {
                    MaSV = txtMaSV.Text,
                    HotenSV = txtHotenSV.Text,
                    Ngaysinh = DateTime.Parse(dtNgaysinh.Text),
                    MaLop = (cboLop.SelectedItem as Lop).MaLop,
                };
                contextdb.SinhViens.Add(sinhVien);
                contextdb.SaveChanges();
                BindGrid(context.SinhViens.ToList());
                MessageBox.Show("Thêm thành công!");
            }
            else
            {
                MessageBox.Show("Đã có dữ liệu!");
            } 
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn xóa ? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                SVContextDB context = new SVContextDB();
                SinhVien dbDelete = context.SinhViens.FirstOrDefault(p => p.MaSV == txtMaSV.Text);
                if (dbDelete != null)
                {
                    context.SinhViens.Remove(dbDelete);
                    context.SaveChanges();
                    BindGrid(context.SinhViens.ToList());
                }
            }
            MessageBox.Show("Xóa thành công!");
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            try
            {
                SVContextDB context = new SVContextDB();
                SinhVien dbupdate = context.SinhViens.FirstOrDefault(t => t.MaSV == txtMaSV.Text);
                if (dbupdate != null)
                {
                    dbupdate.HotenSV = txtHotenSV.Text;
                    dbupdate.Ngaysinh = dtNgaysinh.Value;
                    dbupdate.MaLop = (cboLop.SelectedItem as Lop).MaLop;

                    context.SaveChanges();
                    BindGrid(context.SinhViens.ToList());
                    MessageBox.Show("Sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc muốn thoát ? ","Thông báo", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (kq == DialogResult.OK)
            {
                this.Close();
            }
        }

    }


}
