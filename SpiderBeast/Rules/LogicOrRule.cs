using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 逻辑或规则，当内部规则集合不全为假时成立。
    /// 如果内部规则数目为0，则默认返回不成立。
    /// </summary>
    class LogicOrRule : LogicRule
    {
        public LogicOrRule() : base() { }

        public LogicOrRule(Rule rule1, Rule rule2) : base(rule1, rule2) { }

        public override bool Verify(HtmlNodeNavigator targetNode)
        {
            foreach (Rule i in targetRules)
            {
                if (i.Verify(targetNode)) return true;
            }
            return false;
        }
    }
}
