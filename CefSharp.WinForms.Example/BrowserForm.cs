﻿using System;
using System.Windows.Forms;
using CefSharp.Example;
using CefSharp.WinForms;

namespace CefSharp.WinForms.Example
{
    public partial class BrowserForm : Form
    {
        public event EventHandler ShowDevToolsActivated
        {
            add { showDevToolsMenuItem.Click += value; }
            remove { showDevToolsMenuItem.Click -= value; }
        }

        public event EventHandler CloseDevToolsActivated
        {
            add { closeDevToolsMenuItem.Click += value; }
            remove { showDevToolsMenuItem.Click -= value; }
        }

        public event EventHandler ExitActivated
        {
            add { exitToolStripMenuItem.Click += value; }
            remove { exitToolStripMenuItem.Click -= value; }
        }

        public event EventHandler UndoActivated
        {
            add { undoMenuItem.Click += value; }
            remove { undoMenuItem.Click -= value; }
        }

        public event EventHandler RedoActivated
        {
            add { redoMenuItem.Click += value; }
            remove { redoMenuItem.Click -= value; }
        }

        public event EventHandler CutActivated
        {
            add { cutMenuItem.Click += value; }
            remove { cutMenuItem.Click -= value; }
        }

        public event EventHandler CopyActivated
        {
            add { copyMenuItem.Click += value; }
            remove { copyMenuItem.Click -= value; }
        }

        public event EventHandler PasteActivated
        {
            add { pasteMenuItem.Click += value; }
            remove { pasteMenuItem.Click -= value; }
        }

        public event EventHandler DeleteActivated
        {
            add { deleteMenuItem.Click += value; }
            remove { deleteMenuItem.Click -= value; }
        }

        public event EventHandler SelectAllActivated
        {
            add { selectAllMenuItem.Click += value; }
            remove { selectAllMenuItem.Click -= value; }
        }

        public event EventHandler TestResourceLoadActivated
        {
            add { testResourceLoadMenuItem.Click += value; }
            remove { testResourceLoadMenuItem.Click -= value; }
        }

        public event EventHandler TestSchemeLoadActivated
        {
            add { testSchemeLoadMenuItem.Click += value; }
            remove { testSchemeLoadMenuItem.Click -= value; }
        }

        public event EventHandler TestExecuteScriptActivated
        {
            add { testExecuteScriptMenuItem.Click += value; }
            remove { testExecuteScriptMenuItem.Click -= value; }
        }

        public event EventHandler TestEvaluateScriptActivated
        {
            add { testEvaluateScriptMenuItem.Click += value; }
            remove { testEvaluateScriptMenuItem.Click -= value; }
        }

        public event EventHandler TestBindActivated
        {
            add { testBindMenuItem.Click += value; }
            remove { testBindMenuItem.Click -= value; }
        }

        public event EventHandler TestConsoleMessageActivated
        {
            add { testConsoleMessageMenuItem.Click += value; }
            remove { testConsoleMessageMenuItem.Click -= value; }
        }

        public event EventHandler TestTooltipActivated
        {
            add { testTooltipMenuItem.Click += value; }
            remove { testTooltipMenuItem.Click -= value; }
        }

        public event EventHandler TestPopupActivated
        {
            add { testPopupMenuItem.Click += value; }
            remove { testPopupMenuItem.Click -= value; }
        }

        public event EventHandler TestLoadStringActivated
        {
            add { testLoadStringMenuItem.Click += value; }
            remove { testLoadStringMenuItem.Click -= value; }
        }

        public event EventHandler TestCookieVisitorActivated
        {
            add { testCookieVisitorMenuItem.Click += value; }
            remove { testCookieVisitorMenuItem.Click -= value; }
        }

        public event Action<object, string> UrlActivated;

        public event EventHandler BackActivated
        {
            add { backButton.Click += value; }
            remove { backButton.Click -= value; }
        }

        public event EventHandler ForwardActivated
        {
            add { forwardButton.Click += value; }
            remove { forwardButton.Click -= value; }
        }

        private readonly WebView webView;

        public BrowserForm()
        {
            InitializeComponent();
            Text = "CefSharp";
            WindowState = FormWindowState.Maximized;

            webView = new WebView("custom://cefsharp/home")
            {
                Dock = DockStyle.Fill
            };
            toolStripContainer.ContentPanel.Controls.Add(webView);
        }

        public void SetTitle(string title)
        {
            Text = title;
        }

        public void SetAddress(string address)
        {
            urlTextBox.Text = address;
        }

        public void SetAddress(Uri uri)
        {
            urlTextBox.Text = uri.ToString();
        }

        public void SetCanGoBack(bool can_go_back)
        {
            backButton.Enabled = can_go_back;
        }

        public void SetCanGoForward(bool can_go_forward)
        {
            forwardButton.Enabled = can_go_forward;
        }

        public void SetIsLoading(bool is_loading)
        {
            goButton.Text = is_loading ?
                "Stop" :
                "Go";
            goButton.Image = is_loading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void ExecuteScript(string script)
        {
            webView.ExecuteScriptAsync(script);
        }

        public object EvaluateScript(string script)
        {
            return webView.EvaluateScript(script);
        }

        public void DisplayOutput(string output)
        {
            outputLabel.Text = output;
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void HandleGoButtonClick(object sender, EventArgs e)
        {
            var handler = this.UrlActivated;
            if (handler != null)
            {
                handler(this, urlTextBox.Text);
            }
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            var handler = UrlActivated;
            if (handler != null)
            {
                handler(this, urlTextBox.Text);
            }
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
    }
}
