using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast
{
    /// <summary>
    /// 表示网络中文件的基础类
    /// </summary>
    public class WebFileInfo
    {
        /// <summary>
        /// 文件的链接地址
        /// </summary>
        protected string m_href;

        /// <summary>
        /// 文件的链接地址
        /// </summary>
        public string Href
        {
            get
            {
                if (m_baseUrl == null
                    || m_baseUrl.IndexOf("://") > 0)
                {
                    return m_href;
                }

                StringBuilder builder = new StringBuilder(m_baseUrl + "/");
                if (m_baseUrl.EndsWith("/"))
                {
                    builder.Remove(builder.Length - 1, 1);
                }
                var arr = m_href.Split('/', '\\');
                foreach (string str in arr)
                {
                    if (str == "..")
                    {
                        int i = builder.ToString().LastIndexOf('/');
                        builder.Remove(i, builder.Length - i);
                    }
                    else if (str == "." || String.IsNullOrEmpty(str))
                    {
                    }
                    else
                    {
                        builder.Append('/');
                        builder.Append(str);
                    }
                }
                m_href = builder.ToString();
                m_baseUrl = null;
                return m_href;
            }
            set
            {
                m_href = value;
            }
        }

        /// <summary>
        /// 链接的基准 URL
        /// </summary>
        protected string m_baseUrl;

        /// <summary>
        /// 链接的基准 URL，href为绝对路径时可为null
        /// </summary>
        public string BaseUrl { get { return m_baseUrl; } set { m_baseUrl = value; } }

        /// <summary>
        /// 此文件的引用页，为了对付某些网站的防盗链机制
        /// </summary>
        protected string m_refer;

        /// <summary>
        /// 此文件的引用页，为了对付某些网站的防盗链机制
        /// </summary>
        public string Referer { get { return m_refer; } set { m_refer = value; } }

        public WebFileInfo(string href) : this(href, null, null)
        {

        }
        public WebFileInfo(string href, string basurl) : this(href, basurl, null)
        {

        }
        /// <summary>
        /// 实例化WebFileInfo类
        /// </summary>
        /// <param name="href">文件的链接地址</param>
        /// <param name="basurl">链接的基准 URL，href为绝对路径时可为null</param>
        /// <param name="refer">此文件的引用页，即引用了此文件的网页的网址</param>
        public WebFileInfo(string href, string basurl, string refer)
        {
            m_href = href;
            m_baseUrl = basurl;
            m_refer = refer;
        }
    }
}
