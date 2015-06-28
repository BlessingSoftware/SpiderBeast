using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Base
{
    public enum FilterType : int
    {
        Root = 0,
        NodeID = 1,
        Tag = 2
    }
    /// <summary>
    /// 资源的嵌套深度
    /// </summary>
    public enum EmbedDeep :int
    {
        /// <summary>
        /// 父节点下
        /// </summary>
        Child,
        /// <summary>
        /// 在父节点脚本中
        /// </summary>
        Script,
        DeepChild,
        DeepScript
    }
}
