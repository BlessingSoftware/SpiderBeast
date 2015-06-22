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
        public TextContentResult() { }

        public TextContentResult(HtmlNode node) : base(node) { }


        public override object GetResult()
        {
            //移除HtmlNode中的注释
            var comments = targetNode.SelectNodes(".//comment()");
            if (comments != null)
            {
                foreach (HtmlNode item in comments)
                {
                    item.Remove();
                }
            }
            //移除脚本
            var script = targetNode.SelectNodes(".//script");
            if (script != null)
            {
                foreach (HtmlNode item in script)
                {
                    item.Remove();
                }
            }

            return targetNode.InnerText;
        }

    }
}
