using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            textBoxOptionalTimeInterval.Text = ConfigurationManager.AppSettings[textBoxOptionalTimeInterval.Name] ?? "";
            textBoxRoundTimes.Text = ConfigurationManager.AppSettings[textBoxRoundTimes.Name] ?? "";
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            textBoxInfo.Text = textBoxInfo.Text.Replace("{timeout}", (ScreenSaverTimeout / 60).ToString());
            checkBoxStartOnLogin.Checked = AutomaticStartup;
            checkBoxStartOnLogin.Text = "start Potter automatically on login and enable " + HotKey + " to show it";
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
            config.AppSettings.Settings.Add(textBoxOptionalTimeInterval.Name, textBoxOptionalTimeInterval.Text);
            config.AppSettings.Settings.Add(textBoxRoundTimes.Name, textBoxRoundTimes.Text);
            try
            {
                Save(config);
            }
            catch (System.Exception ex)
            {
                ShowSaveError(ex);
            }
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

        public static string CurrentActivity
        {
            get
            {
                return ActivityList.Count == 0 ? "" : ActivityList[0];
            }

            set
            {
                var trimmedValue = value.Trim();
                if (!string.IsNullOrEmpty(trimmedValue))
                {
                    var updatedList = ActivityList;

                    if (updatedList.Contains(trimmedValue))
                    {
                        updatedList.Remove(trimmedValue);
                    }

                    updatedList.Insert(0, trimmedValue);
                    ActivityList = updatedList;
                }
            }
        }

        public static string CurrentCategory
        {
            get
            {
                return CategoryList.Count == 0 ? "" : CategoryList[0];
            }

            set
            {
                var trimmedValue = value.Trim();
                if (!string.IsNullOrEmpty(trimmedValue))
                {
                    var updatedList = CategoryList;

                    if (updatedList.Contains(trimmedValue))
                    {
                        updatedList.Remove(trimmedValue);
                    }

                    updatedList.Insert(0, trimmedValue);
                    CategoryList = updatedList;
                }
            }
        }

        private static char listDelimiter = '`';
        private static string ActivityListName = "ActivityList";
        private static string CategoryListName = "CategoryList";

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
                Save(config);
            }
        }

        public static List<string> CategoryList
        {
            get
            {
                var currentList = ConfigurationManager.AppSettings[CategoryListName];
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
                config.AppSettings.Settings.Remove(CategoryListName);
                if (value.Count > 0)
                {
                    config.AppSettings.Settings.Add(CategoryListName, value.Aggregate((x, y) => x + listDelimiter + y));
                }
                Save(config);
            }
        }

        static void Save(System.Configuration.Configuration config)
        {
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }

        internal static void ShowSaveError(Exception ex)
        {
            MessageBox.Show(string.Format("Could not save configuration file: {0}", ex.Message), "Time Tracker", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void checkBoxStartOnLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                AutomaticStartup = checkBoxStartOnLogin.Checked;
            }
            catch(Exception ex)
            {
                string msg = "Could not change startup mode: " + ex.Message;
                Logger.Append(msg);
                MessageBox.Show(msg, "Time tracker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBoxStartOnLogin.Checked = !checkBoxStartOnLogin.Checked;
            }
        }

        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

        internal static string StartupFolder
        {
            get
            {
                StringBuilder startupFolder = new StringBuilder(260);
                SHGetSpecialFolderPath(IntPtr.Zero, startupFolder, 7/*CSIDL_STARTUP*/, false);
                return startupFolder.ToString();
            }
        }

        internal static string ProgramName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        internal static string ShortcutPath
        {
            get
            {
                return Path.Combine(StartupFolder, ProgramName + ".lnk");
            }
        }

        internal static string HotKey
        {
            get
            {
                return "Ctrl+Shift+P";
            }
        }

        internal static bool AutomaticStartup
        {
            get
            {
                bool result = System.IO.File.Exists(ShortcutPath);
                Logger.Append("Detected state of automatic startup: " + result);
                return result;
            }

            set
            {
                if (value)
                {
                    Logger.Append("Trying to create potter shortcut at " + ShortcutPath);
                    IWshShortcut shortcut = (IWshShortcut)new WshShell().CreateShortcut(ShortcutPath);
                    shortcut.Description = "Shortcut to " + ProgramName;
                    shortcut.Hotkey = HotKey;
                    shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    shortcut.WorkingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    shortcut.Save();
                    Logger.Append("Successfully created potter shortcut at " + ShortcutPath);
                }
                else
                {
                    Logger.Append("Trying to delete " + ShortcutPath);
                    System.IO.File.Delete(ShortcutPath);
                    Logger.Append("Successfully deleted " + ShortcutPath);
                }
            }
        }
    }
}
