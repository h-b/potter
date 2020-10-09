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
        private static string previousActivity = "";
        bool showConfiguration;

        public Activity(bool showConfiguration)
        {
            this.showConfiguration = showConfiguration;
            InitializeComponent();
            UpdateButtonTexts();
            FillCombobox(previousActivity);
        }

        private void FillCombobox(string currentActivity)
        {
            comboBoxActivity.Items.Clear();
            comboBoxActivity.Items.AddRange(Configuration.ActivityList.ToArray());
            comboBoxActivity.Text = currentActivity;
        }

        private void buttonRemoveActivity_Click(object sender, EventArgs e)
        {
            if (Configuration.ActivityList.Contains(comboBoxActivity.Text.Trim()))
            {
                var updatedList = Configuration.ActivityList;
                updatedList.Remove(comboBoxActivity.Text.Trim());
                Configuration.ActivityList = updatedList;
                FillCombobox("");
            }
        }

        private void ButtonConfiguration_Click(object sender, EventArgs e)
        {
            TopMost = false;
            Configuration dlgConfiguration = new Configuration();
            dlgConfiguration.ShowDialog();
            UpdateButtonTexts();
            TopMost = true;
        }

        private void UpdateButtonTexts()
        {
            buttonAskDefault.Text = string.Format(String.Format("Ask again in {0} minutes", Configuration.DefaultTimeInterval));
            buttonAskOptional.Text = string.Format(String.Format("Ask again in {0} hours", Configuration.OptionalTimeInterval / 60));
        }

        internal string Current
        {
            get
            {
                return comboBoxActivity.Text.Trim();
            }
        }

        private void Activity_FormClosing(object sender, FormClosingEventArgs e)
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
                previousActivity = comboBoxActivity.Text.Trim();

                if (!Configuration.ActivityList.Contains(previousActivity))
                {
                    var updatedList = Configuration.ActivityList;
                    updatedList.Add(comboBoxActivity.Text.Trim());
                    try
                    {
                        Configuration.ActivityList = updatedList;
                    }
                    catch (System.Exception ex)
                    {
                        TopMost = false;
                        Configuration.ShowSaveError(ex);
                        TopMost = true;
                    }
                }
            }
        }

        private void Activity_Load(object sender, EventArgs e)
        {
            if (showConfiguration)
            {
                new Task(() =>
                {
                    Invoke(new Action(() =>
                    {
                        buttonConfiguration.PerformClick();
                    }));
                }).Start();
            }
        }
    }
}
