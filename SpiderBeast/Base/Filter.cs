using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 筛选器的基础抽象类。筛选器可以遍历整个HTML文档，筛选出符合规则集合（模式）的节点，将目标内容提取出来。
    /// </summary>
    public class Filter
    {
        public Filter()
        {
        }

        public Filter(Rule r)
        {
            rule = r;
        }

        /// <summary>
        /// 筛选器的规则集合，满足所有的规则即会被筛选出来。
        /// </summary>
        Rule rule;

        List<HtmlNode> results = new List<HtmlNode>();

        protected FilterResultDelegate mGetFilterResult;

        /// <summary>
        /// 根据Filter的需求将Node封装成FilterResult。
        /// </summary>
        /// <param name="node">待封装的Node</param>
        /// <returns></returns>
        public delegate FilterResult FilterResultDelegate(HtmlNode node);

        //以下两个方法适用于不同的嵌套顺序，考虑到一个Fetch中可能有多个Filter在过滤不同的数据，所以提供两种方式
        //例如，同一个Fetch，针对同一个网页，需要拉取正文内容，和下一页的链接。应该应用两个Filter。所以有两种方式来实现
        //遍历中嵌套筛选(FiltAsNode)、筛选中嵌套遍历(FiltAsRoot)，得到的结果分别是按原顺序，和按分类整理顺序。

        /// <summary>
        /// 用于根据规则遍历全部子节点，并将结果返回。
        /// </summary>
        /// <param name="node">筛选范围的根节点，会遍历此节点的所有子节点。一般可以考虑将body节点作为根节点传入。</param>
        /// <returns>筛选后的结果集合</returns>
        public virtual List<HtmlNode> FiltAsRoot(HtmlNode node)
        {
            //DONE: 完成筛选器的FiltAsRoot虚方法的基本实现
            results.Clear();
            HtmlRecurver r = new HtmlRecurver(node, NodeFoundHandler);
            r.Recure();
            return results;
        }

        void NodeFoundHandler(HtmlNode node)
        {
            if (rule.Verify(node)) results.Add(node);
        }

        /// <summary>
        /// 用规则检验单个节点，返回布尔值表示是否满足。
        /// </summary>
        /// <param name="htmlDOM"></param>
        /// <returns></returns>
        public virtual bool FiltAsNode(HtmlNode node)
        {
            //DONE: 完成筛选器的FiltAsNode虚方法的基本实现
            return rule.Verify(node);
        }
    }
}
