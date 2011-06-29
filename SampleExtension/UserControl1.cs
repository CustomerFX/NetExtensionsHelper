using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

namespace SlxExtensionControl1
{
    public partial class UserControl1 : FX.SalesLogix.NetExtensionsHelper.SalesLogixControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_SalesLogixRecordChanged(string RecordID)
        {
            using (OleDbConnection conn = new OleDbConnection(this.SlxApplication.ConnectionString))
            {
                conn.Open();
                using (OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select lastname as LastName, firstname as FirstName, type as Type from contact where accountid = '{0}'", RecordID), conn))
                {
                    DataTable table = new DataTable();
                    da.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }
    }
}
