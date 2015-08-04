using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsRaizTest.Classes;

namespace WinFormsRaizTest
{
    public partial class LoginForm : Form
    {
       private Config Cfg { get; set; }
       public bool Success { get; set; }
       public string SqlConString { get; set; }
       public string UserRole { get; set; }

        

       public LoginForm(Config cfg)
        {
            InitializeComponent();
            Cfg = cfg;
            Success = false;           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder SqlConBuilder = new SqlConnectionStringBuilder();
            SqlConBuilder["Data Source"] = Cfg.DBServer;
            SqlConBuilder["Initial Catalog"] = Cfg.DB;
            SqlConBuilder["User Id"] = txtBxLogin.Text;
            SqlConBuilder["Password"] = txtBxPassword.Text;
            

            using (SqlConnection sqlcon=new SqlConnection(SqlConBuilder.ConnectionString))
            {
                try
                {
                    sqlcon.Open();
                    
                    
                    using (SqlCommand sqlcomm = new SqlCommand())
                    {
                        sqlcomm.CommandText = SQLQueryStrings.GetUserRole;
                        sqlcomm.Parameters.AddRange(SQLHelper.GetSQLParams(SQLQueryStrings.GetUserRoleParamName, new object[] {txtBxLogin.Text}));
                        sqlcomm.Connection = sqlcon;
                        SqlDataReader reader =sqlcomm.ExecuteReader();
                        while (reader.Read())
                        {
                            UserRole = reader.GetString(0);
                        }
                        
                        if (UserRole == null)
                        { 
                            throw new Exception("Пользователю с логином " + txtBxLogin.Text + " не присвоена роль. Обратитесь в службу поддержки"); 
                        }
                    }
                    SqlConString = SqlConBuilder.ConnectionString;
                    Success = true;
                }
                catch (Exception exc)
                {
                    Success = false;
                    MessageBox.Show(exc.Message); 
                }
                finally
                {
                    sqlcon.Close();
                }
                if (Success) Close();
            }
        }
    }
}
