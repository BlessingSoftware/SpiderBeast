using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 否定逻辑规则，如果子规则成立，则本规则不成立。
    /// 如果没有内部有多个子规则，则只有第一个子规则有效。
    /// 如果内部没有子规则，则默认返回成立。
    /// </summary>
    public class LogicNotRule : LogicRule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rule">待否定的规则</param>
        public LogicNotRule(Rule rule) : base()
        {
            targetRules.Add(rule);
        }

        /// <summary>
        /// 更改内部的子规则
        /// </summary>
        /// <param name="rule">待否定的子规则</param>
        public void ModifyRule(Rule rule)
        {
            targetRules.Clear();
            targetRules.Add(rule);
        }

        public override bool Verify(HtmlNode targetNode)
        {
            if (targetRules.Count() == 0) return true;
            return !targetRules[0].Verify(targetNode);
        }
    }
}
