using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Base
{
    /// <summary>
    /// 数据管理类型，用于整合Filter收集到的数据，做出不同的处理。
    /// </summary>
    public abstract class DataManager
    {
        /// <summary>
        /// 处理开始分析事件。进行写入准备初始化，打开文件或者流等等。
        /// </summary>
        abstract public void OnFetchStartHandler();


        /// <summary>
        /// 处理结束分析事件。进行写入结束的清理事件，关闭文件或者流等等。
        /// </summary>
        abstract public void OnFetchEndHandler();


        /// <summary>
        /// 处理收到的数据。
        /// </summary>
        /// <param name="data">处理收到的Object类型数据。应该根据子类的实现转型成相应的需要的类型。
        /// 原则上转型失败则记录Log，不产生任何效果，忽略错误。</param>
        abstract public void DataHandler(FilterResult data);
    }
}
