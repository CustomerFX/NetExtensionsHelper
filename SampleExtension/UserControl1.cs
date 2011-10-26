using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

// Note: This is to bundle the associated SalesLogix form plugin in the bundle with the .NET extension
[assembly: Sage.SalesLogix.NetExtensions.Deployment.DeployInsertPlugin(Sage.SalesLogix.NetExtensions.Deployment.PluginType.ActiveForm, "Account", "Extension Control Sample")]

namespace SampleExtension
{
    public partial class UserControl1 : FX.SalesLogix.NetExtensionsHelper.SalesLogixControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_SalesLogixRecordChanged(string CurrentID)
        {
            using (var conn = new OleDbConnection(this.SlxApplication.ConnectionString))
            {
                conn.Open();
                using (var da = new OleDbDataAdapter(string.Format("select lastname as LastName, firstname as FirstName, type as Type from contact where accountid = '{0}'", CurrentID), conn))
                {
                    var table = new DataTable();
                    da.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }
    }
}
