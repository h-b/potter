using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace potter
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();

            comboBoxExecuteCommand.Text = (ConfigurationManager.AppSettings[comboBoxExecuteCommand.Name] ?? comboBoxExecuteCommand.Items[0]).ToString();
            textBoxDefaultTimeInverval.Text = ConfigurationManager.AppSettings[textBoxDefaultTimeInverval.Name] ?? "";
            textBoxOptionalTimeInterval.Text = (Int32.Parse(ConfigurationManager.AppSettings[textBoxOptionalTimeInterval.Name])/60).ToString() ?? "";
            textBoxRoundTimes.Text = ConfigurationManager.AppSettings[textBoxRoundTimes.Name] ?? "";
            textBoxInfo.Text = textBoxInfo.Text.Replace("{timeout}", ScreenSaverTimeout.ToString());
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(comboBoxExecuteCommand.Name);
            config.AppSettings.Settings.Remove(textBoxDefaultTimeInverval.Name);
            config.AppSettings.Settings.Remove(textBoxOptionalTimeInterval.Name);
            config.AppSettings.Settings.Remove(textBoxRoundTimes.Name);
            config.AppSettings.Settings.Add(comboBoxExecuteCommand.Name, comboBoxExecuteCommand.Text);
            config.AppSettings.Settings.Add(textBoxDefaultTimeInverval.Name, textBoxDefaultTimeInverval.Text);
            config.AppSettings.Settings.Add(textBoxOptionalTimeInterval.Name, (60*Int32.Parse(textBoxOptionalTimeInterval.Text)).ToString());
            config.AppSettings.Settings.Add(textBoxRoundTimes.Name, textBoxRoundTimes.Text);
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        internal static int DefaultTimeInterval
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings[new Configuration().textBoxDefaultTimeInverval.Name] ?? "-1");
            }
        }

        internal static int OptionalTimeInterval
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings[new Configuration().textBoxOptionalTimeInterval.Name] ?? "-1");
            }
        }

        internal static int RoundTimes
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings[new Configuration().textBoxRoundTimes.Name] ?? "-1");
            }
        }

        internal static string ExecuteCommand
        {
            get
            {
                return ConfigurationManager.AppSettings[new Configuration().comboBoxExecuteCommand.Name] ?? new Configuration().comboBoxExecuteCommand.Items[0].ToString();
            }
        }

        internal static int ScreenSaverTimeout
        {
            get
            {
                return WindowsSpecific.ScreenSaverTimeout;
            }
        }

        private static char listDelimiter = '`';
        private static string ActivityListName = "ActivityList";

        public static List<string> ActivityList
        {
            get
            {
                var currentList = ConfigurationManager.AppSettings[ActivityListName];
                if (currentList != null)
                {
                    return new List<string>(currentList.Split(listDelimiter));
                }
                else
                {
                    return new List<string>();
                }
            }

            set
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings.Remove(ActivityListName);
                if (value.Count > 0)
                {
                    config.AppSettings.Settings.Add(ActivityListName, value.Aggregate((x, y) => x + listDelimiter + y));
                }
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
        }
    }
}
