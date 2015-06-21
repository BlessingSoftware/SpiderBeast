using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// Html递归器。用于递归遍历Html树的全部节点。
    /// </summary>
    public class HtmlRecurver
    {
        HtmlNode root;
        HtmlNodeDelegate mNodeFoundHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeRoot">要遍历的根节点</param>
        /// <param name="NodeFoundHandler">对每个节点调用的处理函数。</param>
        public HtmlRecurver(HtmlNode treeRoot, HtmlNodeDelegate NodeFoundHandler)
        {
            root = treeRoot;
            mNodeFoundHandler = NodeFoundHandler;
        }

        /// <summary>
        /// 开始递归。
        /// </summary>
        public void Recure()
        {
            mNodeFoundHandler(root);
            if (root.HasChildNodes)
            {
                HtmlNode cur = root.FirstChild;
                
                while(cur != null)
                {
                    HtmlRecurver recure = new HtmlRecurver(cur, mNodeFoundHandler);
                    recure.Recure();
                    recure = null;
                    cur = cur.NextSibling;
                }
            }
        }
    }
}
