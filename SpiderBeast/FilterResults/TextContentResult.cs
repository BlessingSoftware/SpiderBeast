using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using HtmlAgilityPack;

namespace SpiderBeast.FilterResults
{
    /// <summary>
    /// 文本类解析结果。返回指定Node Tag之间的文本。
    /// </summary>
    public class TextContentResult : FilterResult
    {
        public TextContentResult(HtmlNode node) : base(node) { }


        public override object GetResult()
        {
            return targetNode.InnerText;
        }
    }
}
