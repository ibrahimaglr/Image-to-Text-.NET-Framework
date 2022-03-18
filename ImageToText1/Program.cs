using System;
using System.Threading;
using System.Windows.Forms;

namespace ImageToText1
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool control;
            Mutex mutex = new Mutex(true, "Program", out control);
            if (control == false)
            {
                MessageBox.Show("The application is already running\n \n Check the notification bar", "Image To Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainWindow());
            GC.KeepAlive(mutex);
        }
    }
}
