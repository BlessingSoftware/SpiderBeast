using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using HtmlAgilityPack;
using SpiderBeast.DataManagers;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Fetchs
{
    /// <summary>
    /// 基本拉取类型，将所有Filter的结果以文本汇总到文件中。
    /// </summary>
    public class SingleHtmlFetch : Fetch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">要解析的URL地址</param>
        /// <param name="path">要写入的文件地址</param>
        public SingleHtmlFetch(string url, string path) : base(url) {
            SetupDataManager(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmldoc">要解析的HTML文档</param>
        /// <param name="path">要写入的文件地址</param>
        public SingleHtmlFetch(HtmlDocument htmldoc, string path) : base(htmldoc) {
            SetupDataManager(path);
        }

        private void SetupDataManager(string path)
        {
            TextStreamDataManager dm = new TextStreamDataManager(path);
            FetchStartEvent += dm.OnFetchStartHandler;
            FetchEndEvent += dm.OnFetchEndHandler;
            dataManagerPool.Add(dm);
        }

        protected override void FetchCallBack(HtmlNode node)
        {
            foreach (Filter i in filterSet)
            {
                if (i.FiltAsNode(node))
                {
                    dataManagerPool[0].DataHandler(new FilterResults.TextContentResult(node));
                }
            }
        }

        protected override void initDataManger()
        {

        }
    }
}
