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
    public partial class AdminForm : Form
    {

        DataTable DT = new DataTable();
        SqlDataAdapter SQLDA = new SqlDataAdapter();
        string SQLConString { get; set; }
        bool first = true;
        public AdminForm(string sqlConString)
        {
            InitializeComponent();
            BindingSource bs = new BindingSource();
            string[] columnNames;
            SQLDA = SQLHelper.SQLDAForAdmin(sqlConString,out columnNames);
            SQLDA.Fill(DT);

            if (columnNames.Length != DT.Columns.Count)
            {
                MessageBox.Show("Количество столбцов в таблице и их наименований не совпадает. Обратитесь в службу поддержки");
            }

           

            DT.Columns["ID"].ReadOnly = true;
            bs.DataSource = DT;
            bindNavPeopleInfo.BindingSource=bs;
            dtGridViewPeopleInfo.DataSource = bs;
            for (int i = 0; i < columnNames.Length && i < dtGridViewPeopleInfo.Columns.Count; i++)
            {
                dtGridViewPeopleInfo.Columns[i].HeaderText=columnNames[i];
            }
           

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int recCount = SQLDA.Update(DT);
                MessageBox.Show("Успешно отредактированное количество записей: " + recCount.ToString() + "");
                btnOK.Enabled = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }


        private void dtGridViewPeopleInfo_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!first)
            {
                btnOK.Enabled = true;
            }
        }

        private void dtGridViewPeopleInfo_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (!first)
            {
                btnOK.Enabled = true;
            }
        }

        private void AdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnOK.Enabled)
            {
                if (DialogResult.No == MessageBox.Show("У Вас есть несохраненные изменения. Вы действительно хотите выйти?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    e.Cancel = true;
                }
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            first = false;
        }

    }
}
