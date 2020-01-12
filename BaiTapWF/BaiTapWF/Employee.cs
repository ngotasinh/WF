
#region Using

using System;
using System.Data;
using System.Windows.Forms;

#endregion

namespace BaiTapWF
{
    /// <summary>
    /// Class Quản lý thông tin nhân viên
    /// </summary>
    public partial class Employee : Form
    {
        #region Các biến private

        /// <summary>
        /// Khởi tạo biến lấy vị trí của row đã click vào
        /// </summary>
        private int indexRow;

        /// <summary>
        /// statusButtonSave Biến trạng thái button save
        /// statusButtonSave = 1 : Add dữ liệu mới
        /// statusButtonSave = 2 : Update dữ liệu
        /// </summary>
        private int statusButtonSave = 0;

        /// <summary>
        /// Khởi tạo DataTable
        /// </summary>
        private DataTable dataTableEmployee = new DataTable();

        #endregion

        /// <summary>
        /// Hàm Khởi tạo
        /// </summary>
        public Employee()
        {
            InitializeComponent();
        }

        #region Các hàm private

        #region EnnabledButton(bool status); Hàm trạng thái các nút button

        /// <summary>
        /// EnabledButton(bool status): Hàm trạng thái các nút button
        /// status = false : Khóa 3 nút add, edit,delete và hiện 2 nút cancel, save
        /// status = true : Hiện 3 nút add, edit,delete và khóa 2 nút cancel, save
        /// </summary>
        /// <param name="status"></param>
        private void EnabledButton(bool status)
        {
            // Enabled 3 nút add, edit, delete theo status
            buttonAdd.Enabled = status;
            buttonEdit.Enabled = status;
            buttonDelete.Enabled = status;

            // Enabled 2 nút cancel, save theo status
            buttonSave.Enabled = !status;
            buttonCancel.Enabled = !status;
        }

        #endregion

        #region EnabledControl(bool status): Hàm chỉ cho phép hay không cho phép nhập dữ liệu

        /// <summary>
        /// Hàm chỉ cho phép hay không cho phép nhập dữ liệu
        /// status = false : không cho phép nhập dữ liệu
        /// status = true: cho phép nhập dữ liệu
        /// </summary>
        /// <param name="status"></param>
        private void EnabledControl(bool status)
        {
            textBoxID.Enabled = status;
            textBoxName.Enabled = status;
            dateTimePickerBirth.Enabled = status;
            comboBoxSex.Enabled = status;
            textBoxAddress.Enabled = status;
            textBoxEmail.Enabled = status;
            textBoxNoNumber.Enabled = status;
            checkBoxFlagDel.Enabled = status;
        }

        #endregion

        #region ResetControl(): Hàm reset các control

        /// <summary>
        /// ResetControl(): Hàm reset các control
        /// </summary>
        private void ResetControl()
        {
            textBoxID.Text = string.Empty;
            textBoxName.Text = string.Empty;
            dateTimePickerBirth.Text = string.Empty;
            comboBoxSex.Text = string.Empty;
            textBoxAddress.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
            textBoxNoNumber.Text = string.Empty;
            checkBoxFlagDel.Checked = false;
        }

        #endregion

        #region EnableButtonDefault(): Hàm các trạng thái mặc định của button

        /// <summary>
        /// EnableButtonDefault(): Hàm các trạng thái mặc định của button
        /// </summary>
        private void EnableButtonDefault()
        {
            buttonAdd.Enabled = true;
            buttonEdit.Enabled = false;
            buttonDelete.Enabled = false;
            buttonSave.Enabled = false;
            buttonCancel.Enabled = false;
        }

        #endregion

        #region AddRowDataGridView() : Hàm add row dataGridViewEmployee

        /// <summary>
        /// AddRowDataGridView() : Hàm add row dataGridViewEmployee
        /// </summary>
        private void AddRowDataGridView()
        {

            foreach (DataRow dr in dataTableEmployee.Rows)
            {
                dataGridViewEmployee.Rows.Add(dr.ItemArray);
            }
        }

        #endregion

        #endregion

        #region Các Event button

        #region Event ButtonAdd_Click kich vào nút Add

        /// <summary>
        /// Event ButtonAdd_Click kich vào nút ADD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            // Gọi hàm reset các control
            ResetControl();

            // Gọi hàm  hiện các control để nhập dữ liệu
            EnabledControl(true);

            // Gọi hàm ẩn hiện các nút button
            EnabledButton(false);

            // Chuyển biến trạng thái sang add
            statusButtonSave = 1;
        }

        #endregion

        #region ButtonEdit_Click: Event click vào nút Edit cho chỉnh sửa dữ liệu

        /// <summary>
        /// ButtonEdit_Click: Event click vào nút Edit cho chỉnh sửa dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // Gọi hàm cho phép chỉnh sửa dữ liệu các control
            EnabledControl(true);

            // Gọi hàm trạng thái các nút button khóa 3 nút add,edit,delete
            EnabledButton(false);

            // Gắn biến statusButtonSave sang chế độ edit
            statusButtonSave = 2;

            // Chỉ cho phép đọc mã nhân viên không cho phép chỉnh sửa
            textBoxID.ReadOnly = true;
        }

        #endregion

        #region ButtonDelete_Click: Event khi click vào nút delete

        /// <summary>
        /// ButtonDelete_Click: Event khi click vào nút delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            //Xác nhận xóa
            if (MessageBox.Show("Bạn có muốn xóa không?", "Remove Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dataGridViewEmployee.Rows.RemoveAt(indexRow);
            }

            // cancel xác nhận xóa
            else
            {
                MessageBox.Show("Dòng vẫn chưa xóa", "Remove Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region ButtonCancel_Click: Event click vào thì sẽ thoát trạng thái

        /// <summary>
        /// ButtonCancel_Click: Event click vào thì sẽ thoát trạng thái
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            // Gọi hàm EnableControl khóa các control
            EnabledControl(false);

            // Gọi hàm trạng thái mặc định các nút button
            EnableButtonDefault();

            // Gọi hàm ResetControl reset các control
            ResetControl();
        }

        #endregion

        #region Event ButtonSave_Click lưu dữ liệu nhân viên

        /// <summary>
        /// Event ButtonSave_Click lưu dữ liệu nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Add mới nhân viên
            if (statusButtonSave == 1)
            {
                // Remove dữ liệu trong dataGridViewEmployee
                for (int i = dataGridViewEmployee.Rows.Count - 1; i >= 0; i--)
                {
                    dataGridViewEmployee.Rows.RemoveAt(i);
                }

                // Đổ dữ liệu vào dataTableEmployee
                this.dataTableEmployee.Rows.Add("1"
                                           , textBoxID.Text
                                           , textBoxName.Text
                                           , dateTimePickerBirth.Text
                                           , comboBoxSex.Text
                                           , textBoxAddress.Text
                                           , textBoxEmail.Text
                                           , textBoxNoNumber.Text
                                           , (bool)checkBoxFlagDel.Checked);

                // Gọi hàm Add row dataGridViewEmployee
                AddRowDataGridView();
            }

            // Update thông tin nhân viên
            else if (statusButtonSave == 2)
            {
                // Khởi tạo biến nhân giá trị dataGridViewEmployee với dòng indexRow
                DataGridViewRow editDataRow = dataGridViewEmployee.Rows[indexRow];

                // Cập nhật giá trị đã thay đổi
                editDataRow.Cells["HoTen"].Value = textBoxName.Text;
                editDataRow.Cells["NgaySinh"].Value = dateTimePickerBirth.Text;
                editDataRow.Cells["GioiTinh"].Value = comboBoxSex.Text;
                editDataRow.Cells["DiaChi"].Value = textBoxAddress.Text;
                editDataRow.Cells["Email"].Value = textBoxEmail.Text;
                editDataRow.Cells["SoDienThoai"].Value = textBoxNoNumber.Text;
                editDataRow.Cells["FlagDel"].Value = checkBoxFlagDel.Checked;
            }
            // Gọi hàm EnableControl khóa các control
            EnabledControl(false);

            // Gọi hàm trạng thái mặc định các nút button
            EnableButtonDefault();

            // Gọi hàm ResetControl reset các control
            ResetControl();
        }

        #endregion

        #endregion

        #region Event Employee_Load load from Employee

        /// <summary>
        /// Event Employee_Load load from Employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employee_Load(object sender, EventArgs e)
        {
            // Giá trị mặt định combobox giới tính.
            this.comboBoxSex.SelectedItem = "Nam";

            // Không tự động tạo dòng mới
            this.dataGridViewEmployee.AllowUserToAddRows = false;

            // Add colums cho dataTable
            dataTableEmployee.Columns.Add("STT", typeof(string));
            dataTableEmployee.Columns.Add("ID", typeof(string));
            dataTableEmployee.Columns.Add("HoTen", typeof(string));
            dataTableEmployee.Columns.Add("NgaySinh", typeof(string));
            dataTableEmployee.Columns.Add("GioiTinh", typeof(string));
            dataTableEmployee.Columns.Add("DiaChi", typeof(string));
            dataTableEmployee.Columns.Add("Email", typeof(string));
            dataTableEmployee.Columns.Add("SoDienThoai", typeof(string));
            dataTableEmployee.Columns.Add("FlagDel", typeof(bool));
        }

        #endregion

        #region Event TextBoxNoNumber_KeyPress chỉ cho nhập số

        /// <summary>
        /// Event TextBoxNoNumber_KeyPress chỉ cho nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxNoNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Xác thực rằng phím vừa nhấn không phải CTRL hoặc không phải dạng số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region DataGridViewEmployee_CellClick: Event click  vào 

        /// <summary>
        /// DataGridViewEmployee_CellClick: Event click  vào 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get the selected Row Index
            indexRow = e.RowIndex; 

            // Khởi tạo biến row tại ví trí hiện tại
            DataGridViewRow row = dataGridViewEmployee.Rows[indexRow];

            // Hiển thị các nút button edit, delete
            buttonEdit.Enabled = true;
            buttonDelete.Enabled = true;

            // Biding dữ liệu vào các control
            textBoxID.Text = row.Cells["ID"].Value.ToString();
            textBoxName.Text = row.Cells["HoTen"].Value.ToString();
            dateTimePickerBirth.Text = row.Cells["NgaySinh"].Value.ToString();
            comboBoxSex.Text = row.Cells["GioiTinh"].Value.ToString();
            textBoxAddress.Text = row.Cells["DiaChi"].Value.ToString();
            textBoxEmail.Text = row.Cells["Email"].Value.ToString();
            textBoxNoNumber.Text = row.Cells["SoDienThoai"].Value.ToString();
            checkBoxFlagDel.Checked = (bool)row.Cells["FlagDel"].Value;
        }

        #endregion
    }
}
