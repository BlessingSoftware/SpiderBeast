using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast;
using SpiderBeast.Fetchs;
using SpiderBeast.Rules;
using SpiderBeast.Base;
using SpiderBeast.FilterResults;

namespace SpiderBeast.Test
{

    /// <summary>
    /// 测试用程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://www.daomubiji.com/she-zhao-gui-cheng-xia-01.html";// ;"http://manhua.dmzj.com/"

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "1.txt");

            SingleHtmlFetch<LinkContentResult> sf = new SingleHtmlFetch<LinkContentResult>(url, path);
            TypeRule tr = new TypeRule("link");
            sf.AddFilter(new Filter(tr));
            sf.StartFetch();
            //Console.WriteLine(Uitlity.HtmlUitilty.GetStringByUrl(url));
            Console.WriteLine("Ready");
            Console.ReadKey();
        }
    }
}
