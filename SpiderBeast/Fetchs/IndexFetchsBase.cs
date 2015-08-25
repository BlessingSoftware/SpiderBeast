using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;
using SpiderBeast.Documents;

namespace SpiderBeast.Fetchs
{
    public enum NodeFilterType
    {
        //Body,
        ByID,
        ByXPath
    }

    public class NodeRule
    {
        public const string BODY = "//Body";
        public static NodeRule Empty = new NodeRule();

        public NodeFilterType Type { get; set; }

        public string Key { get; set; }

        public NodeRule() : this(NodeFilterType.ByXPath, BODY) { }
        public NodeRule(NodeFilterType type, string key)
        {
            this.Type = type;
            this.Key = key;
        }
    }

    public abstract class IndexFetchsBase : Fetch
    {
        /// <summary>
        /// 目录的URL地址
        /// </summary>
        protected String menuURL;

        public HtmlDocument Document { get { return base.doc; } }

        public HtmlNode FetchParent { get; protected set; }

        public List<IHtmlBaseNode> Children { get; protected set; }

        public List<string> VolumeNames { get; protected set; }

        /// <summary>
        /// 筛选出的章节，若未调用StartFetch方法，返回值为null
        /// </summary>
        public List<Chapter> Chapters { get; protected set; }

        public NodeRule ParentRule { get; set; }

        public NodeRule ChildrenRule { get; set; }

        public IndexFetchsBase(string url) : base(url)
        {
            //this.ParentRule = new NodeRule(NodeFilterType.ByXPath, "//ul[@class='mulu_list']");
            //this.ChildrenRule = new NodeRule(NodeFilterType.ByXPath, ".//li/a[@href]");

            //this.Children = new List<HtmlNode>();
            this.menuURL = targetURL.Substring(0, targetURL.IndexOf('/', 8));
        }

        //TODO: 首先做好初始化函数，然后看一下 YBDSingleHtmlFetch 类的接口。
        //对于每一个地址，调用一个单页Fetch，并提供URL和StreamWriter，启动方法开始解析，也可以用Reload方法而不创建新对象
        //鉴于这个实现比较特殊，就暂时不用FilterResult 和 DataManager 了。
        public override void StartFetch()
        {
            //TODO 开始抓取
            base.GetHtmlDocuments();

            TryGetParent();

            this.Children = FetchParent.SelectNodes(ChildrenRule.Key);
            Chapters = new List<Chapter>(Children.Count);
            int i = 0;
            foreach (var item in Children)
            {
                Chapters.Add(new Chapter(Html2Text(item), menuURL + item.Attributes["href"].Value, i++));
            }
        }

        protected void TryGetParent()
        {
            switch (ParentRule.Type)
            {
                case NodeFilterType.ByID:
                    this.FetchParent = doc.GetElementbyId(ParentRule.Key);
                    break;
                case NodeFilterType.ByXPath:
                    this.FetchParent = doc.DocumentNode.SelectSingleNode(ParentRule.Key) as HtmlNode;
                    break;
            }
            if (FetchParent == null)
            {
                throw new Exception("Wrong ParentRule!");
            }
        }

        protected override void DataManagerCallBack(List<HtmlNode> results, int filterID)
        {
            //throw new NotImplementedException();
        }

        //重写FetchStart方法，不必理会此回掉
        protected override void FetchCallBack(HtmlNode node)
        {
            //throw new NotImplementedException();
        }

        protected override void initDataManger()
        {
            //throw new NotImplementedException();
        }

        static char[] s = new char[] { '\r', '\n','\t', ' ' };
        /// <summary>
        /// 获取节点中实际的文本
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string Html2Text(IHtmlBaseNode node)
        {
            string html = node.InnerText.Trim();
            if (html.Length > 0)
            {
                var arr = html.Split(s, StringSplitOptions.RemoveEmptyEntries);
                return HtmlEntity.DeEntitize(string.Join(" ",arr));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
