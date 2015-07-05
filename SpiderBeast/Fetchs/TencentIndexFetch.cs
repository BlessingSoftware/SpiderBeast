using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Fetchs
{
    public class TencentIndexFetch : IndexFetchsBase
    {
        public TencentIndexFetch(string url) : base(url)
        {
            this.ParentRule = new NodeRule(NodeFilterType.ByXPath, "//ol[@class='chapter-page-all works-chapter-list']");//番外 chapter-page-all works-chapter-list
            this.ChildrenRule = new NodeRule(NodeFilterType.ByXPath, ".//a[@href]");
        }

        public string Message
        {
            get
            {
                HtmlAgilityPack.IHtmlBaseNode t = null;
                if ((t = base.Document.DocumentNode.SelectSingleNode("//ul[@class='works-chapter-log ui-left']")) != null)
                {
                    return base.Document.GetTitle() + "\r\n" + string.Format("{0}共有{1}话", t.InnerText.Trim(), Chapters.Count);
                }
                return base.Document.GetTitle() + "\r\n" + string.Format("共有{0}话", Chapters.Count);
            }
        }
    }
}
