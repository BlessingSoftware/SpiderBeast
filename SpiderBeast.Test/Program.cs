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
            TestMenuList("http://www.ybdu.com/xiaoshuo/0/910/");

            Console.WriteLine("Ready");
            Console.ReadKey();

            //var url = "http://www.ybdu.com/xiaoshuo/0/910/4196398.html";
            //var filename = "test";
            //string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ensurePath(filename) + ".txt");
            //StreamWriter sw = new StreamWriter(path, false);

            //YBDSingleHtmlFetch y = new YBDSingleHtmlFetch(url, sw);
            //y.StartFetch();
            //sw.Close();
        }

        static void TestMenuList(string url)
        {
            YBDMenuListFetch ybdM = new YBDMenuListFetch(url);
            ybdM.StartFetch();
            string mydoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            int t = ybdM.Chapters.Count;
            int c = 0;
            while (t > 0)
            {
                t /= 10;
                c++;
            }
            string tmp = new string('0', c);
            for (int i = 0; i < 10; i++)
            {
                string path = Path.Combine(mydoc, i.ToString(tmp) + ensurePath(ybdM.Chapters[i].Name) + ".txt");
                StreamWriter sw = new StreamWriter(path, false);

                YBDSingleHtmlFetch y = new YBDSingleHtmlFetch(ybdM.Chapters[i].Href, sw);
                y.StartFetch();
                sw.Close();

                Console.WriteLine(path);
            }
            Console.WriteLine(ybdM.Children.Count);
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
