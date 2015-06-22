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
        public LinkContentResult() { }

        public LinkContentResult(HtmlNode node) : base(node) { }

        public override object GetResult()
        {
            string url = targetNode.GetAttributeValue("href", "");
            if (isRelativeUrl(url))
            {
                url = targetNode.OwnerDocument.BaseUrl() + url;
            }
            return (object)url;
        }

        bool isRelativeUrl(string url)
        {
            return url.Contains("://");
        }
    }
}
