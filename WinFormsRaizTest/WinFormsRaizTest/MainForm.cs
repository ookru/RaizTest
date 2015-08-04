using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WinFormsRaizTest.Classes;
using WinFormsRaizTest.Forms;

namespace WinFormsRaizTest
{
    public partial class MainForm : Form
    {

        private string SqlConString { get; set; }
        private string UserRole { get; set; }
        private Config Cfg { get; set; }

        public MainForm()
        {
            Cfg = new Config();
            InitializeComponent();
            
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            LoginForm LF = new LoginForm(Cfg);
            LF.ShowDialog();
            if (LF.Success == false)
            {
              //  Close();
            }
            else
            {

                Form FormForUserRole = null;
                SqlConString = LF.SqlConString;
                UserRole = LF.UserRole;
                switch (UserRole)
                {
                    case "Administrator":
                        FormForUserRole = new AdminForm(SqlConString);
                        break;
                    case "Operator":
                        FormForUserRole = new OperatorForm(SqlConString);
                        break;
                }
                FormForUserRole.ShowDialog();
            }
            Close();
        }
    }
}
