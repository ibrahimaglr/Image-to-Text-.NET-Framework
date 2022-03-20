using KeyboardHook;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tesseract;


namespace ImageToText1
{
    public partial class mainWindow : Form
    {

        public static void result()
        {
            ImageToText.Properties.Settings.Default.notify = true;
            ImageToText.Properties.Settings.Default.Save();
            try
            {
                //seçtiği dilden çeivrme
                switch (ImageToText.Properties.Settings.Default.Launge)
                {
                    case 0:
                        {
                            var img = new Bitmap(Application.StartupPath + "\\tessdata.jpg");
                            var ocr = new TesseractEngine("./tessdata", "tur");
                            var sonuc = ocr.Process(img);
                            Clipboard.SetText(sonuc.GetText());
                            sonuc.Dispose();
                            img.Dispose();
                            ocr.Dispose();
                        }
                        break;

                    case 1:
                        {
                            var img = new Bitmap(Application.StartupPath + "\\tessdata.jpg");
                            var ocr = new TesseractEngine("./tessdata", "eng");
                            var sonuc = ocr.Process(img);
                            Clipboard.SetText(sonuc.GetText());
                            sonuc.Dispose();
                            img.Dispose();
                            ocr.Dispose();
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("hata kodu1 \n" + exception.Message);
            }
        }
        public mainWindow()
        {
            InitializeComponent();
            HookedKey();
        }
        globalKeyboardHook hookedKey = new globalKeyboardHook();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.SelectedIndex)
                {

                    case 0:
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            var img = new Bitmap(openFileDialog1.FileName);
                            var ocr = new TesseractEngine("./tessdata", "tur");
                            var sonuc = ocr.Process(img);
                            Clipboard.SetText(sonuc.GetText());

                            notifyIcon1.ShowBalloonTip(5000);
                            sonuc.Dispose();
                            img.Dispose();
                            ocr.Dispose();
                        }
                        break;

                    case 1:
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            var img = new Bitmap(openFileDialog1.FileName);
                            var ocr = new TesseractEngine("./tessdata", "eng");
                            var sonuc = ocr.Process(img);
                            Clipboard.SetText(sonuc.GetText());

                            notifyIcon1.ShowBalloonTip(5000);
                            sonuc.Dispose();
                            img.Dispose();
                            ocr.Dispose();
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Image to Text : ERROR CODE 001 \n" + exception.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageToText.Properties.Settings.Default.Launge = comboBox1.SelectedIndex;
            ImageToText.Properties.Settings.Default.Save();
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            if (ImageToText.Properties.Settings.Default.Start)
            {
                checkBox1.Checked = true;
            }
            this.Location = ImageToText.Properties.Settings.Default.Location;
            comboBox1.SelectedIndex = ImageToText.Properties.Settings.Default.Launge;
            label1.Text = String.Format("({0}) \nOpens Snipping tool when pressed.", ((Keys)(ImageToText.Properties.Settings.Default.Key)));
        }

        public void HookedKey()
        {
            try
            {
                if (ImageToText.Properties.Settings.Default.Key == 16)
                {
                    hookedKey.HookedKeys.Add(Keys.LShiftKey);
                    hookedKey.HookedKeys.Add(Keys.RShiftKey);
                }

                if (ImageToText.Properties.Settings.Default.Key == 17)
                {
                    hookedKey.HookedKeys.Add(Keys.LControlKey);
                    hookedKey.HookedKeys.Add(Keys.RControlKey);
                }
                else if (ImageToText.Properties.Settings.Default.Key == 18)
                {
                    hookedKey.HookedKeys.Add(Keys.Alt);
                }
                else
                {
                    hookedKey.HookedKeys.Add((Keys)ImageToText.Properties.Settings.Default.Key);
                }
                hookedKey.KeyUp += new KeyEventHandler(ResultHandler);
            }
            catch (Exception) { }
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            ImageToText.Properties.Settings.Default.Location = this.Location;
            ImageToText.Properties.Settings.Default.Save();
            label1.Text = String.Format("({0}) \nOpens Snipping tool when pressed.", ((Keys)(ImageToText.Properties.Settings.Default.Key)));
            label1.BackColor = Color.GreenYellow;
            label1.ForeColor = Color.Black;
        }

        void ResultHandler(object sender, KeyEventArgs e)
        {
            ScreenShot resulScreenShot = new ScreenShot();
            resulScreenShot.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Press to Key...";
            label1.BackColor = Color.IndianRed;
            label1.ForeColor = Color.White;
        }

        private void mainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (label1.Text == "Press to Key...")
            {
                label1.BackColor = Color.YellowGreen;
                label1.Text = String.Format("({0})\nOpens Snipping tool when pressed.", e.KeyCode.ToString());
                ImageToText.Properties.Settings.Default.Key = e.KeyValue;
                ImageToText.Properties.Settings.Default.Save();
                Application.Restart();
                Environment.Exit(0);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception) { }
        }
        private int count = 0;
        private void mainWindow_Activated(object sender, EventArgs e)
        {
            if (count == 0)
            {
                this.Hide();
                count++;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ImageToText.Properties.Settings.Default.Start = true;
                ImageToText.Properties.Settings.Default.Save();
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.SetValue("Image To Text", "\"" + Application.ExecutablePath + "\"");
                    key.Dispose();
                }
                catch (Exception)
                { }
            }
            else
            {
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    key.DeleteValue("Image To Text");
                }
                catch (Exception)
                { }
                ImageToText.Properties.Settings.Default.Start = false;
                ImageToText.Properties.Settings.Default.Save();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ibrahimaglr/Image-to-Text-.NET-Framework");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ImageToText.Properties.Settings.Default.notify)
            {
                notifyIcon1.ShowBalloonTip(5000);
                ImageToText.Properties.Settings.Default.notify = false;
                ImageToText.Properties.Settings.Default.Save();
            }

        }
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, Clipboard.GetText());
            var s = Process.Start("Notepad.exe", tempFilePath);
            if (!s.WaitForExit(1000)) File.Delete(tempFilePath);
        }
    }
}
