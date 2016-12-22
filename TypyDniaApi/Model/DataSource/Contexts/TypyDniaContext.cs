using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using TypyDniaApi.Model.ForumObjects;

namespace TypyDniaApi.Model.DataSource.Contexts
{
    public class TypyDniaContext : IContext
    {
        private const string Url = "https://www.forum.bukmacherskie.com/f43/i1.html";

        private int _pageCount;

        private HtmlWeb _webget;

        public TypyDniaContext()
        {
            Refresh();
        }

        public Day GetDay(DateTime date)
        {
            return new Day(GetThreadPagesNodes(date), date);
        }

        private Dictionary<int, HtmlNode> GetThreadPagesNodes(DateTime date)
        {
            string requestedDate = date.ToString("dd.MM.yyyy").First() == '0' ? date.ToString("dd.MM.yyyy").Substring(1) : date.ToString("dd.MM.yyyy");
            for (int i = 0; i < _pageCount; i++)
            {
                HtmlNode node = GetTypyDniaPageNode(i).ChildNodes.FirstOrDefault(n => n.InnerText.Contains(" " + requestedDate));

                if (node != null)
                {
                    HtmlNode hrefContainer = node.Descendants().First(n => n.Name.Equals("a"));
                    string threadUrl = hrefContainer.Attributes.First(a => a.Name.Equals("href")).Value;

                    Dictionary<int, HtmlNode> threadNodes = GetSubpagesNodes(threadUrl);

                    return threadNodes;
                }
            }
            return null;
        }

        private HtmlNode GetTypyDniaPageNode(int num)
        {
            HtmlDocument document = _webget.Load(Url.Replace("i1", string.Format("i{0}", num)));
            return document.GetElementbyId("threadbits_forum_43");
        }

        private Dictionary<int, HtmlNode> GetSubpagesNodes(string threadUrl)
        {
            var dict = new Dictionary<int, HtmlNode>();
            HtmlDocument document = _webget.Load(threadUrl);
            HtmlNode mainNode = document.DocumentNode;

            dict.Add(1, mainNode);

            int pageCount = GetSubpagesCount(mainNode);

            for (int i = 2; i <= pageCount; i++)
            {
                HtmlDocument doc = _webget.Load(threadUrl.Replace(".html", string.Format("-{0}.html", i)));
                dict.Add(i, doc.DocumentNode);
            }

            return dict;
        }

        private int GetSubpagesCount(HtmlNode pageNode)
        {
            var cnt = 1;
            HtmlNode pageNav = pageNode.SelectSingleNode("//div[@class='pagenav']");
            if (pageNav != null)
            {
                HtmlNode countNode = pageNav.Descendants().First(n => n.Name.Equals("td"));
                string count = countNode.InnerText.Replace("Strona 1 z ", "");
                Int32.TryParse(count, out cnt);
            }

            return cnt;
        }

        private void Refresh()
        {
            _webget = new HtmlWeb();
            _webget.OverrideEncoding = Encoding.GetEncoding("ISO-8859-2");
            HtmlDocument document = _webget.Load(Url);

            HtmlNode mainNode = document.DocumentNode;

            _pageCount = GetSubpagesCount(mainNode);
        }
    }
}