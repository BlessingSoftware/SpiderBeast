using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// 设定筛选规则优先级参数。数字越小，优先级越高。
    /// </summary>
    public enum RulePriority : int
    {
        AttributeRulePriority = 1,
        RelativeRulePriority = 3,
        LogicRulePriority = 5,
    }
}
