using System;
using System.Collections.Generic;
using System.Text;

namespace IMKCode.API
{
    public class IconHelper
    {
        #region Win32 API
        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIcon")]
        private static extern IntPtr ExtractAssociatedIconA(
            IntPtr hInst,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] 
            string lpIconPath,
            ref int lpiIcon);

        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "ExtractIcon")]
        private static extern IntPtr ExtractIconA(
            IntPtr hInst,
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] 
            string lpszExeFileName,
            int nIconIndex);

        private static IntPtr hInst = IntPtr.Zero;

        #endregion

        public static System.Drawing.Icon ExtractIcon(int index)
        {
            string fileName = typeof(IconHelper).Assembly.Location;
            return ExtractIcon(fileName, index);
        }

        public static System.Drawing.Icon ExtractIcon(string fileName, int index)
        {
            if (System.IO.File.Exists(fileName) || System.IO.Directory.Exists(fileName))
            {
                System.IntPtr hIcon;

                // 文件所含图标的总数
                hIcon = ExtractIconA(hInst, fileName, -1);

                // 没取到的时候
                if (hIcon.Equals(IntPtr.Zero))
                {
                    // 取得跟文件相关的图标
                    return ExtractAssociatedIcon(fileName);
                }
                else
                {
                    // 图标的总数
                    int numOfIcons = hIcon.ToInt32();

                    if (0 <= index && index < numOfIcons)
                    {
                        hIcon = ExtractIconA(hInst, fileName, index);

                        if (!hIcon.Equals(IntPtr.Zero))
                        {
                            return System.Drawing.Icon.FromHandle(hIcon);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public static System.Drawing.Icon ExtractAssociatedIcon(string fileName)
        {
            if (System.IO.File.Exists(fileName) || System.IO.Directory.Exists(fileName))
            {
                int i = 0;

                IntPtr hIcon = ExtractAssociatedIconA(hInst, fileName, ref i);

                if (!hIcon.Equals(IntPtr.Zero))
                {
                    return System.Drawing.Icon.FromHandle(hIcon);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
