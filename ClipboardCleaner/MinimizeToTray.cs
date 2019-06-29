using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace ClipboardCleaner {
    class MinimizeToTray {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Form1));
        private readonly NotifyIcon _trayIcon;
        private readonly Form _form;

        public MinimizeToTray(NotifyIcon trayIcon, Form form) {
            this._trayIcon = trayIcon;
            this._form = form;

            form.SizeChanged += OnMinimizeWindow;
        }

        private void OnMinimizeWindow(object sender, EventArgs e) {
            try {
                if (_form.WindowState == FormWindowState.Minimized) {
                    _form.Hide();
                    _trayIcon.Visible = true;
                }
                else {
                    _trayIcon.Visible = false;
                }
            }
            catch (Exception ex) {
                Logger.Warn(ex.Message);
                Logger.Warn(ex.StackTrace);
            }
        }
    }
}
