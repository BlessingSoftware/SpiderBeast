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

            YBDUitlity();

            //TestMenuList("http://www.ybdu.com/xiaoshuo/0/910/");

            //Console.WriteLine("Ready");
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

        /// <summary>
        /// 这是一本读全本小说网的下载实用程序的入口
        /// </summary>
        static void YBDUitlity()
        {
            Console.WriteLine("欢迎使用！");
            Console.Write("请输入目录链接:");
            var url = Console.ReadLine();
            Console.WriteLine("小说将被保存在 我的文档 中，请指定文件名称，无需后缀。");
            Console.Write("名称：");
            var filename = Console.ReadLine();

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ensurePath(filename) + ".txt");
            YBDMenuListFetch ybdM = new YBDMenuListFetch(url);
            ybdM.StartFetch();

            if(ybdM.Chapters.Count == 0)
            {
                Console.WriteLine("没有找到任何章节，这中间一定有什么误会！任意键退出。");
                return;
            }
            int id = 0;
            while (true)
            {
                Console.Write("共有" + ybdM.Chapters.Count + "个章节，请输入起始章节ID：");
                id = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine("您选择的章节名称为：" + ybdM.Chapters[id].Name);
                Console.Write("确认吗? y/n");
                var key = Console.ReadLine();
                if (key == "y") break;
            }
            StreamWriter sw = new StreamWriter(path, false);
            YBDSingleHtmlFetch fetch = new YBDSingleHtmlFetch(url, sw);
            for (int i = id; i < ybdM.Chapters.Count; i++)
            {
                fetch.Reload(ybdM.Chapters[i].Href);
                fetch.StartFetch();
                Console.WriteLine("下载完成：" + ybdM.Chapters[i].Name);
            }
            sw.Close();
            Console.WriteLine("都下完啦，累死了！任意键退出啦！");
        }
    }
}
