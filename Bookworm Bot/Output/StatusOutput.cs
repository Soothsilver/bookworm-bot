using Bookworm.Scan;
using Bookworm.Scrabble;
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
            Snapshot snapshot = form.Bot.Scan.LastSnapshot;
            if (lastScan == DateTime.MinValue)
            {
                UpdateStatusLabel(form.lblScanningStatus, Color.Red, "Never");
            }
            else
            {
                TimeSpan timeElapsed = DateTime.Now - lastScan;
                if (timeElapsed.TotalSeconds <= 2)
                {
                    UpdateStatusLabel(form.lblScanningStatus, Color.Green, "OK (" + timeElapsed.TotalSeconds + "s)");
                }
                else
                {
                    UpdateStatusLabel(form.lblScanningStatus, Color.Orange, "Warning (" + timeElapsed.TotalSeconds + "s)");
                }
            }
            // Recognize status
            if (snapshot != null)
            {
                if (snapshot.Keyboard.IsDark)
                {
                    UpdateStatusLabel(form.lblRecognizingStatus, Color.Red, "Keyboard is dark!");
                    this.form.panelGridIsDark.Visible = true;
                }
                else
                {
                    this.form.panelGridIsDark.Visible = false;
                    var recognition = form.Bot.Recognizator.LastRecognitionResults;
                    if (recognition != null)
                    {
                        TimeSpan timeElapsed = DateTime.Now - recognition.Timestamp;
                        if (timeElapsed.TotalSeconds <= 2)
                        {
                            UpdateStatusLabel(form.lblRecognizingStatus, Color.Green, "OK (" + timeElapsed.TotalSeconds + "s)");
                        }
                        else
                        {
                            UpdateStatusLabel(form.lblRecognizingStatus, Color.Orange, "Warning (" + timeElapsed.TotalSeconds + "s)");
                        }
                    }
                    else
                    {
                        UpdateStatusLabel(form.lblRecognizingStatus, Color.Red, "Never");
                    }
                }
            }
            UpdateDatabaseStatusLabel();
            // Scrabble status
            if (form.Bot.Vocabulary.VocabularyLoaded)
            {
                UpdateStatusLabel(form.lblWordlistStatus, Color.Green, "Loaded (" + form.Bot.Vocabulary.Vocabulary.Count + " words)");
                if (form.Bot.Vocabulary.LastScrabbleResult != null)
                {
                    Word bestWord = form.Bot.Vocabulary.LastScrabbleResult.BestWord;
                    if (bestWord != null)
                    {
                        string s = "";
                        foreach(Word word in form.Bot.Vocabulary.LastScrabbleResult.GoodWords)
                        {
                            s += word.Text + Environment.NewLine;
                        }
                        UpdateStatusLabel(form.lblBestWord, Color.Green, s);
                    }
                    else
                    {
                        UpdateStatusLabel(form.lblBestWord, Color.Orange, "No word found");
                    }
                    TimeSpan timeElapsed = DateTime.Now - form.Bot.Vocabulary.LastScrabbleResult.Timestamp;
                    if (timeElapsed.TotalSeconds <= 2)
                    {
                        UpdateStatusLabel(form.lblWordformingStatus, Color.Green, "OK (" + timeElapsed.TotalSeconds + "s)");
                    }
                    else
                    {
                        UpdateStatusLabel(form.lblWordformingStatus, Color.Orange, "Warning (" + timeElapsed.TotalSeconds + "s)");
                    }
                }
                else
                {
                    UpdateStatusLabel(form.lblWordformingStatus, Color.Red, "Never");
                }
            }
            else
            {
                UpdateStatusLabel(form.lblWordformingStatus, Color.Red, "Wordlist not yet loaded");
                UpdateStatusLabel(form.lblWordlistStatus, Color.Orange, "Loading...");
            }
            // Autonomous
            if (form.Bot.Autonomous.IsAutonomous)
            {
                UpdateStatusLabel(form.lblAutonomousModeStatus, Color.Black, form.Bot.Autonomous.Status);
                this.form.panelAutomode.Visible = true;
            }
            else
            {
                this.form.panelAutomode.Visible = false;
                UpdateStatusLabel(form.lblAutonomousModeStatus, Color.Red, "Disabled");
            }
            UpdateStatusLabel(form.lblSpecialSituation, Color.Black, this.form.Bot.Autonomous.SpecialSituationDescription);

        }

        private void UpdateDatabaseStatusLabel()
        {
            if (form.Bot.Database.Database.FirstTimeLaunch)
            {
                UpdateStatusLabel(form.lblDatabaseStatus, Color.Orange, "First launch (" + form.Bot.Database.Database.Count + " entries)");
            }
            else
            {
                UpdateStatusLabel(form.lblDatabaseStatus, Color.Green, "OK (" + form.Bot.Database.Database.Count + " entries)");
            }
        }

        private void UpdateStatusLabel(Label labelStatus, Color foreColor, string caption)
        {
            labelStatus.Text = caption;
            labelStatus.ForeColor = foreColor;
        }
    }
}
