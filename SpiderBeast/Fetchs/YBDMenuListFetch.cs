using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;

namespace SpiderBeast.Fetchs
{

    /// <summary>
    /// 一本读全本小说网"http://www.ybdu.com/"的目录抓取类
    /// </summary>
    public class YBDMenuListFetch : Fetch
    {
        /// <summary>
        /// 目录的URL地址
        /// </summary>
        String menuURL; 

        public YBDMenuListFetch(string url) : base(url)
        {

        }

        //TODO: 首先做好初始化函数，然后看一下 YBDSingleHtmlFetch 类的接口。
        //对于每一个地址，调用一个单页Fetch，并提供URL和StreamWriter，启动方法开始解析，也可以用Reload方法而不创建新对象
        //鉴于这个实现比较特殊，就暂时不用FilterResult 和 DataManager 了。
        public override void StartFetch()
        {
            //TODO 开始抓取
        }
        
        protected override void DataManagerCallBack(List<HtmlNode> results, int filterID)
        {
            throw new NotImplementedException();
        }

        //重写FetchStart方法，不必理会此回掉
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
