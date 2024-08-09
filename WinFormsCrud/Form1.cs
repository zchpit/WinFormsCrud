using WinFormsCrud.Dto;
using WinFormsCrud.Interface;

namespace WinFormsCrud
{
    public partial class Form1 : Form
    {
        ILoginService loginService;
        UserDto loggedUser = null;

        public Form1(ILoginService loginService)
        {
            InitializeComponent();

            this.loginService = loginService;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            string user = tbUser.Text;
            string password = tbPassword.Text;

            loggedUser = loginService.Login(user, password);

            if (loggedUser != null)
            {
                gbLogin.Visible = false;
                btnLogout.Visible = true;

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

            tbUser.Text = string.Empty;
            tbPassword.Text = string.Empty;
        }
    }
}
