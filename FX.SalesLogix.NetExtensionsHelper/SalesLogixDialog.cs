using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Sage.SalesLogix.NetExtensions;
using Sage.SalesLogix.NetExtensions.SalesLogix;
using FX.SalesLogix.NetExtensionsHelper.Utility;

namespace FX.SalesLogix.NetExtensionsHelper
{
    public class SalesLogixDialog : System.Windows.Forms.Form, IRunnable
    {
        public ISlxApplication SlxApplication = null;
        public object Callback = null;
        private string _recordid = string.Empty;

        public SalesLogixDialog()
        {
            InitializeComponent();
        }

        public string RecordID
        {
            get { return this._recordid; }
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

        public void Initialize(ISlxApplication SlxApplication, Sage.SalesLogix.NetExtensions.Licensing.ILicenseKeyManager LicenseKeyManager)
        {
            this.SlxApplication = SlxApplication;
        }

        public object Run(object[] Args)
        {
            //foreach (object o in Args)
            //{
             //   MessageBox.Show("Arg Type: " + o.GetType().ToString());
            //}

            if (Args.Length > 0) this._recordid = Args[0].ToString();

            this.ShowDialog();

            return null;
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
    }
}
