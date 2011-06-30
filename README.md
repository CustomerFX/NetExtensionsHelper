<h1>.NET Extension Helper</h1>

The .NET Extension Helper is a library that will do all the heavy lifting to embed controls in SalesLogix. All you do is have your UserControl inherit from the FX.SalesLogix.NetExtensionsHelper.SalesLogixControl and then use a script class in SalesLogix to load the control and set the record context.

Your .NET UserControl will look like this:

<pre>public partial class UserControl1 : FX.SalesLogix.NetExtensionsHelper.SalesLogixControl
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
}</pre>

In SalesLogix, the code for the form will look like this:

<pre>'Including Script - System:ExtensionControl Class
option explicit

Dim ext

Sub AXFormOpen(Sender)
    Set ext = new ExtensionControl
    ext.Load "SampleExtension", "SampleExtension.UserControl1", Form.HWND, True
End Sub

Sub AXFormChange(Sender)
    ext.CurrentID = Form.CurrentID
End Sub</pre>

Using the provided sample, you'll end up with the following result in SalesLogix (The DataGrid on this tab is a .NET DataGrid control)

<img src="http://content.screencast.com/users/RyanFarley/folders/Jing/media/acd5f0d9-6aec-44d4-8a2d-3499d11bdae2/SalesLogix_Extension_Sample.png">

<h1>Instructions for Use</h1>

There is a sample bundle (click the Downloads button on the github repo to access all available downloads) you can install to see it in action and you can view the code on github or pull the source.

To use it you do the following:

1) Create a .NET UserControl project. Change the base class from UserControl to FX.SalesLogix.NetExtensionsHelper.SalesLogixControl (add a reference to FX.SalesLogix.NetExtensionsHelper.dll)

2) In the .NET UserControl, wire-up the event SalesLogixRecordChanged from the base class

3) Add whatever else you need on the UserControl such as a datagrid, etc.

4) Add the compiled UserControl assembly to the .NET Extension Manager

5) Install the bundle "FX .NET Extensions Helper.sxb"

6) Create a new tab in Architect (it doesn't need to be a new tab, you can put this anywhere)

7) Add an include script "System:ExtensionControl Class"

8) In the Form Open event on the SalesLogix form, add the following code to load the .NET UserControl:

<pre>Dim ext ' The extension variable needs to be declared globally on the form

Sub AXFormOpen(Sender)
    Set ext = new ExtensionControl
    ext.Load "SampleExtension", "SampleExtension.UserControl1", Form.HWND, True

    ' Load parameters: "Extension Title", "Extension ClassName", "Parent Handle", "Resize to fill parent"
End Sub</pre>

9) Then in the form's Change event, you'll need to set the ID of the current with the following code:

<pre>Sub AXFormChange(Sender)
    ext.CurrentID = Form.CurrentID
End Sub</pre>


That's it.