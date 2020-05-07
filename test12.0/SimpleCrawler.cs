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
        private Hashtable urls = new Hashtable();
        private int count = 0;
        public event Action<SimpleCrawler> CrawlerStopped;
        public event Action<SimpleCrawler, string> PageDownloaded;
        private Queue<string> pending = new Queue<string>();
        private readonly string urlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
        public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
        //static void Main(string[] args)
        //{

        public string HostFilter { get; set; }
        public string FileFilter { get; set; }
        public int MaxPage { get; set; }
        public string StartURL { get; set; }
        public Encoding HtmlEncoding { get; set; }
        public Hashtable DownloadedPages { get => urls; }

        //    SimpleCrawler myCrawler = new SimpleCrawler();
        public SimpleCrawler()
        {
            MaxPage = 100;
            HtmlEncoding = Encoding.UTF8;
        }
        //    //string try1 = "../archive/2010/11/02/1867406.html";

        //    //Console.WriteLine(change(try1, startUrl));
        //    if (args.Length >= 1) startUrl = args[0];
        //    myCrawler.urls.Add(startUrl, false);//加入初始页面
        //    new Thread(() => myCrawler.Crawl(startUrl)).Start();
        //}

      
            
            //Console.WriteLine("開始爬行了.... ");
            //List<Task> tasks = new List<Task>();
            //while (tasks.Count < MaxPage && pending.Count > 0)
            //{
            //    string url = pending.Dequeue();
            //    Task task = Task.Run(() => Start(url));
            //    tasks.Add(task);
            //}
            ////Task.WaitAll(tasks.ToArray());
            //CrawlerStopped(this);

           
        public void Start()
        {
            urls.Clear();
            pending.Clear();
            pending.Enqueue(StartURL);
            count = 0;
            List<Task> tasks = new List<Task>();
            while (urls.Count < MaxPage && pending.Count > 0)
            {
                string url = pending.Dequeue();
                try
                {
                    Task<String> task = Task.Run(()=>DownLoad(url));
                    string html = task.Result;
                    urls[url] = true;
                    PageDownloaded(this, url);
                    Parse(html, url);//解析,并加入新的链接
                    tasks.Add(task);
                }
                catch (Exception ex)
                {
                    PageDownloaded(this, url);
                }
                Task.WaitAll(tasks.ToArray());
                CrawlerStopped(this);
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
                string fileName = urls.Count.ToString();
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

        private void Parse(string html, string pageUrl)
        {
            var matches = new Regex(urlDetectRegex).Matches(html);


            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "") continue;
                linkUrl = Change(linkUrl, pageUrl);

                //strRef = change(strRef);

                //strRef = Path.GetFullPath(strRef);

                //Console.WriteLine(strRef);
                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (file == "") file = "index.html";

                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
                  && !urls.ContainsKey(linkUrl))
                {
                    pending.Enqueue(linkUrl);
                    urls.Add(linkUrl, false);
                }
            }
        }
        static private string Change(string str, string baseUrl)
        {
            if (str.Contains("://"))
            {
                return str;
            }
            if (str.StartsWith("//"))
            {
                return "http:" + str;
            }
            if (str.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + str.Substring(1) : site + str;
            }

            if (str.StartsWith("../"))
            {
                str =str.Substring(3);
                int idx = baseUrl.LastIndexOf('/');
                return Change(str, baseUrl.Substring(0, idx));
            }

            if (str.StartsWith("./"))
            {
                return Change(str.Substring(2), baseUrl);
            }

            int end = baseUrl.LastIndexOf("/");
            return baseUrl.Substring(0, end) + "/" + str;
        }




        //if (!Regex.IsMatch(strRef, strRef3))
        //    continue;
        //Console.WriteLine(strRef);

    }
    
}   




