using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace SpiderBeast.Fetchs
{
    /// <summary>
    /// 用特定脚本，生成的自定义目录解析类。
    /// </summary>
    public class ScriptIndexFetch : IndexFetchsBase
    {
        public ScriptIndexFetch(string url, string script) : base(url)
        {
            ParseScript(XmlReader.Create(new StringReader(script)));
        }

        public ScriptIndexFetch(string url, Stream stream) : base(url)
        {
            ParseScript(XmlReader.Create(stream));
        }

        public void ParseScript(XmlReader reader)
        {
            reader.Read();
            reader.Read();           
            if (reader.HasAttributes)
            {
                reader.MoveToFirstAttribute();
                NodeFilterType nft = (NodeFilterType)Enum.Parse(typeof(NodeFilterType), reader.Value);                
                reader.MoveToNextAttribute();
                this.ParentRule = new NodeRule(nft, reader.Value);
                reader.Read();
                reader.Read();
                reader.MoveToFirstAttribute();
                nft = (NodeFilterType)Enum.Parse(typeof(NodeFilterType), reader.Value);
                reader.MoveToNextAttribute();
                this.ChildrenRule = new NodeRule(nft, reader.Value);
            }



            //(NodeFilterType)reader.ReadElementContentAs(typeof(NodeFilterType),)
        }
    }
}
