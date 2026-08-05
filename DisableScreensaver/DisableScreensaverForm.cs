using DisableScreensaver.Classes;
using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace DisableScreensaver
{

    public partial class DisableScreenSaverForm : Form
    {

        #region Registry

        private const string RegistryAutoRunName = "DisableScreensaver";
        private const string RegistryKey = @"SOFTWARE\DisableScreensaver\";
        private const string RunKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        #endregion

        public DisableScreenSaverForm()
        {
            InitializeComponent();

            notifyIcon1.ContextMenuStrip = GenerateContextMenuStrip();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            
            if (LockConfiguration.Instance.IsDisabled)
            {
                LockConfiguration.Instance.EnableScreenLock();
            }
            
            Close();
            
        }



        private void DisabledClick(object sender, EventArgs e)
        {
            
            if (LockConfiguration.Instance.IsDisabled)
            {
                
                bool disabled = ((ToolStripMenuItem)sender).Checked;
                
                try
                {
                    SetRegistryValue("Disabled", disabled.ToString());
                    LockConfiguration.Instance.EnableScreenLock();
                }
                catch (Exception)
                {
#if SERI_LOGGING
                    Log.Error(exception, "An error occurred while enabling the screensaver.");
#endif
                }
            }
            
        }

        /// <summary>
        /// Handles the click event for the "Automatically start" menu item.
        /// Toggles the application's auto-start behavior based on the menu item's checked state.
        /// </summary>
        /// <remarks>
        /// This method updates the auto-start configuration by invoking the <see cref="SetAutoStart"/> method.
        /// Ensure the sender is a <see cref="ToolStripMenuItem"/> with a valid checked state.
        /// </remarks>
        private static void AutostartClick(object sender, EventArgs e)
        {
            SetAutoStart(((ToolStripMenuItem)sender).Checked);
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
            aboutTextBox.Text = $@"Disable ScreenSaver {DateTime.Now.Year} version {Assembly.GetExecutingAssembly().GetName().Version}";
            aboutTextBox.SelectionLength = 0;
            aboutTextBox.SelectionStart = 0;

            Show();

            notifyIcon1.ContextMenuStrip = null;
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;

            OkayButton.Focus();
            
        }

        /// <summary>
        /// Generates and initializes a <see cref="ContextMenuStrip"/> for the application's notify icon.
        /// </summary>
        /// <returns>
        /// A <see cref="ContextMenuStrip"/> containing menu items for controlling the application's behavior,
        /// such as enabling/disabling functionality, toggling auto-start, displaying an "About" dialog, and exiting the application.
        /// </returns>
        /// <remarks>
        /// The generated context menu includes the following items:
        /// - "Disabled": Toggles the application's disabled state.
        /// - "Automatically start": Toggles the application's auto-start behavior.
        /// - "About": Opens an "About" dialog.
        /// - "Exit": Closes the application.
        /// The method also sets up event handlers for each menu item to handle user interactions.
        /// </remarks>
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


        /// <summary>
        /// Configures the application's auto-start behavior by adding or removing 
        /// its entry in the Windows registry under the "Run" key.
        /// </summary>
        /// <param name="autostartMenuItemChecked">
        /// A boolean value indicating whether the application should be set to 
        /// start automatically with Windows. Pass <c>true</c> to enable auto-start, 
        /// or <c>false</c> to disable it.
        /// </param>
        /// <remarks>
        /// This method modifies the registry key located at 
        /// <c>HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run</c>.
        /// Ensure the application has appropriate permissions to write to the registry.
        /// </remarks>
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

    }

}
