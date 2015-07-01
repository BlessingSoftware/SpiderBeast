using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderBeast.Base
{
    public class EmbedResoureFilter : Filter
    {
        private FilterType starttype;

        public FilterType StartFilterType
        {
            get { return starttype; }
            set { starttype = value; }
        }
        private FilterType targettype;

        public FilterType TargetFilterType
        {
            get { return targettype; }
            set { targettype = value; }
        }

        private EmbedDeep targetdeep;

        public EmbedDeep TargetDeep
        {
            get { return targetdeep; }
            set { targetdeep = value; }
        }

        private string start;
        public string Start { get { return start; } set { start = value; } }
        private string target;
        public string Target { get { return target; } set { target = value; } }

        public EmbedResoureFilter(FilterType starttype = FilterType.Root, FilterType targettype = FilterType.Tag, EmbedDeep targetdeep = EmbedDeep.Child)
        {
            this.starttype = starttype;
            this.targettype = targettype;
            this.targetdeep = targetdeep;
        }

        public IEnumerable<IHtmlBaseNode> Filt(HtmlDocument doc)
        {
            switch (starttype)
            {
                case FilterType.NodeID:
                    var root = doc.GetElementbyId(start);
                    if (root != null)
                        return getChildren(root);
                    break;
                case FilterType.Tag:

                    break;
                default:
                    return null;
            }
            return null;
        }
        static readonly List<IHtmlBaseNode> s_emptyNodeList = new List<IHtmlBaseNode>();
        private List<IHtmlBaseNode> getChildren(HtmlNode parent)
        {
            string preXPath = string.Empty;
            switch (targetdeep)
            {
                case EmbedDeep.Child:
                    preXPath = "./" + target;
                    break;
                case EmbedDeep.Script:
                    preXPath = "./script";
                    break;
                case EmbedDeep.DeepChild:
                    preXPath = ".//" + target; ;
                    break;
                case EmbedDeep.DeepScript:
                    preXPath = ".//script";
                    break;
                default:
                    break;
            }
            if (preXPath != string.Empty)
            {
                return parent.SelectNodes(preXPath);
            }
            return s_emptyNodeList;
        }
    }
}
