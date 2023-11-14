using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBrowser
{
    public class Utils
    {

        /// <summary>
        /// IMK: 打开资源管理器到...
        /// </summary>
        /// <param name="filePath"></param>
        public static void ExplorerTo(string filePath)
        {
            System.Diagnostics.Process.Start(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/explorer.exe", "/e,/select,\"" + filePath + "\"");
        }

        public static System.Drawing.Imaging.ImageFormat GetImageFormat(string fileName)
        {
            string extension = System.IO.Path.GetExtension(fileName);
            
            switch (extension.ToLower())
            {
                case @".bmp":
                    return System.Drawing.Imaging.ImageFormat.Bmp;

                case @".gif":
                    return System.Drawing.Imaging.ImageFormat.Gif;

                case @".ico":
                    return System.Drawing.Imaging.ImageFormat.Icon;

                default:
                case @".jpg":
                case @".jpeg":
                    return System.Drawing.Imaging.ImageFormat.Jpeg;

                case @".png":
                    return System.Drawing.Imaging.ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return System.Drawing.Imaging.ImageFormat.Tiff;

                case @".wmf":
                    return System.Drawing.Imaging.ImageFormat.Wmf;
            }
        }
    }
}
