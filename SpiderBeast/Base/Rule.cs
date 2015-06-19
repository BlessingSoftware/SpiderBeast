using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 规则抽象类，代表一种单一的节点模式，提供验证规则的方法
    /// </summary>
    abstract class Rule
    {
        /// <summary>
        /// 规则类的检验方法，验证参数节点是否满足设定的规则
        /// </summary>
        /// <param name="targetNode">待验证的的节点</param>
        /// <returns>返回是否满足规则的布尔值</returns>
        public abstract bool Verify(HtmlAgilityPack.HtmlNodeNavigator targetNode);
    }
}
