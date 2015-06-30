using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;
using System.IO;

namespace SpiderBeast.Fetchs
{
    class YBDSingleHtmlFetch : Fetch
    {

        StreamWriter swriter;

        /// <summary>
        /// 构造函数。如果使用此构造函数，请务必使用SetWriter方法给写入器赋值。
        /// </summary>
        /// <param name="url">要访问的URL</param>
        public YBDSingleHtmlFetch(string url) : base(url)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="sw">要写入的流</param>
        public YBDSingleHtmlFetch(string url, StreamWriter targetWriter) : base(url)
        {
            SetWriter(targetWriter);
        }

        /// <summary>
        /// 重设并刷新新的URL
        /// </summary>
        public void Reload(string url)
        {
            targetURL = url;
            RefreshHtml();
        }

        /// <summary>
        /// 设置要写入的流
        /// </summary>
        /// <param name="newWriter"></param>
        public void SetWriter(StreamWriter newWriter)
        {
            swriter = newWriter;
        }

        /// <summary>
        /// 开始解析当前HTML，并将内容写入流
        /// </summary>
        public override void StartFetch()
        {
            
        }

        protected override void DataManagerCallBack(List<HtmlNode> results, int filterID)
        {
            throw new NotImplementedException();
        }

        protected override void FetchCallBack(HtmlNode node)
        {
            throw new NotImplementedException();
        }

        protected override void initDataManger()
        {
            throw new NotImplementedException();
        }
    }
}
