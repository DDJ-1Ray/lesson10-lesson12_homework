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


namespace test12._0
{
    public partial class Form1 : Form
    {
        SimpleCrawler simpleCrawler = new SimpleCrawler();
        public Form1()
        {
            InitializeComponent();
            simpleCrawler.PageDownloaded += SimpleCrawler_PageDownloaded;

        }
        private void SimpleCrawler_PageDownloaded(string url)
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
            simpleCrawler.StartUrl = urlTextBox.Text;
            showListBox.Items.Clear();
            new Thread(simpleCrawler.Crawl).Start();

        }
    }
}
