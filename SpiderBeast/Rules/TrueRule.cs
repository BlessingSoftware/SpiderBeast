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
    /// 真规则，它总是返回真。
    /// </summary>
    public class TrueRule : Rule
    {
        public override int Priority
        {
            get
            {
                return (int)RulePriority.ConstanRulePriority;
            }
        }

        public override bool Verify(HtmlNode targetNode)
        {
            return true;
        }
    }
}
