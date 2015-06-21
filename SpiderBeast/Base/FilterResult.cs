using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 筛选结果基本抽象类。表示一个筛选器筛选出来的结果。筛选器结果的类型应该由筛选器本身来决定。
    /// </summary>
    public abstract class FilterResult
    {
        public FilterResult(HtmlNode node)
        {
            targetNode = node.Clone();
        }

        /// <summary>
        /// 目标节点的存档
        /// </summary>
        protected HtmlAgilityPack.HtmlNode targetNode;

        /// <summary>
        /// 获取目标节点的结果信息。
        /// </summary>
        /// <returns>返回通用类型的结果信息，根据不同子类的实现可能不同，下转型后使用。</returns>
        public abstract object GetResult();
    }
}
