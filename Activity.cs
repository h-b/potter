using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    public partial class Activity : Form
    {
        bool showConfiguration;
        object updatingButtonTexts = new object();
        System.Timers.Timer updateButtonTexts = new System.Timers.Timer();

        internal TimeSpan DefaultTime { get; private set; }
        internal TimeSpan OptionalTime { get; private set; }

        public Activity(string previousActivity, string previousCategory, bool showConfiguration)
        {
            this.showConfiguration = showConfiguration;
            InitializeTimeSpans(DateTime.Now);
            InitializeComponent();
            FillComboboxActivity(previousActivity);
            FillComboboxCategory(previousCategory);

            updateButtonTexts.Elapsed += UpdateButtonTexts;
            updateButtonTexts.Interval = 1000;
        }

        private void FillComboboxActivity(string currentActivity)
        {
            comboBoxActivity.Items.Clear();
            comboBoxActivity.Items.AddRange(Configuration.ActivityList.ToArray());
            comboBoxActivity.Text = currentActivity;
        }

        private void FillComboboxCategory(string currentCategory)
        {
            comboBoxCategory.Items.Clear();
            comboBoxCategory.Items.AddRange(Configuration.CategoryList.ToArray());
            comboBoxCategory.Text = currentCategory;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                comboBoxActivity.Focus();
                comboBoxActivity.DroppedDown = true;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.C))
            {
                comboBoxCategory.Focus();
                comboBoxCategory.DroppedDown = true;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                ClickConfiguationButton();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonRemoveActivity_Click(object sender, EventArgs e)
        {
            if (Configuration.ActivityList.Contains(comboBoxActivity.Text.Trim()))
            {
                var updatedList = Configuration.ActivityList;
                updatedList.Remove(comboBoxActivity.Text.Trim());
                Configuration.ActivityList = updatedList;
                FillComboboxActivity(Configuration.CurrentActivity);
            }
        }

        private void buttonRemoveCategory_Click(object sender, EventArgs e)
        {
            if (Configuration.CategoryList.Contains(comboBoxCategory.Text.Trim()))
            {
                var updatedList = Configuration.CategoryList;
                updatedList.Remove(comboBoxCategory.Text.Trim());
                Configuration.CategoryList = updatedList;
                FillComboboxCategory(Configuration.CurrentCategory);
            }
        }

        private void ButtonConfiguration_Click(object sender, EventArgs e)
        {
            TopMost = false;
            Configuration dlgConfiguration = new Configuration();
            dlgConfiguration.ShowDialog();
            updateButtonTexts.Stop();
            updateButtonTexts.Start();
            TopMost = true;
        }

        private void UpdateButtonTexts(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (Monitor.TryEnter(updatingButtonTexts))
                {
                    try
                    {
                        updateButtonTexts.Stop();
                        DateTime now = DateTime.Now;
                        InitializeTimeSpans(now);

                        buttonAskDefault.Text = string.Format(String.Format("Ask again at {0}", (now + DefaultTime).ToString(Timesheet.timeFormat)));
                        buttonAskOptional.Text = string.Format(String.Format("Ask again at {0}", (now + OptionalTime).ToString(Timesheet.timeFormat)));
                        updateButtonTexts.Start();
                    }
                    finally
                    {
                        Monitor.Exit(updatingButtonTexts);
                    }
                }
            }));
        }

        private void InitializeTimeSpans(DateTime now)
        {
            int currentMinute = now.Minute;
            DefaultTime = calculateOptimumInterval(Configuration.DefaultTimeInterval, currentMinute, Configuration.DefaultTimeInterval);
            OptionalTime = calculateOptimumInterval(Configuration.OptionalTimeInterval, currentMinute, Configuration.DefaultTimeInterval);
        }

        private static TimeSpan calculateOptimumInterval(int selectedInterval, int minute, int defaultTimeInterval)
        {
            int optimumInterval = selectedInterval;
            int score = 0;

            for (int testInterval = selectedInterval - defaultTimeInterval / 2; testInterval <= selectedInterval + defaultTimeInterval / 2; ++testInterval)
            {
                int m = (minute + testInterval) % 60;
                if (score < 5 && m == 0)
                {
                    optimumInterval = testInterval;
                    score = 5;
                }
                else if (score < 4 && m == 30)
                {
                    optimumInterval = testInterval;
                    score = 4;
                }
                else if (score < 3 && (m == 15 || m == 45))
                {
                    optimumInterval = testInterval;
                    score = 3;
                }
                else if (score < 2 && (m == 10 || m == 20 || m == 40 || m == 50))
                {
                    optimumInterval = testInterval;
                    score = 2;
                }
                else if (score < 1 && (m == 5 || m == 25 || m == 35 || m == 55))
                {
                    optimumInterval = testInterval;
                    score = 1;
                }
            }

            return TimeSpan.FromMinutes(optimumInterval);
        }

        internal string Current
        {
            get
            {
                return comboBoxActivity.Text.Trim();
            }
        }

        internal string Category
        {
            get
            {
                return comboBoxCategory.Text.Trim();
            }
        }

        private void Activity_FormClosing(object sender, FormClosingEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (string.IsNullOrWhiteSpace(comboBoxActivity.Text))
                {
                    TopMost = false;
                    MessageBox.Show("Please enter your current activity.", "Time Tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TopMost = true;
                    e.Cancel = true;
                }
                else
                {
                    updateButtonTexts.Stop();
                }
            }));
        }

        private void Activity_Load(object sender, EventArgs e)
        {
            UpdateButtonTexts(new object(), new EventArgs());
            updateButtonTexts.Start();

            if (showConfiguration)
            {
                ClickConfiguationButton();
            }
        }

        private void ClickConfiguationButton()
        {
            new Task(() =>
            {
                Invoke(new Action(() =>
                {
                    buttonConfiguration.PerformClick();
                }));
            }).Start();
        }

        string forbiddenChars = "`|<>^\"&";

        private void comboBoxActivity_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = comboBoxActivity.Text.IndexOfAny(forbiddenChars.ToCharArray()) != -1;
            if (e.Cancel)
            {
                MessageBox.Show("The following characters must not be used in the activity description: " + forbiddenChars, "Failed validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxCategory_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = comboBoxActivity.Text.IndexOfAny(forbiddenChars.ToCharArray()) != -1;
            if (e.Cancel)
            {
                MessageBox.Show("The following characters must not be used in the category description: " + forbiddenChars, "Failed validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
