using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 相对兄弟节点规则。当下一个兄弟节点存在且满足条件时成立。如果要判断下一个兄弟节点是否存在，请将条件设为 TrueRule。
    /// </summary>
    class RelativeNextSiblingRule : RelativeRule
    {
        public RelativeNextSiblingRule(Base.Rule r) : base(r) { }

        protected override HtmlNode GetRelativeNode(HtmlNode node)
        {
            return node.NextSibling;
        }
    }
}
