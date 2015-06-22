using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Uitlity;
using System.Net;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 是一次执行的基本抽象。包含Html信息。
    /// 内部有多个Filter，根据子类的需求，可以有若干DataManager。Fetch类应该包含如何处理数据的信息。提供的Fetch类实现是作为经典的基础场景。需要更高级的应用可以继承此类并改写方法。
    /// </summary>
    public abstract class Fetch
    {
        /// <summary>
        /// 拉取开始事件。在开始分析时应该触发。一般而言用于通知所有DataManager做好准备工作。
        /// </summary>
        public event Notification FetchStartEvent;

        /// <summary>
        /// 拉取结束事件。在结束分析时应该触发。一般而言用于通知DM做好结束清理工作
        /// </summary>
        public event Notification FetchEndEvent;

        /// <summary>
        /// 表示是否已经加载URL。
        /// </summary>
        bool hasLoaded = false;

        /// <summary>
        /// 目标URL。
        /// </summary>
        string targetURL;

        /// <summary>
        /// 筛选器集合。
        /// </summary>
        protected List<Filter> filterSet = new List<Filter>();

        /// <summary>
        /// 数据处理器集合。
        /// </summary>
        protected List<DataManager> dataManagerPool = new List<DataManager>();

        /// <summary>
        /// 代表整个Html文档。采用Lazy加载方式，在用到的时候才进行加载，并且储存，第二次调用不重新联网获取。
        /// </summary>
        protected HtmlDocument doc;

        private FetchOrder mFetchOder = FetchOrder.OriginHtmlOrder;

        public enum FetchOrder : int
        {
            OriginHtmlOrder = 0,
            FilterOrder = 1
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="URL">要解析的URL地址</param>
        public Fetch(string URL)
        {
            targetURL = URL;
            doc = GetHtmlDocuments();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="htmlDoc">要解析的Html文档</param>
        public Fetch(HtmlDocument htmlDoc)
        {
            doc = htmlDoc;
            hasLoaded = true;
            targetURL = null;
        }

        public FetchOrder StartFetchOrder
        {
            get { return mFetchOder; }
            set { mFetchOder = value; }
        }

        /// <summary>
        /// 获取内部的Html文档，用于实现Lazy获取方式。
        /// </summary>
        /// <returns></returns>
        public HtmlDocument GetHtmlDocuments()
        {
            if (!hasLoaded)
                RefreshHtml();
            return doc;
        }

        /// <summary>
        /// 刷新URL。
        /// </summary>
        public void RefreshHtml()
        {
            if(targetURL != null)
            {
                HtmlWeb web = new HtmlWeb();
                //为 HtmlWeb 添加对gzip压缩的网页的支持，以及使用Cookie伪装
                web.PreRequest +=HtmlUitilty.SetRequestHandler;

                doc =web.Load(targetURL);// HtmlUitilty.GetDocumentByUrl(targetURL);// 
            }
        }

        /// <summary>
        /// 添加筛选器。
        /// </summary>
        /// <param name="f">要添加的筛选器。</param>
        public void AddFilter(Filter f)
        {
            filterSet.Add(f);
        }

        /// <summary>
        /// 清空所有筛选器。
        /// </summary>
        public void ClearFilter()
        {
            filterSet.Clear();
        }

        /// <summary>
        /// 返回筛选器数量。
        /// </summary>
        /// <returns>筛选器数量。</returns>
        public int FilterCount()
        {
            return filterSet.Count();
        }

        /// <summary>
        /// 开始解析Html文档。
        /// </summary>
        public void StartFetch()
        {
            switch (mFetchOder)
            {
                case FetchOrder.OriginHtmlOrder:
                    initDataManger();
                    FetchStartEvent();

                    HtmlNode node = doc.DocumentNode;
                    HtmlRecurver recure = new HtmlRecurver(node, FetchCallBack);
                    recure.Recure();

                    FetchEndEvent();
                    break;

                case FetchOrder.FilterOrder:
                    for(int i = 0; i < filterSet.Count; i ++)
                    {
                        DataManagerCallBack(filterSet[i].FiltAsRoot(doc.DocumentNode), i);
                    }
                    break;
            }
        }

        abstract protected void DataManagerCallBack(List<HtmlNode> results, int filterID);

        /// <summary>
        /// 回调函数，用于让子类实现多态性。基类会递归的将每一个Node作为参数调用此回调函数。
        /// </summary>
        /// <param name="node">HTML文档中的待检查参数</param>
        abstract protected void FetchCallBack(HtmlNode node);

        /// <summary>
        /// 初始化DataManager的调用函数。注意每次解析前都会调用此函数。重写此函数以定制高级的输出规则。
        /// </summary>
        abstract protected void initDataManger();
    }
}
