using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SampleExtension
{
	public partial class Form1 : FX.SalesLogix.NetExtensionsHelper.SalesLogixDialog
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			using (var conn = new OleDbConnection(this.SlxApplication.ConnectionString))
			{
				conn.Open();
				using (var da = new OleDbDataAdapter(string.Format("select lastname as LastName, firstname as FirstName, type as Type from contact where accountid = '{0}'", this.CurrentID), conn))
				{
					var table = new DataTable();
					da.Fill(table);

					dataGridView1.DataSource = table;
				}
			}
		}
	}
}
