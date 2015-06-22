using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 相对关系规则。表示相对于此节点的节点是否满足指定规则。如果该节点不存在，则返回false。
    /// </summary>
    public abstract class RelativeRule : Rule
    {
        protected Rule rule;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r">要相对节点满足的条件。如要判断存在性，请传入True规则。</param>
        public RelativeRule(Rule r)
        {
            rule = r;
        }

        public override RulePriority Priority
        {
            get
            {
                return Uitlity.RulePriority.RelativeRulePriority;
            }
        }

        public override bool Verify(HtmlNode targetNode)
        {
            HtmlNode relativeNode = GetRelativeNode(targetNode);
            if (relativeNode == null) return false;
            return rule.Verify(relativeNode);
        }

        abstract protected HtmlNode GetRelativeNode(HtmlNode node);
    }
}
