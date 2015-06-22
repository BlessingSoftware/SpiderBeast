using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using HtmlAgilityPack;
using SpiderBeast.Uitlity;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 节点类型规则。节点Tag匹配指定的名称则满足。
    /// </summary>
    public class TypeRule : Rule
    {
        string name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeName">要匹配的Tag名称</param>
        public TypeRule(string typeName)
        {
            name = typeName;
        }

        public override int Priority
        {
            get
            {
                return (int)Uitlity.RulePriority.TypeRulePriority;
            }
        }

        public override bool Verify(HtmlNode targetNode)
        {
            //Console.WriteLine(targetNode.Name);
            if (targetNode.OriginalName == name) return true;
            return false;
        }
    }
}
