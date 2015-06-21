﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpiderBeast.Base;
using System.IO;

namespace SpiderBeast.DataManagers
{
    /// <summary>
    /// 文本流数据处理器。它将收到的文本数据全部写入指定文件。未定义的数据全部忽略。
    /// </summary>
    public class TextStreamDataManager : DataManager
    {
        /// <summary>
        /// 写入文本时的编码
        /// </summary>
        Encoding m_encoding;

        /// <summary>
        /// 设置或获取文本的编码
        /// </summary>
        public Encoding TextEncoding
        {
            get { return m_encoding; }
            set { m_encoding = value; }
        }
        /// <summary>
        /// 指定文件的地址
        /// </summary>
        string filePath;

        /// <summary>
        /// 追加模式。True为追加。
        /// </summary>
        bool appendMode = false;

        StreamWriter w;

        /// <summary>
        /// 文本流数据处理器构造函数。默认覆盖原文件。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        public TextStreamDataManager(string path) : this(path, false)
        {
        }

        /// <summary>
        /// 文本流处理器构造函数，使用UTF-8编码。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        /// <param name="append">指定追加模式，True为追加。</param>
        public TextStreamDataManager(string path, bool append) : this(path, append, Encoding.UTF8)
        {
        }

        /// <summary>
        /// 文本流处理器构造函数,指定使用的字符编码。
        /// </summary>
        /// <param name="path">要指定写入的地址</param>
        /// <param name="append">指定追加模式，True为追加。</param>
        /// <param name="encoding">要使用的字符编码。</param>
        public TextStreamDataManager(string path, bool append, Encoding encoding)
        {
            filePath = path;
            appendMode = append;
            m_encoding = encoding;
        }

        /// <summary>
        /// 设定追加模式。
        /// </summary>
        /// <param name="append">新的追加模式。</param>
        public void SetAppendMode(bool append)
        {
            appendMode = append;
        }

        /// <summary>
        /// 数据处理函数。接受传递进来的数据。只接受文本类，忽略其他。
        /// </summary>
        /// <param name="data"></param>
        public override void DataHandler(FilterResult data)
        {
            string text = data.GetResult<string>();
            w.WriteLine(text);
        }

        /// <summary>
        /// 处理解析结束事件。
        /// </summary>
        public override void OnFetchEndHandler()
        {
            if (w != null) w.Close();
            w = null;
        }

        /// <summary>
        /// 处理解析开始事件。
        /// </summary>
        public override void OnFetchStartHandler()
        {
            w = new StreamWriter(filePath, appendMode, m_encoding);
        }
    }
}
