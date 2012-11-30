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
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using FX.SalesLogix.NetExtensionsHelper.Utility;
using Sage.SalesLogix.NetExtensions;
using Sage.SalesLogix.NetExtensions.Licensing;
using Sage.SalesLogix.NetExtensions.SalesLogix;

namespace FX.SalesLogix.NetExtensionsHelper
{
    public delegate void RecordChangeEventHandler(string CurrentID);

    [ToolboxBitmap(typeof(Form))]
    public class SalesLogixControl : UserControl, IRunnable
    {
        [Description("Event that is raised when the record is changed in SalesLogix, if passed"), Category("SalesLogix")]
        public event RecordChangeEventHandler SalesLogixRecordChanged;

		public ExtensionProperties ExtensionProperties = null;
        public ISlxApplication SlxApplication = null;
		public ILicenseKeyManager LicenseKeyManager = null;

        public SalesLogixControl()
        {
            InitializeComponent();
        }

        #region Component Code

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
            // SalesLogixControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SalesLogixControl";
            this.Size = new System.Drawing.Size(150, 150);
            this.ResumeLayout(false);

        }

        #endregion

        public void Initialize(ISlxApplication SlxApplication, ILicenseKeyManager LicenseKeyManager)
        {
            this.SlxApplication = SlxApplication;
			this.LicenseKeyManager = LicenseKeyManager;

			this.ExtensionProperties = new ExtensionProperties();
        }

		// To initialize: Pass 4 parameters (must be in the following order)
		// Param 1: The handle/hwnd of the container to embed the control
		// Param 2: A boolean indicating whether or not to resize the control to fill the container (default: false)
		// Param 3: A boolean indicating whether to force the control to embed to the SLX form's parent container tab instead of the form itself
		// Param 4: An optional reference to a callback function, set in SLX passing "FunctionName" as a string where FunctionName accepts two params
		//          'Sample callback function in VBScript
		//          Function CallbackFunction(EventName, EventData)
		//          End Function
		//--------------------------------------------------------------
		// To set record context: Pass 1 parameter
		// Param 1: The ID of the current SLX record
        public object Run(object[] Args)
        {
			ExtensionProperties.SetProperties(Args);

			if (ExtensionProperties.ParentHandle == IntPtr.Zero)
				throw new ApplicationException("Invalid properties passed");

			if (ExtensionProperties.ExtensionState == ExtensionState.Initialize)
			{
				this.Show();

				Win32.SetParent(this.Handle, ExtensionProperties.ParentHandle);

				if (ExtensionProperties.FillParent)
					Fill(ExtensionProperties.ParentHandle);
			}
			else
			{
				if (SalesLogixRecordChanged != null) 
					SalesLogixRecordChanged(ExtensionProperties.RecordID);
			}

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
                throw new Exception("An error occurred calling the callback function from the .NET SalesLogix Control. " + ex.Message, ex);
            }
        }

        private void Fill(IntPtr ParentHandle)
        {
            Win32.RECT rect;
			if (Win32.GetWindowRect(ParentHandle, out rect))
            {
                this.Size = rect.Size;
            }
        }
    }
}
