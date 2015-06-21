using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 逻辑规则与，只有内部的所有条件均成立，本条件才成立。
    /// 如果内部规则数量为0，则默认返回成立。
    /// </summary>
    public class LogicAndRule : LogicRule
    {

        public LogicAndRule() : base() { }

        public LogicAndRule(Rule rule1, Rule rule2) : base(rule1, rule2) { }

        public override bool Verify(HtmlNode targetNode)
        {
            foreach (Rule i in targetRules)
            {
                if (!i.Verify(targetNode)) return false;
            } 
            return true;
        }
    }
}
