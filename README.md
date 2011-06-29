The .NET Extension Helper is a library that will do all the heavy lifting to embed controls in SalesLogix. All you do is have your UserControl inherit from the FX.SalesLogix.NetExtensionsHelper.SalesLogixControl and then use a script class in SalesLogix to load the control and set the record context.

Your .NET UserControl will look like this:

<code>public partial class UserControl1 : FX.SalesLogix.NetExtensionsHelper.SalesLogixControl
{
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
}</code>

In SalesLogix, the code for the form will look like this:

<code>'Including Script - System:ExtensionControl Class
option explicit

Dim ext

Sub AXFormOpen(Sender)
    Set ext = new ExtensionControl
    ext.Load "SampleExtension", "SampleExtension.UserControl1", Form.HWND, True
End Sub

Sub AXFormChange(Sender)
    ext.CurrentID = Form.CurrentID
End Sub</code>