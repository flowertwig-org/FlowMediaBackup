using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var driveWatcher = new DriveWatcher();
            driveWatcher.OpticalDiskArrived += DriveWatcherOnOpticalDiskArrived;
            driveWatcher.Start();

            while (true)
            {
                System.Threading.Thread.Sleep(1 * 1000);
            }
        }

        private static void DriveWatcherOnOpticalDiskArrived(object sender, OpticalDiskArrivedEventArgs e)
        {
            if (e.Drive.IsReady)
            {
                try
                {
                    if (ProgressForm.Drives != null && !ProgressForm.Drives.ContainsKey(e.Drive.Name))
                    {
                        var dialog = new ProgressForm(e.Drive);
                        dialog.ShowDialog();
                    }
                }
                catch (ArgumentException)
                {
                    // If drive has already been added
                }
            }
            else
            {
                lock (ProgressForm.Drives)
                {
                    if (ProgressForm.Drives != null && ProgressForm.Drives.ContainsKey(e.Drive.Name))
                    {
                        ProgressForm form = ProgressForm.Drives[e.Drive.Name];
                        if (form.InvokeRequired)
                        {
                            form.Invoke(form.PublicClose);
                        }
                    }
                }
            }
        }
    }
}
