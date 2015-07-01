using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 相对子节点规则。当存在符合条件的子节点时成立，隐含或。如果要判断是否存在子节点，请将Rule设为True。
    /// </summary>
    public class RelativeSonRule : RelativeRule
    {
        public RelativeSonRule(Base.Rule r) : base(r) { }

        protected override HtmlNode GetRelativeNode(HtmlNode node)
        {
            if (!node.HasChildNodes) return null;
            return node.FirstChild;
        }

        public override bool Verify(HtmlNode targetNode)
        {
            if (!targetNode.HasChildNodes) return false;
            HtmlNode cur = targetNode.FirstChild;
            while(cur != null)
            {
                if (rule.Verify(cur)) return true;
                cur = cur.NextSibling;
            }
            return false;
        }
    }
}
