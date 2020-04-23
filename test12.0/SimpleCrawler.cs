using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace test12._0
{
    class SimpleCrawler
    {
        public Hashtable urls;
        private int count = 0;
        public event Action<string> PageDownloaded;


        //static void Main(string[] args)
        //{
        //    SimpleCrawler myCrawler = new SimpleCrawler();
        private string startUrl;
        //    //string try1 = "../archive/2010/11/02/1867406.html";
        public void Start()
        {
            
        }
        //    //Console.WriteLine(change(try1, startUrl));
        //    if (args.Length >= 1) startUrl = args[0];
        //    myCrawler.urls.Add(startUrl, false);//加入初始页面
        //    new Thread(() => myCrawler.Crawl(startUrl)).Start();
        //}
        public string StartUrl
        {
            get=>startUrl;
            set
            {
                StartUrl = value;
                urls = new Hashtable();
                urls.Add(value, false);
            }
        }
        public  void Crawl()
        {
            //Console.WriteLine("開始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url])
                        continue;
                    current = url;

                }

                if (current == null || count > 10) break;
                //Console.WriteLine("爬行" + current + "頁面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html);//解析,并加入新的链接
                //Console.WriteLine("爬行結束");
            }
        }

        public string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);

                //Console.WriteLine(url);
                string fileName = count.ToString();
                //Console.WriteLine(fileName);
                File.WriteAllText(fileName, html, Encoding.UTF8);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            //string strRef2 = @"cnblogs";
            string strRef3 = @"(html|HTML)+$";
            MatchCollection matches = new Regex(strRef).Matches(html);

            foreach (Match match in matches)
            {

                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                //strRef = change(strRef);

                //strRef = Path.GetFullPath(strRef);

                //Console.WriteLine(strRef);
                strRef = change(strRef);
                if (!Regex.IsMatch(strRef, strRef3) )//|| !Regex.IsMatch(strRef, strRef2
                {

                    continue;
                }



                //if (!Regex.IsMatch(strRef, strRef3))
                //    continue;
                //Console.WriteLine(strRef);

                if (strRef.Length == 0) continue;
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
        private string change(string str)
        {
            Regex regex;

            if (str.StartsWith("http://"))
            {
                regex = new Regex("http://");
                str = regex.Replace(str, "https://");
                return str;
            }
            else if (Regex.IsMatch(str, "^[a-zA-Z]") && (!Regex.IsMatch(str, "https://")))
            {
                str = startUrl + str;
                return str;
            }
            else if (Regex.IsMatch(str, "^/"))
            {

                str = startUrl.Substring(0, startUrl.Length - 1) + str;
                return str;
            }
            else if (Regex.IsMatch(str, "^../"))
            {
                regex = new Regex("^../");
                str = regex.Replace(str, "/");
                return startUrl.Substring(0, startUrl.Length - 1) + str;
            }
            return str;
        }
    }
}
