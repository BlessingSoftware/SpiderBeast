using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// 封装HTML文档的方法的静态类
    /// </summary>
    public static class HtmlUitilty
    {
        #region "APIs"

        /// <summary>
        /// 设置cookie
        /// Creates a cookie associated with the specified URL.
        /// </summary>
        /// <param name="lpszUrlName">Pointer to a null-terminated string that specifies the URL for which the cookie should be set.</param>
        /// <param name="lbszCookieName">Pointer to a null-terminated string that specifies the name to be associated with the cookie data. If this parameter is NULL, no name is associated with the cookie.</param>
        /// <param name="lpszCookieData">Pointer to the actual data to be associated with the URL.</param>
        /// <returns>Returns TRUE if successful, or FALSE otherwise. To get a specific error message, call GetLastError.</returns>
        /// <remarks>Cookies created by InternetSetCookie without an expiration date are stored in memory and are available only in the same process that created them. Cookies that include an expiration date are stored in the windows\cookies directory.
        /// Creating a new cookie might cause a dialog box to appear on the screen asking the user if they want to allow or disallow cookies from this site based on the privacy settings for the user.
        /// Caution  InternetSetCookie will unconditionally create a cookie even if “Block all cookies” is set in Internet Explorer. This behavior can be viewed as a breach of privacy even though such cookies are not subsequently sent back to servers while the “Block all cookies” setting is active.Applications should use InternetSetCookieEx to correctly honor the user's privacy settings.
        /// For more cookie internals, see http://blogs.msdn.com/ieinternals/archive/2009/08/20/WinINET-IE-Cookie-Internals-FAQ.aspx.
        /// Like all other aspects of the WinINet API, this function cannot be safely called from within DllMain or the constructors and destructors of global objects.
        /// Note WinINet does not support server implementations.In addition, it should not be used from a service. For server implementations or services use Microsoft Windows HTTP Services (WinHTTP).
        /// </remarks>
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie([In] string lpszUrlName, [In] string lbszCookieName, [In] string lpszCookieData);

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="url">A pointer to a null-terminated string that specifies the URL for which cookies are to be retrieved.</param>
        /// <param name="name">Not implemented.</param>
        /// <param name="data">A pointer to a buffer that receives the cookie data. This parameter can be NULL.</param>
        /// <param name="dataSize">A pointer to a variable that specifies the size of the lpszCookieData parameter buffer, in TCHARs. If the function succeeds, the buffer receives the amount of data copied to the lpszCookieData buffer. If lpszCookieData is NULL, this parameter receives a value that specifies the size of the buffer necessary to copy all the cookie data, expressed as a byte count.</param>
        /// <returns>If the function succeeds, the function returns TRUE.</returns>
        /// <remarks>
        /// InternetGetCookie does not require a call to InternetOpen. InternetGetCookie checks in the windows\cookies directory for persistent cookies that have an expiration date set sometime in the future. InternetGetCookie also searches memory for any session cookies, that is, cookies that do not have an expiration date that were created in the same process by InternetSetCookie, because these cookies are not written to any files. Rules for creating cookie files are internal to the system and can change in the future.
        /// As noted in HTTP Cookies, InternetGetCookie does not return cookies that the server marked as non-scriptable with the "HttpOnly" attribute in the Set-Cookie header.
        /// Like all other aspects of the WinINet API, this function cannot be safely called from within DllMain or the constructors and destructors of global objects.
        /// Note WinINet does not support server implementations.In addition, it should not be used from a service. For server implementations or services use Microsoft Windows HTTP Services (WinHTTP).
        /// </remarks>
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetGetCookie([In] string url, [In] string name, [Out] StringBuilder data, [In, Out] ref int dataSize);

        #endregion
        /// <summary>
        /// Cookie的过期时间
        /// </summary>
        const string EXPIRES = ";expires=Sun,22-Feb-2099 00:00:00 GMT";
        /// <summary>
        /// Cookie的过期时间
        /// </summary>
        static readonly DateTime s_expires;

        static HtmlUitilty()
        {
            s_cookies = new CookieContainer();
            s_useIECookie = false;
            s_expires = new DateTime(2099, 2, 22);
        }

        #region "Properties"

        /// <summary>
        /// 网络请求所使用的Cookie的容器
        /// </summary>
        private static CookieContainer s_cookies;

        /// <summary>
        /// 获取HTTP网络请求所使用的CookieContainer对象
        /// </summary>
        public static CookieContainer Cookies
        {
            get { return s_cookies; }
        }

        /// <summary>
        /// 是否使用IE浏览器的Cookie
        /// </summary>
        private static bool s_useIECookie;

        /// <summary>
        /// 获取或设置是否使用IE浏览器的Cookie
        /// </summary>
        public static bool UseIECookie
        {
            get { return s_useIECookie; }
            set { s_useIECookie = value; }
        }

        #endregion

        /// <summary>
        /// 获取指定网址的域
        /// </summary>
        /// <param name="urlName"></param>
        /// <returns></returns>
        public static Uri GetDomainByUrl(string urlName)
        {
            int i = urlName.IndexOf("://") + 3;
            string protocol = urlName.Substring(0, i);
            int j = urlName.IndexOf("/", i);
            if (j < 0)
            {
                j = urlName.Length;
            }
            return new Uri(protocol + "");
        }
        /// <summary>
        /// 获取IE浏览器中指定网址的Cookie
        /// </summary>
        /// <param name="urlName"></param>
        /// <returns></returns>
        public static CookieCollection GetInternetCookie(string urlName)
        {
            CookieContainer cookies = new CookieContainer();
            //CookieCollection cc = new CookieCollection();
            StringBuilder cookie = new StringBuilder(2048);
            int datasize = cookie.Length;
            
            if (!InternetGetCookie(urlName, null, cookie, ref datasize))
            {
                cookie.EnsureCapacity(datasize);
                InternetGetCookie(urlName, null, cookie, ref datasize);
            }
            Uri uri = new Uri(urlName);
            //var s = GetDomainByUrl(urlName);
            //var s = uri.Authority;
            //foreach (string c in cookie.ToString().Split(new char[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    int i = c.IndexOf('=');
            //    string name = c.Substring(0, i);
            //    string value = c.Substring(i + 1);

            //    cc.Add((new Cookie(name, value, "/", s) { Expires = DateTime.Now.AddMonths(1) }));
            //}
            cookies.SetCookies(uri, cookie.ToString().Replace(';', ','));
            return cookies.GetCookies(uri);
        }

        /// <summary>
        /// 设置IE的Cookie
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="uri"></param>
        public static void SetInternetCookie(CookieCollection cc, Uri uri)
        {
            //uri.Host;
            foreach (Cookie c in cc)
            {
                InternetSetCookie(uri.GetLeftPart(System.UriPartial.Authority), c.Name, c.Value+";expires=Sun,22-Feb-2099 00:00:00 GMT");
            }
        }

        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
            System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        private static void SetCookies(CookieCollection cc, Uri uri)
        {
            SetInternetCookie(cc, uri);
        }

        private static void GetCookies(string urlName)
        {
            var cc = GetInternetCookie(urlName);
            s_cookies.Add(cc);
            //foreach (Cookie c in cc)
            //{

            //}
        }

        public const string HTTP_Protocol = "http://";
        /// <summary>
        /// 获取给定网址的HTML字符串
        /// </summary>
        /// <param name="url">网址,http协议可省略</param>
        /// <returns>HTML字符串</returns>
        public static string GetStringByUrl(string url)
        {
            if (url.IndexOf("://", StringComparison.CurrentCultureIgnoreCase) < 0)
            {
                url = HTTP_Protocol + url;
            }
            Uri myuri = new Uri(url);
            WebRequest req = WebRequest.Create(myuri);

            if (s_useIECookie && req is HttpWebRequest)
            {
                //TODO: 实现使用IE Cookie
                //var t = GetInternetCookie(url);
                //GetCookies(url);
                (req as HttpWebRequest).CookieContainer = s_cookies;
            }
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

                if (s_useIECookie && resp is HttpWebResponse)
                {
                    //(resp as HttpWebResponse).GetResponseHeader
                    SetCookies((resp as HttpWebResponse).Cookies, resp.ResponseUri);
                }
                //(resp as HttpWebResponse).Cookies
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