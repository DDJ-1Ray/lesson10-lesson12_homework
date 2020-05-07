using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace test12._0
{
    public partial class Form1 : Form
    {
        SimpleCrawler simpleCrawler = new SimpleCrawler();
        Thread thread = null;
        public Form1()
        {
            InitializeComponent();
            simpleCrawler.CrawlerStopped += SimpleCrawler_CrawlerStopped;
            simpleCrawler.PageDownloaded += SimpleCrawler_PageDownloaded;

        }
        private void SimpleCrawler_CrawlerStopped(SimpleCrawler obj)
        {
            Action action = () => label2.Text = "爬虫已停止";
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        private void SimpleCrawler_PageDownloaded(SimpleCrawler simpleCrawler1, string url)
        {
            if (this.showListBox.InvokeRequired)
            {
                Action<String> action = this.AddUrl;
                this.Invoke(action, new object[] { url });
            }
            else
            {
                AddUrl(url);
            }
        }
        private void AddUrl(string url)
        {
            showListBox.Items.Add(url);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            showListBox.Items.Clear();
            simpleCrawler.StartURL = urlTextBox.Text;

            Match match = Regex.Match(simpleCrawler.StartURL, SimpleCrawler.urlParseRegex);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            simpleCrawler.HostFilter = "^" + host + "$";
            simpleCrawler.FileFilter = ".html?$";

            if (thread != null)
            {
                thread.Abort();
            }
            thread = new Thread(simpleCrawler.Start);
            thread.Start();
            label2.Text = "爬虫已启动....";
        }
    }
}
