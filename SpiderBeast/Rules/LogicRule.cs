using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 逻辑规则抽象类，用于处理规则之间的逻辑关系。内部含有子规则。
    /// </summary>
    abstract class LogicRule : Base.Rule
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

        public void AddRule(Rule rule)
        {
            targetRules.Add(rule);
            targetRules.Sort();
        }

        public int RuleCount()
        {
            return targetRules.Count();
        }

        //TODO: 删除某个现有规则的方法，在等处理完比较器后，再实现一个迭代器来实现？
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
                //设定逻辑规则的优先级为5
                return 5;
            }
        }
    }
}
