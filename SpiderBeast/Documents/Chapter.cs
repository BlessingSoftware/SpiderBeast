using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpiderBeast.Documents
{
    [DebuggerDisplay("{Name},{Href}")]
    public class Chapter
    {
        public int VolumeIndex { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 章节地址
        /// </summary>
        public string Href { get; set; }

        public Chapter()
        {

        }

        public Chapter(string name, string href, int vol)
        {
            this.Name = name;
            this.Href = href;
            this.VolumeIndex = vol;
        }
    }
    [DebuggerDisplay("Title = {ComicName},{ChapterName},Count = {Count}")]
    public class CmoicChapter
    {
        public List<string> UrlList { get; private set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }
        /// <summary>
        /// 漫画名称
        /// </summary>
        public string ComicName { get; set; }

        public int Index { get; set; }

        public int Count { get { return UrlList.Count; } }

        public string this[int index] { get { return UrlList[index]; } }

        public CmoicChapter()
        {
            UrlList = new List<string>();
        }
    }
}
