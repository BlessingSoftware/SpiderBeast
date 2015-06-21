using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 参数规则，如果具有指定名称的参数则不满足。
    /// </summary>
    public class ExceptAttributeRule : AttributeRule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">要排除的参数名称</param>
        public ExceptAttributeRule(string name) : base(name)
        {

        }

        public override bool Verify(HtmlNode targetNode)
        {
            var attr = targetNode.Attributes;
            foreach (var i in attr)
            {
                if (i.Name == attributeName) return false;
            }
            return true;
        }
    }
}
