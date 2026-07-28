using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DisableScreensaver
{

    public partial class DisableScreenSaverForm : Form
    {

        public bool IsEnabled { get; set; }


        public DisableScreenSaverForm()
        {
            InitializeComponent();

            notifyIcon1.ContextMenuStrip = GenerateContextMenuStrip();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }



        private void DisabledClick(object sender, EventArgs e)
        {
            bool disabled = ((ToolStripMenuItem)sender).Checked;
        }

        private static void AutostartClick(object sender, EventArgs e)
        {
            MessageBox.Show("Disabled");
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
