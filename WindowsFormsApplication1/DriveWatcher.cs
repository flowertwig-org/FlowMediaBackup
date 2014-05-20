using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;

namespace WindowsFormsApplication1
{
    public class DriveWatcher
    {
        public delegate void OpticalDiskArrivedEventHandler(Object sender, OpticalDiskArrivedEventArgs e);

        /// <summary>
        ///     Gets or sets the time, in seconds, before the drive watcher checks for new media insertion relative to the last occurance of check.
        /// </summary>
        public int Interval = 1;

        private Timer _driveTimer;

        private Dictionary<string, bool> _drives;

        /// <summary>
        ///     Occurs when a new optical disk is inserted or ejected.
        /// </summary>
        public event OpticalDiskArrivedEventHandler OpticalDiskArrived;

        private void OnOpticalDiskArrived(OpticalDiskArrivedEventArgs e)
        {
            OpticalDiskArrivedEventHandler handler = OpticalDiskArrived;
            if (handler != null) handler(this, e);
        }

        public void Start()
        {
            _drives = new Dictionary<string, bool>();
            foreach (
                DriveInfo drive in
                    DriveInfo.GetDrives().Where(driveInfo => driveInfo.DriveType.Equals(DriveType.CDRom)))
            {
                _drives.Add(drive.Name, false);
            }
            _driveTimer = new Timer { Interval = Interval * 1000 };
            _driveTimer.Elapsed += DriveTimerOnElapsed;
            _driveTimer.Start();
        }

        public void Stop()
        {
            if (_driveTimer != null)
            {
                _driveTimer.Stop();
                _driveTimer.Dispose();
            }
        }

        private void DriveTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                var tmp = new List<DriveInfo>();
                lock (_drives)
                {
                    var drives = DriveInfo.GetDrives().Where(
                        drive => drive.DriveType.Equals(DriveType.CDRom)
                                 && _drives.ContainsKey(drive.Name)
                                 && !_drives[drive.Name].Equals(drive.IsReady)
                        );
                    foreach (DriveInfo drive in drives)
                    {
                        if (!_drives[drive.Name].Equals(drive.IsReady))
                        {
                            _drives[drive.Name] = drive.IsReady;
                            tmp.Add(drive);
                        }
                    }
                }

                foreach (var drive in tmp)
                {
                    OnOpticalDiskArrived(new OpticalDiskArrivedEventArgs { Drive = drive });
                }
            }
            catch (Exception exception)
            {
                Debug.Write(exception.Message);
            }
            finally
            {
            }
        }
    }
}