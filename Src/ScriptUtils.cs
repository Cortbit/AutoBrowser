using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using IMKCode;

namespace AutoBrowser
{
    public static class ScriptUtils
    {
        public static int MsgBox(object msg)
        {
            return (int)System.Windows.Forms.MessageBox.Show(""+ msg);
        }
        public static int MsgBox(object msg, object title)
        {
            return (int)System.Windows.Forms.MessageBox.Show(""+ msg, ""+ title);
        }
        public static int MsgBox(object msg, object title, int timeout)
        {
            return IMKCode.Api.MessageBoxTimeoutA(IntPtr.Zero, ""+msg, ""+title, 0, 0, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="type">
        /// [Buttons]
        /// OK = 0,
        /// OKCancel = 1,
        /// AbortRetryIgnore = 2,
        /// YesNoCancel = 3,
        /// YesNo = 4,
        /// RetryCancel = 5
        /// [Icons]
        /// 16	警告标志(一般用于错误提示)	0x10
        /// 32	问号图标	0x20
        /// 48	感叹号图标	0x30
        /// 64	由一个"i"和圆圈组成的图标(消息通知)    0x40
        /// [DefaultButtion]
        /// 256	第二个按钮是默认按钮	0x100
        /// 512	第三个按钮是默认按钮	0x200
        /// [Mode]
        /// 4096	系统模式(对话框带有图标)	0x1000
        /// 8192	任务模式	0x2000
        /// [Features]
        /// 262144	消息框将具有顶层窗口属性	0x40000
        /// 524288	标题文字及文本内容将右对齐	0x80000
        /// </param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static int MsgBox(object msg, object title, int type, int timeout)
        {
            return IMKCode.Api.MessageBoxTimeoutA(IntPtr.Zero, "" + msg, "" + title, type, 0, timeout);
        }


        public static string InputBox(object title)
        {
            return InputBox(title, string.Empty, string.Empty);
        }

        public static string InputBox(object title, string defaultValue, object description)
        {
            string content;
            if (FrmPrompt.Prompt(""+ title, defaultValue, ""+description, out content, 0))
            {
                return content;
            }
            return string.Empty;
        }

        public static string InputBox(object title, string defaultValue, object description, int timeout)
        {
            string content;
            if (FrmPrompt.Prompt("" + title, defaultValue, "" + description, out content, timeout))
            {
                return content;
            }
            return defaultValue;
        }


        public static System.Drawing.Point[] FindTarget(string name)
        {
            refreshWorkImage();
            System.Drawing.Image _itarget;
            if (TMG.TryGetValue(name, out _itarget))
            {
                return FindTarget(ImageWorkArea, _itarget);
            }
            return new System.Drawing.Point[] { };
        }

        public static System.Drawing.Point[] FindTarget(System.Drawing.Image imgScreen, System.Drawing.Image imgTarget)
        {
            List<System.Drawing.Point> outPoints = new List<System.Drawing.Point>();

            using (Mat refMat = BitmapConverter.ToMat(new System.Drawing.Bitmap(imgScreen)))//大图
            using (Mat tplMat = BitmapConverter.ToMat(new System.Drawing.Bitmap(imgTarget)))//小图
            using (Mat res = new Mat(refMat.Rows - tplMat.Rows + 1, refMat.Cols - tplMat.Cols + 1, MatType.CV_32FC1))
            {
                //Convert input images to gray
                Mat gref = refMat.CvtColor(ColorConversionCodes.BGR2GRAY);
                Mat gtpl = tplMat.CvtColor(ColorConversionCodes.BGR2GRAY);

                Cv2.MatchTemplate(gref, gtpl, res, TemplateMatchModes.CCoeffNormed);
                Cv2.Threshold(res, res, 0.8, 1.0, ThresholdTypes.Tozero);
                while (true)
                {
                    double minval, maxval, threshold = 0.8;
                    Point minloc, maxloc;
                    Cv2.MinMaxLoc(res, out minval, out maxval, out minloc, out maxloc);

                    if (maxval >= threshold)
                    {
                        //Setup the rectangle to draw
                        Rect r = new Rect(new Point(maxloc.X, maxloc.Y), new Size(tplMat.Width, tplMat.Height));
                        //Console.WriteLine(maxloc.X +"," + maxloc.Y);
                        //Draw a rectangle of the matching area
                        Cv2.Rectangle(refMat, r, Scalar.LimeGreen, 2);

                        outPoints.Add(new System.Drawing.Point(r.X + (r.Width/2), r.Y + (r.Height / 2)));
                        //outPoints.Add(new System.Drawing.Point(r.X + (r.Width / 2), r.Y + (r.Height / 2)));

                        //Fill in the res Mat so you don't find the same area again in the MinMaxLoc
                        Rect outRect;
                        Cv2.FloodFill(res, maxloc, new Scalar(0), out outRect, new Scalar(0.1), new Scalar(1.0), FloodFillFlags.Link4);
                    }
                    else
                    {
                        break;
                    }
                }


                //Cv2.ImShow("Matches", refMat);

                //Cv2.WaitKey();
            }

            return outPoints.ToArray();
        }

        public static System.Drawing.Color GetPosColor(int x, int y)
        {
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(3, 3))
            {
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(x-1, y-1, 0, 0, bmp.Size);
                    return bmp.GetPixel(1, 1);
                }
            }
        }

        public static System.Drawing.Color GetColor(int x, int y)
        {
            if (RM != null)
            {
                RM.IfPointToScreen(ref x, ref y);
            }
            return GetPosColor(x, y);
        }

        #region Mouse & Keyboard Action

        internal static IMKCode.APIX.RecordManager RM;
        internal static Dictionary<string, System.Drawing.Image> TMG = new Dictionary<string, System.Drawing.Image>(StringComparer.CurrentCultureIgnoreCase);
        internal static System.Drawing.Rectangle WorkRect;
        internal static System.Drawing.Image ImageWorkArea;
        internal static System.Drawing.Graphics GWorkArea;
        internal static DateTime? lastWorkImageTime;

        private static void refreshWorkImage()
        {
            if (lastWorkImageTime == null || DateTime.Now.Subtract(lastWorkImageTime.Value).TotalSeconds > 2)
            {
                lastWorkImageTime = DateTime.Now;
                if (ImageWorkArea == null || ImageWorkArea.Size != WorkRect.Size)
                {
                    if (GWorkArea != null) { GWorkArea.Dispose(); }
                    if (ImageWorkArea != null) { ImageWorkArea.Dispose(); }
                    ImageWorkArea = new System.Drawing.Bitmap(WorkRect.Width, WorkRect.Height);
                    GWorkArea = System.Drawing.Graphics.FromImage(ImageWorkArea);
                }
                GWorkArea.CopyFromScreen(ScriptUtils.WorkRect.X, ScriptUtils.WorkRect.Y, 0, 0, ScriptUtils.WorkRect.Size);
            }
        }

        public static void Sleep(int times)
        {
            if (times > 0)
            {
                System.Threading.Thread.Sleep(times);
            }
        }

        public static void MouseMove(int x, int y)
        {
            if (RM != null)
            {
                RM.MouseMove(x.ToString(), y.ToString());
            }
        }

        public static void MouseDown(string button)
        {
            if (RM != null)
            {
                RM.MouseDown(button);
            }
        }
        public static void MouseUp(string button)
        {
            if (RM != null)
            {
                RM.MouseUp(button);
            }
        }

        public static void KeyUp(string key)
        {
            if (RM != null)
            {
                RM.KeyUp(key);
            }
            else
            {
                System.Windows.Forms.Keys ek = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), key, true);
                IMKCode.Api.keybd_event((byte)ek, 0, 2, 0);
            }
        }
        public static void KeyDown(string key)
        {
            if (RM != null)
            {
                RM.KeyDown(key);
            }
            else
            {
                System.Windows.Forms.Keys ek = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), key, true);
                Api.keybd_event((byte)ek, 0, 1, 0);
            }
        }

        public static string GetPointMode()
        {
            return RM.PointMode;
        }
        /// <summary>
        /// IMK: 设置 指标模式(local/global);
        /// </summary>
        /// <param name="mode"></param>
        public static void SetPointMode(string mode)
        {
            if (string.IsNullOrWhiteSpace(mode)) { return; }
            switch (mode.ToLower())
            {
                case "local":
                {
                        RM.IsAbsolutePoint = false;
                        break;
                }
                case "global":
                {
                        RM.IsAbsolutePoint = true;
                        break;
                }
            }
        }

        #endregion

        #region 扩展方法
        public static int GetValue(this System.Drawing.Color c)
        {
            return (c.ToArgb() & 0x00FFFFFF);
        }


        /// <summary>
        /// IMK: Color like Color
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool LikeColor(this System.Drawing.Color c1, System.Drawing.Color c2)
        {
            return LikeColor(c1, c2, 5);
        }

        /// <summary>
        /// IMK: Color like Color, threshold
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool LikeColor(this System.Drawing.Color c1, System.Drawing.Color c2, int threshold)
        {
            int _absR = System.Math.Abs(c1.R - c2.R);
            int _absG = System.Math.Abs(c1.G - c2.G);
            int _absB = System.Math.Abs(c1.B - c2.B);
            return _absR < threshold
                && _absG < threshold
                && _absB < threshold;

        }

        /// <summary>
        /// IMK: ColorValue like ColorValue
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this int c1, int c2)
        {
            return LikeColorValue(c1, c2, 5);
        }

        /// <summary>
        /// IMK: ColorValue like ColorValue, threshold
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this int c1, int c2, int threshold)
        {
            int _absR = System.Math.Abs(((c1 >> 16) & 0xFF) - ((c2 >> 16) & 0xFF));
            int _absG = System.Math.Abs(((c1 >> 8) & 0xFF) - ((c2 >> 8) & 0xFF));
            int _absB = System.Math.Abs((c1 & 0xFF) - (c2 & 0xFF));
            return _absR < threshold
                && _absG < threshold
                && _absB < threshold;

        }

        /// <summary>
        /// IMK: Color like ColorValue
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this System.Drawing.Color c1, int c2)
        {
            return LikeColorValue(c1, c2, 5);
        }

        /// <summary>
        /// IMK: Color like ColorValue, threshold
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this System.Drawing.Color c1, int c2, int threshold)
        {
            int _absR = System.Math.Abs(c1.R - ((c2 >> 16) & 0xFF));
            int _absG = System.Math.Abs(c1.G - ((c2 >> 8) & 0xFF));
            int _absB = System.Math.Abs(c1.B - (c2 & 0xFF));
            return _absR < threshold
                && _absG < threshold
                && _absB < threshold;

        }

        /// <summary>
        /// IMK: ColorValue like Color
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this int c1, System.Drawing.Color c2)
        {
            return LikeColorValue(c1, c2, 5);
        }

        /// <summary>
        /// IMK: ColorValue like Color, threshold
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool LikeColorValue(this int c1, System.Drawing.Color c2, int threshold)
        {
            int _absR = System.Math.Abs(((c1 >> 16) & 0xFF) - c2.R);
            int _absG = System.Math.Abs(((c1 >> 8) & 0xFF) - c2.G);
            int _absB = System.Math.Abs((c1 & 0xFF) - c2.B);
            return _absR < threshold
                && _absG < threshold
                && _absB < threshold;

        }

        /// <summary>
        /// IMK: Color Diff: Color, Color
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorDiff(this System.Drawing.Color c1, System.Drawing.Color c2)
        {
            int _absR = System.Math.Abs(c1.R - c2.R);
            int _absG = System.Math.Abs(c1.G - c2.G);
            int _absB = System.Math.Abs(c1.B - c2.B);
            return System.Drawing.Color.FromArgb(_absR, _absG, _absB);
        }
        /// <summary>
        /// IMK: 获取颜色与数值的差异度
        /// IMK: Color Diff: Color, ColorValue
        /// </summary>
        /// <param name="c"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorDiff(this System.Drawing.Color c, int v)
        {
            int _absR = System.Math.Abs(c.R - ((v >> 16) & 0xFF));
            int _absG = System.Math.Abs(c.G - ((v >> 8) & 0xFF));
            int _absB = System.Math.Abs(c.B - (v & 0xFF));
            return System.Drawing.Color.FromArgb(_absR, _absG, _absB);
        }
        /// <summary>
        /// IMK: Color Diff: ColorValue, ColorValue
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorDiff(this int v1, int v2)
        {
            int _absR = System.Math.Abs(((v1 >> 16) & 0xFF) - ((v2 >> 16) & 0xFF));
            int _absG = System.Math.Abs(((v1 >> 8) & 0xFF) - ((v2 >> 8) & 0xFF));
            int _absB = System.Math.Abs((v1 & 0xFF) - (v2 & 0xFF));
            return System.Drawing.Color.FromArgb(_absR, _absG, _absB);
        }

        #endregion
    }
}
