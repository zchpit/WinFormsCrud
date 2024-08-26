using CommonLibrary.Dto;
using Microsoft.Extensions.Configuration;
using NLog;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;
using WinFormsCrud.IServices;

namespace WinFormsCrud
{
    public partial class SimpleTestForm : Form
    {
        IUserService userService;
        ICaseService caseService;
        IReportService reportService;
        private readonly IConfiguration _configuration;
        ILogger logger;

        SimpleUserDto? loggedUser;
        CaseDto selectedCase = null;

        public SimpleTestForm(IUserService userService, ICaseService caseService, IReportService reportService, ILogger logger)
        {
            InitializeComponent();

            this.userService = userService;
            this.caseService = caseService;
            this.reportService = reportService;
            this.logger = logger;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            string user = tbUser.Text;
            string password = tbPassword.Text;

            loggedUser = await userService.Login(user, password);
            if (loggedUser != null)
            {
                logger.Info(string.Concat("Login sucess for user: ", user));

                gbLogin.Visible = false;
                btnLogout.Visible = true;
                dgvCases.Visible = true;
                gbEditRow.Visible = true;

                await ReloadGridData(loggedUser.Id, loggedUser.UserRole);

                tbUser.Text = string.Empty;
                tbPassword.Text = string.Empty;

                btnGenerateReport.Visible = loggedUser.UserRole == RoleDto.Manager;
            }
            else
            {
                lbError.Visible = true;
                logger.Warn(string.Concat("Login failed for user: ", user));
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

                if (selectedCase.CreatedBy != loggedUser.Id)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "Can't add new value. Validation error.";
            CaseCreateDto caseCreateDto = CaseCreateDtoFromUI(loggedUser.Id);

            bool isCaseValid = caseService.IsValidCase(caseCreateDto);
            if (!isCaseValid)
            {
                MessageBox.Show(message);
            }
            else
            {
                message = string.Concat("Error occurred while adding user case. Please contact with administration.");

                var success = await caseService.CreateCase(caseCreateDto);
                await HandleCaseServiceCall(success, message, loggedUser.Id, loggedUser.UserRole);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to edit record: " + selectedCase.Header;
            string confirmeDelete = "Confirm Edit!!";
            CaseUpdateDto caseUpdateDto = CaseUpdateDtoFromUI(selectedCase.Id, loggedUser.Id);

            var confirmResult = MessageBox.Show(message, confirmeDelete, MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                message = string.Concat("Error occurred while edditing user case. Please contact with administration.");

                var success = await caseService.UpdateCase(caseUpdateDto);
                await HandleCaseServiceCall(success, message, loggedUser.Id, loggedUser.UserRole);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete record: " + selectedCase.Header;
            string confirmeDelete = "Confirm Delete!!";

            var confirmResult = MessageBox.Show(message, confirmeDelete, MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                message = string.Concat("Error occurred while deleting user case. Please contact with administration.");
                var success = await caseService.DeleteCase(selectedCase.Id, loggedUser.Id);

                await HandleCaseServiceCall(success, message, loggedUser.Id, loggedUser.UserRole);
            }
        }

        private async Task HandleCaseServiceCall(bool success, string message, int userId, RoleDto userRole)
        {
            if (success)
            {
                await ReloadGridData(userId, userRole);
            }
            else
            {
                message = string.Concat("Error occurred while edditing user case. Please contact with administration.");
                MessageBox.Show(message);
            }
        }

        private async Task ReloadGridData(int userId, RoleDto userRole)
        {
            var result = await caseService.GetUserCases(userId, userRole);
            if (result == null)
            {
                string message = string.Concat("Error occurred while geting user cases. Please contact with administration.");
                MessageBox.Show(message);
            }
            else
            {
                dgvCases.DataSource = result;
            }
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportName = AppSettings.ReportFileSettings().ReportName;
            string reportLocation = AppSettings.ReportFileSettings().ReportLocation;

            string fileName = string.Concat(reportLocation, reportName);

            var reportEntities = await reportService.GetReport(loggedUser.Id);
            if (reportEntities == null)
            {
                string message = string.Concat("Error occurred while creating report. Report not created. Please contact with administration.");
                MessageBox.Show(message);
            }
            else
            {
                reportService.CreateFolderIfNotExists(reportLocation);
                string savedReportLocation = await reportService.SaveReportToDisc(reportEntities, fileName);

                if (!string.IsNullOrEmpty(savedReportLocation))
                {
                    string message = string.Concat("File created on: ", savedReportLocation);
                    MessageBox.Show(message);
                }
            }
        }

        private CaseCreateDto CaseCreateDtoFromUI(int loggedUserId)
        {
            return new CaseCreateDto()
            {
                Header = tbHeader.Text,
                Description = tbDescription.Text,
                Priority = Int32.Parse(nudPriority.Value.ToString()),
                CreatedBy = loggedUserId,
                CreateDate = DateTime.UtcNow,
                LastModifiedBy = loggedUserId,
                LastModifiedDate = DateTime.UtcNow
            };
        }

        private CaseUpdateDto CaseUpdateDtoFromUI(int selectedCaseId, int loggedUserId)
        {
            return new CaseUpdateDto()
            {
                Id = selectedCaseId,
                Header = tbHeader.Text,
                Description = tbDescription.Text,
                Priority = Int32.Parse(nudPriority.Value.ToString()),
                LastModifiedDate = DateTime.UtcNow,
                LastModifiedBy = loggedUserId
            };
        }
    }
}
