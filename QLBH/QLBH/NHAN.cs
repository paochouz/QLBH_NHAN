namespace QLBH
{
    public partial class NHAN : Form
    {
        private int selectedRowIndex = -1;
        public NHAN()
        {
            InitializeComponent();
            dateTimePicker_NgayNhan.Value = DateTime.Now;
            dateTimePicker_ThgianNhan.Value = DateTime.Now;
            dateTimePicker_NgayNhan.Visible = true;
            dateTimePicker_ThgianNhan.Visible = true;

            // Liên kết sự kiện CheckedChanged
            checkBox_PTTTTienMat.CheckedChanged += checkBox_PTTTTienMat_CheckedChanged;
            checkBox_PTTTCKhoan.CheckedChanged += checkBox_PTTTCKhoan_CheckedChanged;

            // Sự kiện CellClick để xử lý Sửa/Xóa
            dataGridView_HoadonNhap.CellClick += dataGridView_HoadonNhap_CellClick;
        }
        private void checkBox_PTTTTienMat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PTTTTienMat.Checked)
            {
                checkBox_PTTTCKhoan.Checked = false;
            }
        }

        // Sự kiện khi chọn Chuyển Khoản
        private void checkBox_PTTTCKhoan_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PTTTCKhoan.Checked)
            {
                checkBox_PTTTTienMat.Checked = false;
            }
        }
        private void dateTimePicker_NgayNhan_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dateTimePicker_ThgianNhan_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_Them_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_MaDDH.Text) ||
                string.IsNullOrWhiteSpace(textBox_MaNV.Text) ||
                string.IsNullOrWhiteSpace(textBox_MaNCC.Text) ||
                string.IsNullOrWhiteSpace(textBox_MaLH.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra ngày nhận có nhỏ hơn hoặc bằng ngày hiện tại không
            if (dateTimePicker_NgayNhan.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày nhận phải nhỏ hơn hoặc bằng ngày hiện tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get payment method
            string pttt = "";
            if (checkBox_PTTTTienMat.Checked) pttt += "Tiền mặt ";
            if (checkBox_PTTTCKhoan.Checked) pttt += "Chuyển khoản";
            if (string.IsNullOrWhiteSpace(pttt))
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create message with all information
            // Thêm dữ liệu vào DataGridView
            dataGridView_HoadonNhap.Rows.Add(
                textBox_MaDDH.Text,
                textBox_MaNV.Text,
                textBox_MaNCC.Text,
                textBox_MaLH.Text,
                dateTimePicker_NgayNhan.Value.ToShortDateString(),
                dateTimePicker_ThgianNhan.Value.ToLongTimeString(),
                pttt
            );
            ClearForm();
        }


        private void dataGridView_HoadonNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lưu lại chỉ số hàng đang chọn
                selectedRowIndex = e.RowIndex;

                // Load dữ liệu lên form
                textBox_MaDDH.Text = dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox_MaNV.Text = dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox_MaNCC.Text = dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox_MaLH.Text = dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[3].Value.ToString();
                dateTimePicker_NgayNhan.Value = DateTime.Parse(dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[4].Value.ToString());
                dateTimePicker_ThgianNhan.Value = DateTime.Parse(dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[5].Value.ToString());

                string pttt = dataGridView_HoadonNhap.Rows[e.RowIndex].Cells[6].Value.ToString();
                checkBox_PTTTTienMat.Checked = pttt == "Tiền mặt";
                checkBox_PTTTCKhoan.Checked = pttt == "Chuyển khoản";
            }
        }

        private void button_Sua_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0)
            {
                if (string.IsNullOrWhiteSpace(textBox_MaDDH.Text) ||
                    string.IsNullOrWhiteSpace(textBox_MaNV.Text) ||
                    string.IsNullOrWhiteSpace(textBox_MaNCC.Text) ||
                    string.IsNullOrWhiteSpace(textBox_MaLH.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[0].Value = textBox_MaDDH.Text;
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[1].Value = textBox_MaNV.Text;
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[2].Value = textBox_MaNCC.Text;
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[3].Value = textBox_MaLH.Text;
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[4].Value = dateTimePicker_NgayNhan.Value.ToShortDateString();
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[5].Value = dateTimePicker_ThgianNhan.Value.ToLongTimeString();
                dataGridView_HoadonNhap.Rows[selectedRowIndex].Cells[6].Value = checkBox_PTTTTienMat.Checked ? "Tiền mặt" : "Chuyển khoản";

                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void button_Xoa_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0)
            {
                dataGridView_HoadonNhap.Rows.RemoveAt(selectedRowIndex);
                ClearForm();
            }
        }
        private void ClearForm()
        {
            textBox_MaDDH.Clear();
            textBox_MaNV.Clear();
            textBox_MaNCC.Clear();
            textBox_MaLH.Clear();
            dateTimePicker_NgayNhan.Value = DateTime.Now;
            dateTimePicker_ThgianNhan.Value = DateTime.Now;
            checkBox_PTTTTienMat.Checked = false;
            checkBox_PTTTCKhoan.Checked = false;
            selectedRowIndex = -1;
        }
    }
}
