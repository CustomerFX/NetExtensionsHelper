using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Sage.SalesLogix.NetExtensions;
using Sage.SalesLogix.NetExtensions.SalesLogix;
using FX.SalesLogix.NetExtensionsHelper.Utility;

namespace FX.SalesLogix.NetExtensionsHelper
{
    public delegate void RecordChangeEventHandler(string RecordID);

    [ToolboxBitmap(typeof(PageSetupDialog))]
    public class SalesLogixControl : UserControl, IRunnable
    {
        [Description("Event that is raised when the record is changed in SalesLogix, if passed"), Category("SalesLogix")]
        public event RecordChangeEventHandler SalesLogixRecordChanged;

        public ISlxApplication SlxApplication = null;
        public object Callback = null;

        private IntPtr _parent = IntPtr.Zero;
        private bool _fillparent = false;

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

        public void Initialize(Sage.SalesLogix.NetExtensions.SalesLogix.ISlxApplication SlxApplication, Sage.SalesLogix.NetExtensions.Licensing.ILicenseKeyManager LicenseKeyManager)
        {
            this.SlxApplication = SlxApplication;
        }

		// To initialize: Pass 4 parameters (must be in the following order)
		// Param 1: The handle/hwnd of the container to embed the control
		// Param 2: A boolean indicating whether or not to resize the control to fill the container (default: false)
		// Param 3: A boolean indicating whether to force the control to embed to the SLX form's parent container tab instead of the form itself
		// Param 4: A reference to a callback function, set in SLX using GetRef("FunctionName") where FunctionName accepts two params
		//          'Sample callback function in VBScript
		//          Function CallbackFunction(EventName, EventData)
		//          End Function
		//--------------------------------------------------------------
		// To set record context: Pass 1 parameter
		// Param 1: The ID of the current SLX record
        public object Run(object[] Args)
        {
            if (Args[0] is Int32)
            {
                _parent = new IntPtr((int)Args[0]);

				// Check for force resize mode
				if (Args.Length >= 3 && (bool)Args[2] == true)
					_parent = Win32.GetParent(_parent);

                this.Show();

				// embed the extension
                Win32.SetParent(this.Handle, _parent);
				// resize to fill the parent container
                if (Args.Length >= 2) _fillparent = (bool)Args[1];
                if (_fillparent) ResizeControl(_parent);

				// set callback if included
                if (Args.Length == 4 && Args[3] != null)
                    this.Callback = Args[3];
            }
            else
            {
                if (_parent != IntPtr.Zero) if (_fillparent) ResizeControl(_parent);

                string recordid = Args[0].ToString();
                if (SalesLogixRecordChanged != null) SalesLogixRecordChanged(recordid);
            }

            return null;
        }

		public string GetClassName(IntPtr handle)
		{
			System.Text.StringBuilder className = new System.Text.StringBuilder(100);
			int nRet = Win32.GetClassName(handle, className, className.Capacity);
			if (nRet != 0)
				return className.ToString();
			else
				return string.Empty;
		}

        protected void RaiseSalesLogixCallbackEvent(string EventName, object EventData)
        {
            try
            {
                if (Callback != null)
                {
                    Type t = Callback.GetType();
                    t.InvokeMember("", BindingFlags.InvokeMethod, null, Callback, new object[] { EventName, EventData });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred calling the callback function from the .NET SalesLogix Control. " + ex.Message, ex);
            }
        }

        private void ResizeControl(IntPtr handle)
        {
            Win32.RECT rect;
            if (Win32.GetWindowRect(handle, out rect))
            {
                this.Size = rect.Size;
            }
        }
    }
}
