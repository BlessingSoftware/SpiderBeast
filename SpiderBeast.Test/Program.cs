using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast;

namespace SpiderBeast.Test
{

    /// <summary>
    /// 测试用程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Uitlity.HtmlUitilty.GetStringByUrl("www.baidu.com"));
            Console.ReadKey();
        }
    }
}
