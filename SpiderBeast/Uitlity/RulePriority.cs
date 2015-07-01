using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Uitlity
{
    /// <summary>
    /// 设定筛选规则优先级参数基础值。数字越小，优先级越高。注意，实际值可能是多层次累加值。
    /// </summary>
    public enum RulePriority : int
    {
        ConstanRulePriority = 0,
        TypeRulePriority = 1,
        AttributeRulePriority = 1,
        LogicRulePriority = 2,
        RelativeRulePriority = 3,
    }
}
