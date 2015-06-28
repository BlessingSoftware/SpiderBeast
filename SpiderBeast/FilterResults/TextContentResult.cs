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

        private void RemoveUnuseNode(string xpath)
        {
            var comments = targetNode.SelectNodes(xpath);
            if (comments == null)
                return;
       
                comments.ForEach((n) => { (n as HtmlNode).Remove(); });
                //foreach (HtmlNode item in comments)
                //{
                //    item.Remove();
                //}
          
        }

        public override object GetResult()
        {
            //移除HtmlNode中的注释
            RemoveUnuseNode(".//comment()");

            //移除脚本
            RemoveUnuseNode(".//script");

            return targetNode.InnerText;
        }

    }
}
