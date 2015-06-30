using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SpiderBeast;
using SpiderBeast.Fetchs;
using SpiderBeast.Rules;
using SpiderBeast.Base;
using SpiderBeast.Uitlity;
using SpiderBeast.FilterResults;
using System.IO;
using HtmlAgilityPack;

namespace SpiderBeast.Test
{

    /// <summary>
    /// 测试用程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://read.qidian.com/BookReaderNew/3463725,84452150.aspx";// ;"http://manhua.dmzj.com/"
            /*
            http://files.qidian.com/Author6/3463725/84452150.txt
            
            */
            //string tmp = Path.GetTempFileName();

            //EmbedHtmlStringFetch ehsf = new EmbedHtmlStringFetch(url, tmp);
            //EmbedResoureFilter erf = new EmbedResoureFilter(FilterType.NodeID) { Start = "chaptercontent", Target = "script[@src]" };

            //ehsf.AddFilter(erf);
            //ehsf.StartFetch();
            //var filename = ehsf.GetHtmlDocuments().GetTitle();// = Regex.Match(url, ",(\\w*)\\.aspx").Groups[1].Value + ".txt";
            var filename = "ss";
            url = "http://www.snwx.com/book/32/32820/13435471.html";
            var ff=HtmlUitilty.GetStringByUrl(url);

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ensurePath(filename)+".txt");
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
            //File.Move(tmp, path);

            SingleHtmlFetch<TextContentResult> sf = new SingleHtmlFetch<TextContentResult>(url, path);
            //TypeRule tr = new TypeRule("link");
            sf.AddFilter(new Filter(new TypeRule("div")));
            var doc=sf.GetHtmlDocuments();
            sf.StartFetch();
            // Console.WriteLine(Uitlity.HtmlUitilty.GetStringByUrl(url));
            Console.WriteLine("Ready");
            Console.ReadKey();
        }

        static string ensurePath(string str)
        {
            List<char> tmp = new List<char>();
            tmp.AddRange(str.ToCharArray());
            var t = Path.GetInvalidFileNameChars();
            tmp.RemoveAll((c) =>
            {
                foreach (var item in t)
                {
                    if (c == item)
                    {
                        return true;
                    }
                }
                return false;
            });

            return new string(tmp.ToArray());
        }
    }
}
