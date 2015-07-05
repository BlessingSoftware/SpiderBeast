using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;

namespace SpiderBeast.Fetchs
{

    /// <summary>
    /// 一本读全本小说网"http://www.ybdu.com/"的目录抓取类
    /// </summary>
    public class YBDMenuListFetch : IndexFetchsBase
    {


        public YBDMenuListFetch(string url) : base(url)
        {
            this.ParentRule = new NodeRule(NodeFilterType.ByXPath, "//ul[@class='mulu_list']");
            this.ChildrenRule = new NodeRule(NodeFilterType.ByXPath, ".//li/a[@href]");
        }

    }

}
