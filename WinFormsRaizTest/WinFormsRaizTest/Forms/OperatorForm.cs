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

namespace WinFormsRaizTest.Forms
{
    public partial class OperatorForm : Form
    {

        private SqlCommand SQLComm { get; set; }
        private string SQLConString { get; set; }

        public OperatorForm(string sqlConString)
        {
            InitializeComponent();
            SQLConString = sqlConString;
        }

        private void ClearFields()
        {
            foreach (TextBox txtBx in this.Controls.OfType<TextBox>())
            {
               txtBx.Text = string.Empty;
            }

            foreach (CheckBox chkBx in this.Controls.OfType<CheckBox>())
            {
               chkBx.Checked=false;
            }
        }


        private void btnOK_Click(object sender, EventArgs e)
        {

            //Проверка введенных данных

            foreach (TextBox txtBx in this.Controls.OfType<TextBox>())
            {
                if (txtBx.Text == string.Empty)
                {
                    MessageBox.Show("Для внесения данных в базу необходимо заполнить все поля");
                    return;
                }
            }
            
            OperationResult opres=DataValidation.ValidatePassportData(txtBxPassportSerial.Text,txtBxPassportNumber.Text);
            if (!opres.Good)
            {
                MessageBox.Show(opres.Message);
                return;
            }
       

            object[] ParamsForSQL=new object[] {txtBxFamilyName.Text,
                txtBxFirstName.Text,
                txtBxPatronymic.Text,
                txtBxPassportSerial.Text,
                txtBxPassportNumber.Text,
                chkBxInterfaces.Checked,
                chkBxRussian.Checked,
                chkBxArchitecture.Checked
            };

            opres = SQLHelper.SQLCommandNoResult(SQLConString, SQLQueryStrings.InsertPeopleInfo, SQLQueryStrings.InsertPeopleParams, ParamsForSQL);
            MessageBox.Show(opres.Message);
            ClearFields();

        }
    }
}
