using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DisableScreensaver
{

    public partial class DisableScreenSaverForm : Form
    {
        private const int SPI_GETSCREENSAVERTIMEOUT = 14;
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int flags);

        private const string RegistryAutoRunName = "DisableScreensaver";
        private const string RegistryKey = @"SOFTWARE\DisableScreensaver\";
        private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static int GetScreenSaverTimeout()
        {
            var value = 0;
            SystemParametersInfo(SPI_GETSCREENSAVERTIMEOUT, 0, ref value, 0);
            return value;
        }

        public DisableScreenSaverForm()
        {
            InitializeComponent();

            timer1.Enabled = !LoadDisabled();
            timer1.Interval = (GetScreenSaverTimeout() - 1) * 1000;

            notifyIcon1.ContextMenuStrip = GenerateContextMenuStrip();
        }

        private static void PressScrollLock()
        {
            const byte vkScroll = 0x91;
            const byte keyeventfKeyup = 0x2;

            keybd_event(vkScroll, 0x45, 0, (UIntPtr)0);
            keybd_event(vkScroll, 0x45, keyeventfKeyup, (UIntPtr)0);
        }

        private static void Timer1Tick(object sender, EventArgs e)
        {
            PressScrollLock();
            PressScrollLock();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private static bool LoadDisabled()
        {
            bool result;
            bool.TryParse(GetRegistryValue("Disabled"), out result);

            bool disabled = false;

            if (result)
            {
                disabled = true;
            }

            return disabled;
        }

        private void DisabledClick(object sender, EventArgs e)
        {
            bool disabled = ((ToolStripMenuItem)sender).Checked;
            timer1.Enabled = !disabled;
            SetRegistryValue("Disabled", disabled.ToString());
        }

        private static void AutostartClick(object sender, EventArgs e)
        {
            MessageBox.Show("Disabled");
        }

        private static void SetAutoStart(bool autostartMenuItemChecked)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey(RunKey, true);

            if (rkApp == null)
            {
                Registry.CurrentUser.CreateSubKey(RunKey);
                rkApp = Registry.CurrentUser.OpenSubKey(RunKey, true);
            }

            if (rkApp != null)
            {
                if (autostartMenuItemChecked)
                {
                    if (!CheckAutoRunRegSet(rkApp))
                    {
                        rkApp.SetValue(RegistryAutoRunName, Application.ExecutablePath);
                    }
                }
                else
                {
                    if (CheckAutoRunRegSet(rkApp))
                    {
                        rkApp.DeleteValue(RegistryAutoRunName);
                    }
                }
            }
        }

        private static void SetRegistryValue(string keyName, string value)
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk = rk.CreateSubKey(RegistryKey);

            if (sk != null)
            {
                sk.SetValue(keyName, value);
            }
        }

        private static string GetRegistryValue(string keyName)
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk = rk.OpenSubKey(RegistryKey);

            if (sk != null)
            {
                if (sk.GetValue(keyName) != null)
                {
                    return sk.GetValue(keyName).ToString();
                }
            }

            return null;
        }

        private static bool CheckAutoRunRegSet(RegistryKey rk)
        {
            if (rk == null)
            {
                return false;
            }

            if (rk.GetValue(RegistryAutoRunName) == null)
            {
                return false;
            }

            return true;
        }

        private void AboutMenuItemClick(object sender, EventArgs e)
        {
            AboutMenu();
        }

        private void NotifyIcon1DoubleClick(object sender, EventArgs e)
        {
            if (notifyIcon1.ContextMenuStrip != null)
            {
                AboutMenu();
            }
            else
            {
                Activate();
            }
        }

        private void AboutMenu()
        {
            aboutTextBox.Text = $@"Disable ScreenSaver version {Assembly.GetExecutingAssembly().GetName().Version}";
            aboutTextBox.SelectionLength = 0;
            aboutTextBox.SelectionStart = 0;

            Show();

            notifyIcon1.ContextMenuStrip = null;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;

            OkayButton.Focus();
        }

        private ContextMenuStrip GenerateContextMenuStrip()
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip(components);
            var disabledMenuItem = new ToolStripMenuItem();
            var autoStartMenuItem = new ToolStripMenuItem();
            var aboutMenuItem = new ToolStripMenuItem();
            var toolStripSeparator = new ToolStripSeparator();
            var exitMenuItem = new ToolStripMenuItem();

            contextMenuStrip.Items.AddRange
            (
                new ToolStripItem[]
                {
                    disabledMenuItem,
                    autoStartMenuItem,
                    aboutMenuItem,
                    toolStripSeparator,
                    exitMenuItem
                }
            );

            disabledMenuItem.Checked = !timer1.Enabled;
            disabledMenuItem.CheckOnClick = true;
            disabledMenuItem.Text = @"Disabled";
            disabledMenuItem.Click += DisabledClick;
            autoStartMenuItem.CheckOnClick = true;
            autoStartMenuItem.Text = @"Automatically start";
            autoStartMenuItem.Click += AutostartClick;
            aboutMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            aboutMenuItem.Text = @"About";
            aboutMenuItem.Click += AboutMenuItemClick;
            exitMenuItem.Text = @"Exit";
            exitMenuItem.Click += ExitClick;

            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey(RunKey, true);
            autoStartMenuItem.Checked = CheckAutoRunRegSet(rkApp);

            return contextMenuStrip;
        }

        private void DisableScreensaverForm_Paint(object sender, PaintEventArgs e)
        {
            if (notifyIcon1.ContextMenuStrip != null)
            {
                Hide();
            }
        }

        private void DisableScreensaverForm_Load(object sender, EventArgs e)
        {
            if (notifyIcon1.ContextMenuStrip != null)
            {
                Hide();
            }
        }

        private void NotifyIcon1Click(object sender, EventArgs e)
        {
            if (notifyIcon1.ContextMenuStrip == null)
            {
                Activate();
            }
        }

        private void OkayButtonClick(object sender, EventArgs e)
        {
            notifyIcon1.ContextMenuStrip = GenerateContextMenuStrip();
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            Hide();
        }
    }
}
