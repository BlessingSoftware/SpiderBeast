using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using Jint;
using LitJson;
using SpiderBeast.Documents;

namespace SpiderBeast.Fetchs
{
    public class TencentComicFetchs : Base.Fetch
    {
        public TencentComicFetchs(string url) : base(url)
        {

            Chapter = new CmoicChapter();
        }
        /// <summary>
        /// 漫画中的图片
        /// </summary>
        public CmoicChapter Chapter { get; protected set; }

        public override void StartFetch()
        {
            try
            {
                var doc = this.GetHtmlDocuments();
                Engine jsEngine = new Engine();//html/body/script[last()-3]
                var item = doc.DocumentNode.SelectSingleNode("html/body/script[@type='text/javascript' and not(@src)]");
                jsEngine.Execute(item.InnerText);
                var str = jsEngine.GetValue("DATA").AsString();
                int i = str.Length % 4;
                if (i != 0)               
                    str = str.Substring(i);
                
                var buff = Convert.FromBase64String(str);
                str = Encoding.Default.GetString(buff);
                //System.Diagnostics.Debug.WriteLine(str);
                var jsObj = JsonMapper.ToObject(str);
                Chapter.ChapterName = jsObj["chapter"]["cTitle"].ToString();
                Chapter.ComicName = jsObj["comic"]["title"].ToString();
                var pics = jsObj["picture"];
                JsonData pic;
                for (int j = 0; j < pics.Count; j++)
                {
                    pic = pics[j];
                    Chapter.UrlList.Add(pic["url"].ToString());
                    //System.Diagnostics.Debug.WriteLine(pic["url"]);
                }

            }
            catch
            {
                Chapter.UrlList.Clear();
            }
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
