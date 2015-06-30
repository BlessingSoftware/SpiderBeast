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
            var url = "http://www.ybdu.com/xiaoshuo/0/910/4196398.html";
            var filename = "test";
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ensurePath(filename) + ".txt");
            StreamWriter sw = new StreamWriter(path, false);

            YBDSingleHtmlFetch y = new YBDSingleHtmlFetch(url, sw);
            y.StartFetch();
            sw.Close();
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
