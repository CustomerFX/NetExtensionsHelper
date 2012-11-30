#region License Information
/* 

    SalesLogix Mobile Developer Tools
    Copyright (C) 2012  Customer FX Corporation - http://customerfx.com/

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
	
    ADDITIONAL TERMS: 
    Attribution required.

    Contact Information:
    
    Ryan Farley 
    Customer FX Corporation
    http://customerfx.com/
    2324 University Avenue West, Suite 115
    Saint Paul, Minnesota 55114
    Tel: 651.646.7777  Fax: 651.846.5185
    
    This copyright must remain intact
    
*/
#endregion

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using Sage.SalesLogix.NetExtensions;
using Sage.SalesLogix.NetExtensions.Licensing;
using Sage.SalesLogix.NetExtensions.SalesLogix;
using FX.SalesLogix.NetExtensionsHelper.Utility;

namespace FX.SalesLogix.NetExtensionsHelper
{
	[ToolboxBitmap(typeof(Form))]
    public class SalesLogixDialog : Form, IRunnable
    {
		public ExtensionProperties ExtensionProperties = null;
		public ISlxApplication SlxApplication = null;
		public ILicenseKeyManager LicenseKeyManager = null;

        public SalesLogixDialog()
        {
            InitializeComponent();
        }

		public string CurrentID
		{
			get
			{
				return ExtensionProperties.RecordID; 
			}
		}

        #region Component Code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SalesLogixDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesLogixDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SalesLogixDialog1";
            this.ResumeLayout(false);

        }

        #endregion

        public void Initialize(ISlxApplication SlxApplication, ILicenseKeyManager LicenseKeyManager)
        {
            this.SlxApplication = SlxApplication;
			this.LicenseKeyManager = LicenseKeyManager;

			this.ExtensionProperties = new ExtensionProperties();
        }

		// To set record context: Pass 1 parameter
		// Param 1: The ID of the current SLX record
		// Param 2: An optional reference to a callback function, set in SLX passing "FunctionName" as a string where FunctionName accepts two params
		//          'Sample callback function in VBScript
		//          Function CallbackFunction(EventName, EventData)
		//          End Function
        public object Run(object[] Args)
        {
			ExtensionProperties.SetProperties(Args);

			if (ExtensionProperties.RecordID == string.Empty)
				throw new ApplicationException("Invalid properties passed");

			this.ShowDialog();
			return null;
        }

        protected void RaiseSalesLogixCallbackEvent(string EventName, object EventData)
        {
            try
            {
                if (ExtensionProperties.Callback != null)
                {
					Type t = ExtensionProperties.Callback.GetType();
					t.InvokeMember("", BindingFlags.InvokeMethod, null, ExtensionProperties.Callback, new object[] { EventName, EventData });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred calling the callback function from the .NET SalesLogix Dialog. " + ex.Message, ex);
            }
        }
    }
}
