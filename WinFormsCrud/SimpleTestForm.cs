using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

namespace WinFormsCrud
{
    public partial class SimpleTestForm : Form
    {
        IUserService userService;
        ICaseService caseService;

        SimpleUserDto? loggedUser;
        CaseDto selectedCase = null;

        public SimpleTestForm(IUserService userService, ICaseService caseService)
        {
            InitializeComponent();

            this.userService = userService;
            this.caseService = caseService;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            string user = tbUser.Text;
            string password = tbPassword.Text;

            loggedUser = userService.Login(user, password);
            if (loggedUser != null)
            {
                gbLogin.Visible = false;
                btnLogout.Visible = true;
                dgvCases.Visible = true;
                gbEditRow.Visible = loggedUser.UserRole == RoleDto.User ? true : false;

                ReloadGridData(loggedUser.Id);

                tbUser.Text = string.Empty;
                tbPassword.Text = string.Empty;
            }
            else
            {
                lbError.Visible = true;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            loggedUser = null;

            gbLogin.Visible = true;
            btnLogout.Visible = false;
            dgvCases.Visible = false;
            gbEditRow.Visible = false;

            tbUser.Text = string.Empty;
            tbPassword.Text = string.Empty;
        }

        private void dgvCases_RowEnter(object sender, EventArgs e)
        {
            var dataGridEventArg = e as DataGridViewCellEventArgs;
            if (dataGridEventArg != null && loggedUser != null)
            {
                List<CaseDto> cases = dgvCases.DataSource as List<CaseDto>;
                selectedCase = cases[dataGridEventArg.RowIndex];
                tbHeader.Text = selectedCase.Header;
                tbDescription.Text = selectedCase.Description;
                nudPriority.Value = selectedCase.Priority;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "Can't add new value. Validation error.";
            CaseDto caseDto = new CaseDto();
            caseDto.CreatedBy = loggedUser.Id;

            SetCaseValuesFromUI(caseDto);

            bool isCaseValid = caseService.IsValidCase(caseDto);
            if (!isCaseValid)
            {
                MessageBox.Show(message);
            }
            else
            {
                caseService.UpdateCase(caseDto, loggedUser.Id);
                ReloadGridData(loggedUser.Id);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to edit record: " + selectedCase.Header;
            string confirmeDelete = "Confirm Edit!!";

            var confirmResult = MessageBox.Show(message, confirmeDelete, MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string validationMessage = "Can't edit existing value. Validation error.";
                SetCaseValuesFromUI(selectedCase);

                bool isCaseValid = caseService.IsValidCase(selectedCase);
                if (!isCaseValid)
                {
                    MessageBox.Show(validationMessage);
                }
                else
                {
                    caseService.UpdateCase(selectedCase, loggedUser.Id);
                    ReloadGridData(loggedUser.Id);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete record: " + selectedCase.Header;
            string confirmeDelete = "Confirm Delete!!";

            var confirmResult = MessageBox.Show(message, confirmeDelete, MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                selectedCase.IsDeleted = true;
                selectedCase.DeletedDate = DateTime.Now;
                selectedCase.DeletedBy = loggedUser.Id;

                caseService.UpdateCase(selectedCase, loggedUser.Id);
                ReloadGridData(loggedUser.Id);
            }
        }

        private void ReloadGridData(int userId)
        {
            dgvCases.DataSource = caseService.GetUserCases(userId);
        }

        private void SetCaseValuesFromUI(CaseDto caseDto)
        {
            caseDto.Header = tbHeader.Text;
            caseDto.Description = tbDescription.Text;
            caseDto.Priority = Int32.Parse(nudPriority.Value.ToString());

            caseDto.LastModifiedDate = DateTime.Now;
            caseDto.LastModifiedBy = loggedUser.Id;
        }
    }
}
