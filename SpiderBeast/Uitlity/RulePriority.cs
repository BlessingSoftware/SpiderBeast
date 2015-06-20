using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// 设定筛选规则优先级参数。数字越小，优先级越高。
    /// </summary>
    class RulePriority
    {
        public const int LogicRulePriority = 5;
        public const int AttributeRulePriority = 1;
        public const int RelativeRulePriority = 3;
    }
}
