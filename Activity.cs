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

        public Activity(string previousActivity, string previousCategory, bool showConfiguration)
        {
            this.showConfiguration = showConfiguration;
            InitializeComponent();
            UpdateButtonTexts();
            FillComboboxActivity(previousActivity);
            FillComboboxCategory(previousCategory);
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
