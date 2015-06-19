using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SpiderBeast.Uitlity
{    
    /// <summary>
    /// 封装HTML文档的方法的静态类
    /// </summary>
    public static class HtmlUitilty
    {
        public const string HTTP_Protocol = "http://";
        /// <summary>
        /// 获取给定网址的HTML字符串
        /// </summary>
        /// <param name="url">网址,http协议可省略</param>
        /// <returns>HTML字符串</returns>
        public static string GetStringByUrl(string url)
        {
            if (!url.StartsWith(HTTP_Protocol, StringComparison.CurrentCultureIgnoreCase))
            {
                url = HTTP_Protocol + url;
            }
            WebRequest req = WebRequest.Create(url);

            //允许服务器发送压缩过的文件流
            req.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            req.Timeout = 100000;
            using (WebResponse resp = req.GetResponse())
            {
                var stream = resp.GetResponseStream();
                //判断流是否经过压缩
                switch (resp.Headers[HttpResponseHeader.ContentEncoding])
                {
                    case "gzip":
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                        break;
                    case "deflate":
                        stream = new DeflateStream(stream, CompressionMode.Decompress);
                        break;
                    default:
                        break;
                }
                var encoding = Encoding.GetEncoding((resp as HttpWebResponse).CharacterSet);

                var streamReader = new StreamReader(stream, encoding);
                return streamReader.ReadToEnd();
            }
        }
        /// <summary>
        /// 获取给定网址的HtmlDocument对象
        /// </summary>
        /// <param name="url">网址,http协议可省略</param>
        /// <returns>返回解析网页后得到的HtmlDocument对象</returns>
        public static HtmlDocument GetDocumentByUrl(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetStringByUrl(url));
            return doc;
        }


    }
}
