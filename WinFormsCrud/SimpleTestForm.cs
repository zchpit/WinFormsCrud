using CommonLibrary.Dto;
using System.Windows.Forms;
using WinFormsCrud.Helper;
using WinFormsCrud.Interface;
using WinFormsCrud.IServices;
using Microsoft.Extensions.Configuration;
using NLog;

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

                ReloadGridData(loggedUser);

                tbUser.Text = string.Empty;
                tbPassword.Text = string.Empty;

                btnGenerateReport.Visible = loggedUser.UserRole == RoleDto.Manager;
            }
            else
            {
                lbError.Visible = true;
                logger.Warn(string.Concat("Login failed for user: ",user));
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
                await caseService.UpdateCase(caseDto, loggedUser.Id);
                ReloadGridData(loggedUser);
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
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
                    await caseService.UpdateCase(selectedCase, loggedUser.Id);
                    ReloadGridData(loggedUser);
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete record: " + selectedCase.Header;
            string confirmeDelete = "Confirm Delete!!";

            var confirmResult = MessageBox.Show(message, confirmeDelete, MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                selectedCase.IsDeleted = true;
                selectedCase.DeletedDate = DateTime.Now;
                selectedCase.DeletedBy = loggedUser.Id;

                await caseService.UpdateCase(selectedCase, loggedUser.Id);
                ReloadGridData(loggedUser);
            }
        }

        private async void ReloadGridData(SimpleUserDto simpleUserDto)
        {
            dgvCases.DataSource = await caseService.GetUserCases(simpleUserDto);
        }

        private void SetCaseValuesFromUI(CaseDto caseDto)
        {
            caseDto.Header = tbHeader.Text;
            caseDto.Description = tbDescription.Text;
            caseDto.Priority = Int32.Parse(nudPriority.Value.ToString());

            caseDto.LastModifiedDate = DateTime.Now;
            caseDto.LastModifiedBy = loggedUser.Id;
        }

        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportName = AppSettings.ReportFileSettings().ReportName;
            string reportLocation = AppSettings.ReportFileSettings().ReportLocation;

            string fileName = string.Concat(reportLocation, reportName);

            var reportEntities = await reportService.GetReport(loggedUser.Id);
            if (reportEntities == null)
            {
                string message = string.Concat("Error occurred while creating report. Report not created");
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
    }
}
