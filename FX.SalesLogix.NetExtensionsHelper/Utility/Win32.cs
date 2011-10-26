using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace FX.SalesLogix.NetExtensionsHelper.Utility
{
    public class Win32
    {
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }
            public Size Size { get { return new Size(Width, Height); } }
            public Point Location { get { return new Point(Left, Top); } }

            public Rectangle ToRectangle()
            { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

            public static RECT FromRectangle(Rectangle rectangle)
            { return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom); }

            public static implicit operator Rectangle(RECT rect)
            { return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom); }

            public static implicit operator RECT(Rectangle rect)
            { return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom); }

            public override int GetHashCode()
            {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                            ^ ((Width << 0x1a) | (Width >> 6))
                            ^ ((Height << 7) | (Height >> 0x19));
            }

			public string GetClassName(IntPtr handle)
			{
				var className = new System.Text.StringBuilder(100);

				if (Win32.GetClassName(handle, className, className.Capacity) != 0)
					return className.ToString();
				else
					return string.Empty;
			}
        }
    }
}
