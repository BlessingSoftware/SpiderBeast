using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO.Compression;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// 提供静态的下载文件的方法
    /// </summary>
    public static class DownLoadUitlity
    {
        public const int BUFFER_SIZE = 0x8000;
        /// <summary>
        /// 下载指定的文件，并保存在本地
        /// </summary>
        /// <param name="wfi"></param>
        /// <param name="saveFileName">文件名</param>
        /// <param name="folder">保存的目录</param>
        public static void DownLoadFile(WebFileInfo wfi, string saveFileName, string folder)
        {
            var stream = OpenRead(wfi);
            DirectoryInfo dir = new DirectoryInfo(folder);
            if (!dir.Exists)
            {
                dir.Create();
            }
            try
            {
                using (var outStream = File.Create(Path.Combine(folder, saveFileName), BUFFER_SIZE))
                {
                    byte[] buff = new byte[BUFFER_SIZE];
                    int k = BUFFER_SIZE;
                    while (k > 0)
                    {
                        k = stream.Read(buff, 0, BUFFER_SIZE);
                        outStream.Write(buff, 0, k);
                    }
                }

            }
            finally
            {
                stream.Close();

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wfi"></param>
        /// <returns></returns>
        public static Stream OpenRead(this WebFileInfo wfi)
        {
            var req = HtmlUitilty.GetRequestByUrl(wfi.Href);
            if (req is HttpWebRequest)
            {
                (req as HttpWebRequest).Referer = wfi.Referer;
            }
            try
            {
                WebResponse resp = req.GetResponse();

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
                return stream;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else
                Console.WriteLine("Error\r\nMessage: {0}\r\nStackTrace: {1}\r\nSource: {2}\r\n", ex.Message,ex.StackTrace,ex.Source);
                
#endif
            }
        }

        public static string OpenReadString(this WebFileInfo wfi)
        {
            //var stram = OpenRead(wfi);
            return OpenReadString(wfi,Encoding.Default);
        }
        public static string OpenReadString(this WebFileInfo wfi,Encoding e)
        {
            var stram = OpenRead(wfi);
            StreamReader reader = new StreamReader(stram, e);            
            return reader.ReadToEnd();
        }
    }
}
