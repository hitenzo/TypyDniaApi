using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace TypyDniaApi.Model.ForumObjects
{
    public class Day
    {
        public DateTime DayDate { get; private set; }

        public List<Post> Posts { get; private set; }

        private readonly Dictionary<int, HtmlNode> _pageNodes;

        public Day(Dictionary<int, HtmlNode> pageNodes, DateTime date)
        {
            _pageNodes = pageNodes;
            DayDate = date;
            Posts = GetPost();
        }

        private List<Post> GetPost()
        {
            var posts = new List<Post>();

            foreach (KeyValuePair<int, HtmlNode> pair in _pageNodes)
            {
                HtmlNode node = pair.Value;

                HtmlNode postContainer = node.Descendants().First(n => n.Id.Equals("posts"));

                List<HtmlNode> postsNodes = GetPostsNodes(postContainer);

                foreach (HtmlNode postNode in postsNodes)
                {
                    var post = new Post(postNode, DayDate);

                    posts.Add(post);
                }
            }

            return posts;
        }

        private List<HtmlNode> GetPostsNodes(HtmlNode boardNode)
        {
            List<HtmlNode> nodes =
                boardNode.Descendants()
                    .Where(el => el.Name.Equals("table") && el.Id.Contains("post"))
                    .ToList();

            return nodes;
        }
    }
}