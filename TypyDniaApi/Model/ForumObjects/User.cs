using System;
using System.Linq;
using HtmlAgilityPack;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.ForumObjects
{
    public class User
    {
        public string Login { get; set; }

        public DateTime JoinedDate { get; set; }

        public int PostsCount { get; set; }

        public int Reputation { get; set; }

        private readonly HtmlNode _postNode;

        public User(HtmlNode postNode)
        {
            _postNode = postNode;
            Reputation = GetReputation();
            Login = Utils.GetLogin(postNode);
            PostsCount = Utils.GetPostCount(postNode);
            JoinedDate = Utils.GetJoinedDate(postNode); //todo wojtek
        }

        private void ParseNode()
        {
            string login = _postNode.ChildNodes[1].InnerText;


        }

        private int GetReputation()
        {
            int rep = -1;

            HtmlNode repNode =
                _postNode.Descendants()
                    .FirstOrDefault(
                        child =>
                            child.Name.Equals("span") && 
                            child.Attributes["id"] != null &&
                            child.Attributes["id"].Value.Contains("rep"));

            if (repNode != null && repNode.InnerText.Contains("Reputacja"))
            {
                string repString = repNode.InnerText.Replace("Reputacja: ", string.Empty);
                rep = Int32.Parse(repString);
            }


            return rep;
        }

    }
}