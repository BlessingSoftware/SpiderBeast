using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 逻辑规则抽象类，用于处理规则之间的逻辑关系。内部含有子规则。
    /// </summary>
    abstract public class LogicRule : Base.Rule
    {
        protected List<Rule> targetRules;

        public LogicRule()
        {
            targetRules = new List<Rule>();
        }

        public LogicRule(Rule rule1, Rule rule2)
        {
            targetRules = new List<Rule>();
            targetRules.Add(rule1);
            targetRules.Add(rule2);
            targetRules.Sort();
        }

        /// <summary>
        /// 添加一个子规则
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(Rule rule)
        {
            targetRules.Add(rule);
            targetRules.Sort();
        }

        /// <summary>
        /// 返回内部子规则的数量
        /// </summary>
        /// <returns></returns>
        public int RuleCount()
        {
            return targetRules.Count();
        }

        //TODO: 删除某个现有规则的方法，在等处理完比较器后，再实现一个迭代器来实现？
        /// <summary>
        /// 清除全部子规则
        /// </summary>
        public void ClearAll()
        {
            targetRules.Clear();
        }

        //TODO: 添加优先级队列数据结构，避免每次修改都重新排序，提升性能
        private void SortRules()
        {
            targetRules.Sort();
        }

        public override int Priority
        {
            get
            {
                int pri = (int)Uitlity.RulePriority.LogicRulePriority;
                foreach(var i in targetRules)
                {
                    pri += i.Priority;
                }
                return pri;
            }
        }
    }
}
