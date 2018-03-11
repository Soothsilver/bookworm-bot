using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bookworm.Output
{
    public class StatusOutput
    {
        private BookwormForm form;

        public StatusOutput(BookwormForm bookwormForm)
        {
            this.form = bookwormForm;
        }

        internal void UpdateAllStatus()
        {
            // Scan status
            DateTime lastScan = form.Bot.Scan.LastScanTimestamp;
            if (lastScan == DateTime.MinValue)
            {
                UpdateStatusLabel(form.lblScanningStatus, Color.Red, "Never");
            }
            else
            {
                TimeSpan timeElapsed = DateTime.Now - lastScan;
                if (timeElapsed.TotalSeconds <= 2)
                {
                    UpdateStatusLabel(form.lblScanningStatus, Color.LimeGreen, "OK (" + timeElapsed.TotalSeconds + "s)");
                }
                else
                {
                    UpdateStatusLabel(form.lblScanningStatus, Color.Orange, "Warning (" + timeElapsed.TotalSeconds + "s since last scan)");
                }
            }
            // Recognize status
            UpdateStatusLabel(form.lblRecognizingStatus, Color.Red, "Not implemented");
            UpdateDatabaseStatusLabel();

        }

        private void UpdateDatabaseStatusLabel()
        {
            if (form.Bot.Database.FirstTimeLaunch)
            {
                UpdateStatusLabel(form.lblDatabaseStatus, Color.Orange, "First launch (" + form.Bot.Database.Count + " entries)");
            }
            else
            {
                UpdateStatusLabel(form.lblDatabaseStatus, Color.Green, "OK (" + form.Bot.Database.Count + " entries)");
            }
        }

        private void UpdateStatusLabel(Label labelStatus, Color foreColor, string caption)
        {
            labelStatus.Text = caption;
            labelStatus.ForeColor = foreColor;
        }
    }
}
