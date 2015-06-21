using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 检查参数规则的抽象类
    /// </summary>
    public abstract class AttributeRule : Rule
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        protected string attributeName;

        /// <summary>
        /// 参数值
        /// </summary>
        protected string attributeValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数的名称</param>
        public AttributeRule(string name)
        {
            attributeName = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="value">参数的值</param>
        public AttributeRule(string name, string value)
        {
            attributeName = name;
            attributeValue = value;
        }

        public override RulePriority Priority
        {
            get
            {
                return RulePriority.AttributeRulePriority;
            }
        }
    }
}
