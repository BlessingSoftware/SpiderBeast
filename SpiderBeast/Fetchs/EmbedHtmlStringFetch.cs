using SpiderBeast.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.DataManagers;
using SpiderBeast.FilterResults;

namespace SpiderBeast.Fetchs
{
    /// <summary>
    /// 
    /// </summary>
    public class EmbedHtmlStringFetch : Fetch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">要解析的URL地址</param>
        /// <param name="path">要写入的文件地址</param>
        public EmbedHtmlStringFetch(string url, string path) : base(url)
        {
            SetupDataManager(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmldoc">要解析的HTML文档</param>
        /// <param name="path">要写入的文件地址</param>
        public EmbedHtmlStringFetch(HtmlDocument htmldoc, string path) : base(htmldoc)
        {
            SetupDataManager(path);
        }

        private void SetupDataManager(string path)
        {
            EmbedHtmlStringDataManager dm = new EmbedHtmlStringDataManager(path);
            FetchStartEvent += dm.OnFetchStartHandler;
            FetchEndEvent += dm.OnFetchEndHandler;
            dataManagerPool.Add(dm);
        }


        protected override void DataManagerCallBack(List<HtmlNode> results, int filterID)
        {
            foreach (var i in results)
            {
                dataManagerPool[0].DataHandler(new LinkContentResult() { TargetNode = i });
            }
        }

        protected override void FetchCallBack(HtmlNode node)
        {
            foreach (Filter i in filterSet)
            {
                if (i is EmbedResoureFilter)
                {
                    foreach (var item in (i as EmbedResoureFilter).Filt(this.doc))
                    {
                        dataManagerPool[0].DataHandler(new LinkContentResult() { TargetNode = item as HtmlNode });
                    }
                }
                //if (i.FiltAsNode(node))
                //{
                //    dataManagerPool[0].DataHandler(new LinkContentResult() { TargetNode = node });
                //}
            }
        }

        protected override void initDataManger()
        {
        }

        public override void StartFetch()
        {
            switch (mFetchOder)
            {
                case FetchOrder.OriginHtmlOrder:
                    initDataManger();
                    foreach (Filter i in filterSet)
                    {
                        if (i is EmbedResoureFilter)
                        {
                            foreach (var item in (i as EmbedResoureFilter).Filt(this.doc))
                            {
                                dataManagerPool[0].DataHandler(new LinkContentResult(item as HtmlNode,"src"));
                            }
                        }
                    }
                    break;

                case FetchOrder.FilterOrder:
                    for (int i = 0; i < filterSet.Count; i++)
                    {
                        DataManagerCallBack(filterSet[i].FiltAsRoot(doc.DocumentNode), i);
                    }
                    break;
            }
        }
    }
}
