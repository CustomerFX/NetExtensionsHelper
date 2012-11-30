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

				if (Properties.Length >= 4 && Properties[3] != null)
					this.Callback = Properties[3];
			}
			else
			{
				ExtensionState = ExtensionState.SetContext;
				RecordID = Properties[0].ToString();

				if (Properties.Length >= 2 && Properties[1] != null)
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
