using ImageToText;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ImageToText1
{
    public partial class ScreenShot : Form
    {
        public ScreenShot()
        {
            InitializeComponent();
        }

        private void ScreenShot_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //allprint screen
            if (File.Exists(Application.StartupPath + "\\tessdata.jpg") == true)
            {
                Process.Start(Application.StartupPath + "\\tessdata.jpg");
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kod panel
            CutScreen result = new CutScreen();
            result.Show();
            this.Close();

        }

        private void ScreenShot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyData == Keys.F1)
            {
                if (File.Exists(Application.StartupPath + "\\tessdata.jpg") == true)
                {
                    Process.Start(Application.StartupPath + "\\tessdata.jpg");
                    this.Close();
                }
            }
            if (e.KeyData == Keys.F2)
            {
                this.Close();
                CutScreen result = new CutScreen();
                result.Show();

            }
        }

        private void ScreenShot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Close();
                CutScreen result = new CutScreen();
                result.Show();
            }
        }
    }
}