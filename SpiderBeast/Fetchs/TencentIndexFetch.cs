using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Fetchs
{
    public class TencentIndexFetch : IndexFetchsBase
    {
        public TencentIndexFetch(string url) : base(url)
        {
            this.ParentRule = new NodeRule(NodeFilterType.ByXPath, "//ol[@class='chapter-page-all works-chapter-list']");
            this.ChildrenRule = new NodeRule(NodeFilterType.ByXPath, ".//a[@href]");
        }
    }
}
