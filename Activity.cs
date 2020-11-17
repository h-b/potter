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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                comboBoxActivity.DroppedDown = true;
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
                    previousActivity = comboBoxActivity.Text.Trim();

                    var updatedList = Configuration.ActivityList;

                    if (updatedList.Contains(previousActivity))
                    {
                        updatedList.Remove(previousActivity);
                    }

                    updatedList.Insert(0, comboBoxActivity.Text.Trim());
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
            }));
        }

        private void Activity_Load(object sender, EventArgs e)
        {
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
    }
}
