using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast;
using SpiderBeast.Fetchs;
using SpiderBeast.Rules;
using SpiderBeast.Base;

namespace SpiderBeast.Test
{

    /// <summary>
    /// 测试用程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.daomubiji.com/she-zhao-gui-cheng-xia-01.html";

            string path = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "1.txt");

            SingleHtmlFetch sf = new SingleHtmlFetch(url, path);
            TypeRule tr = new TypeRule("p");
            sf.AddFilter(new Filter(tr));
            sf.StartFetch();
            //Console.WriteLine(Uitlity.HtmlUitilty.GetStringByUrl("www.baidu.com"));
            Console.ReadKey();
        }
    }
}
