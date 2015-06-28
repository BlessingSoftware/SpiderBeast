using SpiderBeast.Uitlity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 规则抽象类，代表一种单一的节点模式，提供验证规则的方法
    /// </summary>
    public abstract class Rule : IComparable
    {
        /// <summary>
        /// 表示规则的优先级，数字越低，优先级越高。用于优化性能，原则上越简单的规则优先级越高。基类重写访问器来设定其固定值。
        /// </summary>
        public abstract int Priority { get; }

        public int CompareTo(object obj)
        {
            Rule ruleB = obj as Rule;
            if (ruleB == null)
                return 0;
            //if (Priority > ruleB.Priority) return 1;
            //if (Priority == ruleB.Priority) return 0;
            //return -1;
            return this.CompareTo(ruleB);
        }

        /// <summary>
        /// 比较两个Rule对象的优先级
        /// </summary>
        /// <param name="rul"></param>
        /// <returns></returns>
        public int CompareTo(Rule rul)
        {
            return Priority.CompareTo(rul.Priority);
        }

        /// <summary>
        /// 规则类的检验方法，验证参数节点是否满足设定的规则
        /// </summary>
        /// <param name="targetNode">待验证的的节点</param>
        /// <returns>返回是否满足规则的布尔值</returns>
        public abstract bool Verify(HtmlNode targetNode);
    }
}
