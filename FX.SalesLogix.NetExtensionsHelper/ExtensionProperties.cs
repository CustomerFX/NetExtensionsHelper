using System;
using FX.SalesLogix.NetExtensionsHelper.Utility;

namespace FX.SalesLogix.NetExtensionsHelper
{
	public enum ExtensionState
	{
		Initialize,
		SetContext
	}

	public class ExtensionProperties
	{
		public ExtensionProperties()
		{
			ExtensionState = ExtensionState.Initialize;
			ParentHandle = IntPtr.Zero;
			FillParent = false;
			ForceResizeMode = false;
			Callback = null;
		}

		public ExtensionProperties(object[] Properties) : this()
		{
			SetProperties(Properties);
		}

		public void SetProperties(object[] Properties)
		{
			if (Properties[0] is Int32)
			{
				ExtensionState = ExtensionState.Initialize;

				ParentHandle = new IntPtr((int)Properties[0]);

				if (Properties.Length >= 3 && (bool)Properties[2] == true && ParentHandle != IntPtr.Zero)
				{
					ForceResizeMode = true;
					ParentHandle = Win32.GetParent(ParentHandle);
				}

				if (Properties.Length >= 2) FillParent = (bool)Properties[1];

				if (Properties.Length == 4 && Properties[3] != null)
					this.Callback = Properties[3];
			}
			else
			{
				ExtensionState = ExtensionState.SetContext;
				RecordID = Properties[0].ToString();

				if (Properties.Length == 2 && Properties[1] != null)
					this.Callback = Properties[1];
			}
		}

		public ExtensionState ExtensionState { get; set; }
		public object Callback { get; set; }
		public IntPtr ParentHandle { get; set; }
		public bool FillParent { get; set; }
		public bool ForceResizeMode { get; set; }
		public string RecordID { get; set; }
	}
}
