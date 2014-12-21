using HandBrake.ApplicationServices.Model;
using HandBrake.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TMDbLib.Client;
using TMDbLib.Objects.Search;

namespace WindowsFormsApplication1
{
    public partial class ProgressForm : Form
    {
        private static SortedList<string, ProgressForm> _drives = new SortedList<string, ProgressForm>();

        public static SortedList<string, ProgressForm> Drives
        {
            get { return _drives; }
        }

        private DriveInfo _drive;
        private string _title = "";
        private List<string> _titleSuggestions = new List<string>();

        public delegate void CloseDelegate();
        public CloseDelegate PublicClose;

        public ProgressForm()
            : this(null)
        {

        }
        public ProgressForm(DriveInfo driveInfo)
        {
            _drives.Add(driveInfo.Name, this);
            _drive = driveInfo;

            PublicClose = new CloseDelegate(Close);

            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            backgroundWorker2.ProgressChanged += backgroundWorker2_ProgressChanged;
            backgroundWorker2.RunWorkerCompleted += backgroundWorker2_RunWorkerCompleted;

            cbTitles.SelectedValueChanged += cbTitles_SelectedValueChanged;
            cbTitles.TextChanged += cbTitles_TextChanged;
        }

        void cbTitles_TextChanged(object sender, EventArgs e)
        {
            if (cbTitles.Text.Length > 2)
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        void cbTitles_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbTitles.Text.Length > 2)
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
            //_title = cbTitles.Text;
            ////lblMediaTitle.Text = cbTitles.Text;
            //lblMediaTitle.Visible = true;
            //cbTitles.Visible = false;
            //backgroundWorker2.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbTitles.Text.Length < 2)
            {
                MessageBox.Show("Media title has to be more then 2 chars long");
                return;
            }
            _title = cbTitles.Text;
            //lblMediaTitle.Text = cbTitles.Text;
            lblMediaTitle.Visible = true;
            cbTitles.Visible = false;
            button1.Visible = false;
            backgroundWorker2.RunWorkerAsync();
        }

        void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //_drives.Remove(_drive.Name);
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _drives.Remove(_drive.Name);
            base.OnClosing(e);
        }

        void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            var status = (ProcessStatus)e.UserState;

            switch (status)
            {
                case ProcessStatus.CopyTitle:
                    if (e.ProgressPercentage == 0)
                    {
                        lblMediaTitle.Text = _title;
                        label2.Text = "Copying...";
                        splitContainer2.Panel2Collapsed = false;
                    }
                    if (e.ProgressPercentage == 99)
                    {
                        label2.Text = "DONE!";
                    }
                    break;
                case ProcessStatus.Closing:
                    label2.Text = "DONE! Closing in " + (100 - e.ProgressPercentage);
                    break;
            }
        }

        private void ScanSource(string dir, HBConfiguration config)
        {

        }

        void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int progress = 0; progress < 100; progress++)
            //{
            backgroundWorker2.ReportProgress(0, ProcessStatus.CopyTitle);

            var instance = new HandBrakeInstance();
            instance.Initialize(1);

            var dir = @"C:\Copy\";
            if (!Directory.Exists(Path.GetDirectoryName(dir)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dir));
            }

            var config = new HBConfiguration
            {
                DisableQuickSyncDecoding = false,
                EnableDxva = false,
                IsDvdNavDisabled = true,
                IsLoggingEnabled = false,
                MinScanDuration = 10,
                PreviewScanCount = 10,
                ProcessPriority = "Below Normal",
                SaveLogCopyDirectory = "",
                SaveLogToCopyDirectory = false,
                SaveLogWithVideo = false,
                ScalingMode = VideoScaler.Lanczos,
                Verbosity = 1
            };

            ScanSource(dir, config);




            //    var rand = new Random();
            //    var sleepTime = rand.Next(100, 15000);
            //    Thread.Sleep(sleepTime);
            //}

            for (int progress = 0; progress < 100; progress++)
            {
                backgroundWorker2.ReportProgress(progress, ProcessStatus.Closing);
                Thread.Sleep(150);
            }
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_titleSuggestions.Count == 0)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                label2.Text = "Please choose a title.";
                cbTitles.Items.AddRange(_titleSuggestions.ToArray());
                lblMediaTitle.Visible = false;
                cbTitles.Visible = true;
                //button1.Visible = true;
            }
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            var status = (ProcessStatus)e.UserState;

            switch (status)
            {
                case ProcessStatus.Scanning:
                    break;
                case ProcessStatus.LookingUpInfo:
                    if (e.ProgressPercentage == 0)
                    {
                        label2.Text = "Looking up title...";
                        this.Height = splitContainer2.Panel1.Height + splitContainer1.Panel2.Height;
                        splitContainer1.Panel1Collapsed = false;
                        splitContainer2.Panel2Collapsed = true;
                        Top = Screen.PrimaryScreen.WorkingArea.Bottom - Height;
                    }
                    if (e.ProgressPercentage >= 99)
                    {
                        lblMediaTitle.Text = _title;
                        splitContainer2.Panel2Collapsed = false;
                    }
                    break;
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, ProcessStatus.LookingUpInfo);
            TMDbClient client = new TMDbClient("6e1b6e599f6cd3ce4cd1b9efe0d94642");
            backgroundWorker1.ReportProgress(2, ProcessStatus.LookingUpInfo);
            var query = _drive.VolumeLabel ?? "";
            query = query.Replace("_", " ").ToLower().Trim();
            var results = client.SearchMovie(query);
            if (results.TotalResults == 0)
            {
                query = query.Replace("dvd1", "");
                query = query.Replace("dvd2", "");
                query = query.Replace("dvd3", "");
                query = query.Replace("dvd", "");
                query = query.Trim();
                results = client.SearchMovie(query);
            }

            if (results.TotalResults == 0)
            {
                // TODO: Force user to enter name
                backgroundWorker1.ReportProgress(99, ProcessStatus.LookingUpInfo);
            }

            backgroundWorker1.ReportProgress(10, ProcessStatus.LookingUpInfo);

            if (results.TotalResults == 1)
            {
                // TODO: We only had one result, use it.
                _title = results.Results[0].Title;
                backgroundWorker1.ReportProgress(100, ProcessStatus.LookingUpInfo);
            }

            // TODO: Let user choose between the names.
            backgroundWorker1.ReportProgress(50, ProcessStatus.LookingUpInfo);
            foreach (SearchMovie result in results.Results)
            {
                if (result.ReleaseDate > DateTime.Now)
                {
                    // If it was not released yet, ignore it because it can not be this one.
                    continue;
                }

                var tmpTitle = "";
                if (result.Title.StartsWith("The ", true, CultureInfo.CurrentCulture))
                {
                    tmpTitle = result.Title.Substring(4) + ", The";
                }
                else
                {
                    tmpTitle = result.Title;
                }

                if (!_titleSuggestions.Contains(tmpTitle))
                {
                    _titleSuggestions.Add(tmpTitle);
                }
            }

            if (_titleSuggestions.Count == 1)
            {
                // If we have only added one suggestion, use it and continue.
                _title = _titleSuggestions[0];
                _titleSuggestions.Clear();
            }

            backgroundWorker1.ReportProgress(100, ProcessStatus.LookingUpInfo);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitFromDrive();

            cbTitles.Visible = false;
            button1.Visible = false;

            this.Height = splitContainer1.Panel2.Height;
            splitContainer1.Panel1Collapsed = true;

            Top = Screen.PrimaryScreen.WorkingArea.Bottom - Height;
            //Top = Screen.PrimaryScreen.WorkingArea.Top;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width;

            backgroundWorker1.RunWorkerAsync();
        }

        private void InitFromDrive()
        {
            if (_drive.IsReady)
            {
                _title = _drive.VolumeLabel;
                lblMediaTitle.Text = _title;
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public enum ProcessStatus
    {
        Scanning = 0,
        LookingUpInfo,
        CopyTitle,
        Closing
    }
}
