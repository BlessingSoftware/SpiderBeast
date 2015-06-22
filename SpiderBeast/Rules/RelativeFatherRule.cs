using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 相对父节点规则。当父节点存在且满足条件时成立。如果要判断父节点是否存在，请将条件设为 TrueRule。
    /// </summary>
    public class RelativeFatherRule : RelativeRule
    {
        public RelativeFatherRule(Base.Rule r) : base(r) { }

        protected override HtmlNode GetRelativeNode(HtmlNode node)
        {
            return node.ParentNode;
        }
    }
}
