using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 筛选器的基础抽象类。筛选器可以遍历整个HTML文档，筛选出符合规则集合（模式）的节点，将目标内容提取出来。
    /// </summary>
    abstract class Filter
    {
        /// <summary>
        /// 筛选器的规则集合，满足所有的规则即会被筛选出来。
        /// </summary>
        List<Rule> mRuleSet;


        /// <summary>
        /// 用于根据规则遍历全部子节点，并将结果返回。
        /// </summary>
        /// <param name="htmlDOM">筛选范围的根节点，会遍历此节点的所有子节点。一般可以考虑将body节点作为根节点传入。</param>
        /// <returns>筛选后的结果集合</returns>
        public virtual List<FilterResult> Filt(HtmlAgilityPack.HtmlNodeNavigator htmlDOM) {
            //TODO: 完成筛选器的Filt虚方法的基本实现
            
            return null;
        }
    }
}
