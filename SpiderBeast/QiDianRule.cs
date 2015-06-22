using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using SpiderBeast.Base;
using SpiderBeast.Rules;

namespace SpiderBeast
{
    public class QiDianRule : Rule
    {
        Rule r;

        public override int Priority
        {
            get
            {
                return 10;
            }
        }

        public QiDianRule()
        {
            TypeRule typep = new TypeRule("p");
            TypeRule typediv = new TypeRule("div");
            AttributeEqualToRule ar = new AttributeEqualToRule("id", "chaptercontent");
            LogicAndRule logicand = new LogicAndRule(typediv, ar);
            RelativeFatherRule f = new RelativeFatherRule(logicand);
            r = (Rule)(new LogicAndRule(typep, f));
        }

        public override bool Verify(HtmlNode targetNode)
        {
            return r.Verify(targetNode);
        }
    }
}
