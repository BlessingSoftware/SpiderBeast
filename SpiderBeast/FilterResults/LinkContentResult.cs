using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using HtmlAgilityPack;
using SpiderBeast.Uitlity;

namespace SpiderBeast.FilterResults
{
    public class LinkContentResult : FilterResult
    {
        public const string HrefAttr = "href";
        public const string SrcAttr = "src";
        private string attrName;

        public LinkContentResult() : this(null as HtmlNode) { }

        public LinkContentResult(HtmlNode node, string attr = HrefAttr) : base(node)
        {
            attrName = attr;
        }

        public override object GetResult()
        {
            //string url = targetNode.GetAttributeValue("href", "");
            //if (isRelativeUrl(url))
            //{
            //    url = targetNode.OwnerDocument.GetBaseUrl() + url;
            //}
            //return (new WebFileInfo(AbsolutePath).OpenRead());
            return AbsolutePath;
        }

        /// <summary>
        /// 获取链接的绝对路径
        /// </summary>
        public string AbsolutePath
        {
            get
            {
                string url = targetNode.GetAttributeValue(attrName, string.Empty);
                if (isRelativeUrl(url))
                {
                    url = targetNode.OwnerDocument.GetBaseUrl() + url;
                }
                return url;
            }
        }


        bool isRelativeUrl(string url)
        {
            return url.Contains("://");
        }
    }
}
