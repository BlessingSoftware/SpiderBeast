using SpiderBeast.Base;
using SpiderBeast.Uitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.IO;

namespace SpiderBeast.DataManagers
{
    public class EmbedHtmlStringDataManager : TextStreamDataManager
    {
        #region "ctor"
        /// <summary>
        /// 文本流数据处理器构造函数。默认覆盖原文件。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        public EmbedHtmlStringDataManager(string path) : base(path, false)
        {
        }

        /// <summary>
        /// 文本流处理器构造函数，使用UTF-8编码。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        /// <param name="append">指定追加模式，True为追加。</param>
        public EmbedHtmlStringDataManager(string path, bool append) : base(path, append, Encoding.UTF8)
        {
        }

        /// <summary>
        /// 文本流处理器构造函数,指定使用的字符编码。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        /// <param name="append">指定追加模式，True为追加。</param>
        /// <param name="encoding">要使用的字符编码。</param>
        public EmbedHtmlStringDataManager(string path, bool append, Encoding encoding) : base(path, append, encoding)
        {
        }

        #endregion

        /// <summary>
        /// 数据处理函数。接受传递进来的数据。只接受文本类，忽略其他。
        /// </summary>
        /// <param name="data"></param>
        public override void DataHandler(FilterResult data)
        {
            base.OnFetchStartHandler();
            string url = data.GetResult<string>();
            WebFileInfo wfi = new WebFileInfo(url);
            //text = wfi.OpenReadString(Encoding.GetEncoding("gb2312"));

            HtmlDocument doc = new HtmlDocument();
            //HtmlWeb hw = new HtmlWeb();
            doc.Load(wfi.OpenRead());
            //doc.LoadHtml(text);
            //foreach (var item in doc.)
            //{

            //}           
            string text = Regex.Replace(doc.DocumentNode.InnerHtml, "</?.*?>", "\r\n");

            int start, end;
            start = text.IndexOf('\'');
            end = text.LastIndexOf('\'');
            text = text.Substring(++start, end - start);
            w.WriteLine(text);

            base.OnFetchEndHandler();
        }
    }
}
