using ImageToText1;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ImageToText
{
    public partial class CutScreen : Form
    {
        private int selectX;
        private int selectY;
        private int selectedHeight;
        private int selectedWidth;
        public Pen selectPen;
        private bool start = false;

        private void SaveToClipBoard()
        {
            if (selectedWidth > 0 && selectedHeight > 0)
            {

                Rectangle rect = new Rectangle(selectX, selectY, selectedWidth, selectedHeight);
                Bitmap OriginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
                Bitmap _img = new Bitmap(selectedWidth, selectedHeight);
                Graphics g = Graphics.FromImage(_img);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);

                try
                {
                    if (pictureBox1.Image != null)
                    {
                        Image img = _img;
                        Bitmap bmp = new Bitmap(img.Width, img.Height);
                        Graphics gra = Graphics.FromImage(bmp);
                        gra.DrawImageUnscaled(img, new Point(0, 0));
                        bmp.Save(Application.StartupPath + "\\tessdata.jpg", ImageFormat.Jpeg);
                        gra.Dispose();
                        bmp.Dispose();
                    }
                }
                catch (Exception) { }
                mainWindow.result();
                this.Close();
            }
        }

        public CutScreen()
        {
            InitializeComponent();
        }

        private void CutScreen_KeyUp(object sender, KeyEventArgs e)
        {
            this.Close();
            ScreenShot result = new ScreenShot();
            result.Show();
        }

        private void CutScreen_Load(object sender, EventArgs e)
        {
            this.Hide();
            Bitmap printScreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printScreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printScreen.Size);
            using (MemoryStream s = new MemoryStream())
            {
                printScreen.Save(s, ImageFormat.Bmp);
                pictureBox1.Size = new Size(this.Width, this.Height);
                pictureBox1.Image = Image.FromStream(s);
            }
            this.Show();
            this.Cursor = Cursors.Cross;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                this.Cursor = Cursors.Cross;
                return;
            }

            if (start)
            {
                selectedWidth = e.X - selectX;
                selectedHeight = e.Y - selectY;
                if (selectedWidth > 0 && selectedHeight > 0)
                {
                    this.Cursor = Cursors.Cross;
                }
                else
                {
                    this.Cursor = Cursors.No;
                }
                pictureBox1.Refresh();

                pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectedWidth, selectedHeight);
            }


        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.No)
            {
                this.Cursor = Cursors.Cross;
            }
            if (!start)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.Cursor = Cursors.Cross;
                    selectX = e.X;
                    selectY = e.Y;
                    selectPen = new Pen(Color.Red, 2);
                    selectPen.DashStyle = DashStyle.DashDotDot;
                }
                pictureBox1.Refresh();
                start = true;
            }
            else
            {
                if (pictureBox1.Image == null)
                    return;

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    pictureBox1.Refresh();
                    selectedWidth = e.X - selectX;
                    selectedHeight = e.Y - selectY;
                    pictureBox1.CreateGraphics().DrawRectangle(selectPen, selectX, selectY, selectedWidth, selectedHeight);
                }
                start = false;
                SaveToClipBoard();
            }


        }
    }
}
