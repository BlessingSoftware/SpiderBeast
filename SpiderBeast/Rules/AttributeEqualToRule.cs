using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace SpiderBeast.Rules
{
    /// <summary>
    /// 参数等于，如果具有指定属性，并且等于指定的值，则成立。
    /// </summary>
    public class AttributeEqualToRule : AttributeRule
    {
        public AttributeEqualToRule(string name, string value) : base(name, value) { }

        public string AttributeName
        {
            get { return attributeName; }
            set { attributeName = value; }
        }

        public string AttributeValue
        {
            get { return attributeValue;}
            set { attributeValue = value; }
        }

        public override bool Verify(HtmlNode targetNode)
        {
            var attr = targetNode.Attributes;
            foreach (var i in attr)
            {
                if (i.Name == attributeName)
                {
                    if (i.Value == AttributeValue)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            return false;
        }
    }
}
