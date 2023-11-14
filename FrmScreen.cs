using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class FrmScreen : Form
    {
        private Graphics MainPainter;   //主画面
        private Pen pen;                //画笔
        private bool isDowned;          //判断鼠标是否按下 
        private bool RectReady;         //矩形是否绘制完成 
        private Image baseImage;        //基本图形(原来的画面) 
        private Rectangle Rect;         //就是要保存的矩形 
        private Point downPoint;        //鼠标按下的点 
        int tmpx;
        int tmpy;
        int imgW, imgH;

        public FrmScreen()
        {
            InitializeComponent();
        }

        private void FrmScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            MainPainter = this.CreateGraphics();
            pen = new Pen(Brushes.Magenta);
            isDowned = false;
            baseImage = this.BackgroundImage;
            imgW = baseImage.Width;
            imgH = baseImage.Height;
            Rect = new Rectangle();
            RectReady = false;
            this.TransparencyKey = SystemColors.AppWorkspace;
            this.AllowTransparency = true;
        }

        private void FrmScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Rect.Contains(e.X, e.Y))
            {
                Image memory = new Bitmap(Rect.Width, Rect.Height);
                Graphics g = Graphics.FromImage(memory);

                g.DrawImage(baseImage, 0 - Rect.X, 0 - Rect.Y);
                //g.CopyFromScreen(Rect.X + 1, Rect.Y + 1, 0, 0, Rect.Size);
                //Clipboard.SetImage(memory);
                string filePath = null;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "Image files (JPeg, Gif, Bmp, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif; *.tiff; *.png|" +
                "JPeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg |GIF files (*.gif)|*.gif |BMP files (*.b" +
                "mp)|*.bmp|Tiff files (*.tif;*.tiff)|*.tif;*.tiff|Png files (*.png)| *.png |All f" +
                "iles (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog1.FileName.ToString();
                    
                    memory.Save(filePath, Utils.GetImageFormat(filePath));
                    this.DialogResult = DialogResult.OK;
                }
                this.Close();
            }
        }

        private void FrmScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDowned = true;

                if (RectReady == false)
                {
                    Rect.X = e.X;
                    Rect.Y = e.Y;
                    downPoint = new Point(e.X, e.Y);
                }
                if (RectReady == true)
                {
                    tmpx = e.X;
                    tmpy = e.Y;
                }
            }
        }

        private void FrmScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDowned = false;
                RectReady = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
                return;
            }
        }

        private void FrmScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (RectReady == false)
            {
                if (isDowned == true)
                {
                    Image New = DrawScreen((Image)baseImage.Clone(), e.X, e.Y);
                    MainPainter.DrawImage(New, 0, 0);
                    New.Dispose();
                }
            }
            if (RectReady == true)
            {
                if (Rect.Contains(e.X, e.Y))
                {
                    this.Cursor = Cursors.Hand;
                    if (isDowned == true)
                    {
                        //和上一次的位置比较获取偏移量 
                        Rect.X = Rect.X + e.X - tmpx;
                        Rect.Y = Rect.Y + e.Y - tmpy;
                        //记录现在的位置 
                        tmpx = e.X;
                        tmpy = e.Y;
                        MoveRect((Image)baseImage.Clone(), Rect);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            this.Text = Rect.ToString() +"@" + e.X +","+ e.Y +"/"+ imgW +","+ imgH +" - (左键框选，右键取消，双击确认)";
        }

        //画屏幕
        private Image DrawScreen(Image back, int Mouse_x, int Mouse_y)
        {
            Graphics Painter = Graphics.FromImage(back);
            DrawRect(Painter, Mouse_x, Mouse_y);
            return back;
        }
        //画矩形
        private void DrawRect(Graphics Painter, int Mouse_x, int Mouse_y)
        {
            int width = 0;
            int heigth = 0;
            try
            {
                if (Mouse_y < Rect.Y)
                {
                    Rect.Y = Mouse_y;
                    heigth = downPoint.Y - Mouse_y;
                }
                else
                {
                    heigth = Mouse_y - downPoint.Y;
                }
                if (Mouse_x < Rect.X)
                {
                    Rect.X = Mouse_x;
                    width = downPoint.X - Mouse_x;
                }
                else
                {
                    width = Mouse_x - downPoint.X;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("cuo");
            }
            finally
            {
                Rect.Size = new Size(width, heigth);
                Painter.DrawRectangle(pen, Rect);
            }
        }

        //移动矩形
        private void MoveRect(Image image, Rectangle Rect)
        {
            using (Graphics Painter = Graphics.FromImage(image))
            {
                Painter.DrawRectangle(pen, Rect.X, Rect.Y, Rect.Width, Rect.Height);
                MainPainter.DrawImage(image, 0, 0);
                image.Dispose();
            }
        }

    }
}
