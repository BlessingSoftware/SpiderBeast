using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpiderBeast.Fetchs;
using System.IO;
using System.Xml;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace SpiderBeast.UnitTest
{
    [TestClass]
    public class UnitTestLinovel
    {
        [TestMethod]
        public void Test_ScriptIndexFetch()
        {
            //TestGetChapters(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\5515.html");
            String script = "<ScriptIndexFetch><ParentRule type=\"ByXPath\" key=\"//ul[@class=&quot;lk-chapter-list unstyled pt-10 pb-10 mt-20&quot;]\"></ParentRule><ChildrenRule type=\"ByXPath\" key=\"child::li/a\" ></ChildrenRule></ScriptIndexFetch>";
            ScriptIndexFetch sif = new ScriptIndexFetch("http://linovel.com/main/book/5515.html",
                script);

            //HtmlNode.ElementsFlags["img"] = HtmlElementFlag.Closed;
            //HtmlNode.ElementsFlags["meta"] = HtmlElementFlag.Closed;
            //HtmlNode.ElementsFlags["link"] = HtmlElementFlag.Closed;
            sif.StartFetch();
            
            foreach (var item in sif.Chapters)
            {
                System.Diagnostics.Debug.WriteLine(item.Href);
                System.Diagnostics.Debug.WriteLine(item.Name);
                System.Diagnostics.Debug.WriteLine(item.VolumeIndex);
            }
        }

        void TestGetChapters(string fileNmae)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            var str = File.ReadAllText(fileNmae, System.Text.Encoding.UTF8);
            doc.LoadHtml(str);
            var node = doc.DocumentNode.SelectSingleNode("//ul[@class=\"lk-chapter-list unstyled pt-10 pb-10 mt-20\"]");
            foreach (var element in (node as HtmlNode).SelectNodes("child::li/a"))
            {
                System.Diagnostics.Debug.WriteLine((element as HtmlNode).OuterHtml);
                TestTheHtml(element.Attributes["href"].Value);
                foreach (var tn in (element as HtmlNode).Descendants("#text"))
                {
                    System.Diagnostics.Debug.WriteLine(HtmlEntity.DeEntitize(tn.InnerText.Trim()));
                }
                //				System.Diagnostics.Debug.WriteLine(HtmlEntity.DeEntitize(element.InnerText.Trim()));
            }
        }

        void TestTheHtml(string fileNmae)
        {
            var doc = new HtmlWeb().Load(fileNmae);// new HtmlAgilityPack.HtmlDocument();
            //var str = File.ReadAllText(fileNmae, System.Text.Encoding.UTF8);
            //doc.LoadHtml(str);
            var jw = doc.GetElementbyId("J_view");

            //创建一个新的xhtml实例
            var xhtml = new HtmlAgilityPack.HtmlDocument();
            //			xhtml.OptionAutoCloseOnEnd = true;

            xhtml.DocumentNode.AppendChild(xhtml.CreateTextNode("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">"));
            var html = xhtml.CreateElement("html");
            html.SetAttributeValue("xmlns", "http://www.w3.org/1999/xhtml");
            xhtml.DocumentNode.AppendChild(html);
            var head = xhtml.CreateElement("head");
            html.AppendChild(head);
            //添加charset
            var charset = xhtml.CreateElement("meta");
            charset.SetAttributeValue("http-equiv", "Content-Type");
            charset.SetAttributeValue("content", "text/html; charset=utf-8");
            head.AppendChild(charset);
            //添加css
            var stylesheet = xhtml.CreateElement("link");
            stylesheet.SetAttributeValue("rel", "stylesheet");
            stylesheet.SetAttributeValue("type", "text/css");
            stylesheet.SetAttributeValue("href", "stylesheet.css");
            head.AppendChild(stylesheet);

            var body = xhtml.CreateElement("body");
            html.AppendChild(body);

            HtmlNode p = null;
            HtmlNode imgNode = null;

            List<HtmlNode> lstImg = new List<HtmlNode>();

            foreach (var element in jw.SelectNodes("./div[@id and @class=\"lk-view-line\"]"))
            {
                imgNode = (element as HtmlNode).Element("div");
                if (imgNode != null && imgNode.GetAttributeValue("class", string.Empty) == "lk-view-img")
                {
                    p = xhtml.CreateElement("p");
                    var timg = imgNode.Element("a").Element("img");
                    lstImg.Add(timg);
                    p.AppendChild(timg);
                    body.AppendChild(p);
                }
                else
                {
                    p = xhtml.CreateElement("p");
                    p.AppendChild(xhtml.CreateTextNode(element.InnerText));
                    body.AppendChild(p);
                }

            }

            string src = string.Empty;
            foreach (var element in lstImg)
            {
                src = "http://linovel.com" + element.GetAttributeValue("data-cover", string.Empty);
                element.Attributes.RemoveAll();
                element.SetAttributeValue("src", src);
            }

            fileNmae = "001" + ".xhtml";
            xhtml.Save(fileNmae, System.Text.Encoding.UTF8);
        }
    }
}
