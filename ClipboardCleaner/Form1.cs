using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardCleaner {
    public partial class Form1 : Form {
        private readonly LowLevelKeyboardHook _hook;
        private MinimizeToTray _minimizeToTray;

        public Form1(LowLevelKeyboardHook hook) {
            InitializeComponent();
            _hook = hook;
        }

        private void Form1_Load(object s, EventArgs e) {
            _hook.OnKeyDown += HookOnOnKeyDown;
            this.Closing += (sender, args) => _hook.OnKeyDown -= HookOnOnKeyDown;
            _minimizeToTray = new MinimizeToTray(trayIcon, this);
            Hide();
        }

        private void HookOnOnKeyDown(object o, Keys keys) {
            var isV = (int) keys == 'V';
            bool ctrlDown = // TODO: Also triggers on shift
                (LowLevelKeyboardHook.GetKeyState((int) Keys.LControlKey) & 0xF0) != 0 ||
                (LowLevelKeyboardHook.GetKeyState((int) Keys.RControlKey) & 0xF0) != 0;


            if (isV && ctrlDown) {
                if (Clipboard.ContainsText(TextDataFormat.Html)) {
                    Clipboard.SetText(Clipboard.GetText(TextDataFormat.UnicodeText));
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
            e.Cancel = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }
       
    }
}