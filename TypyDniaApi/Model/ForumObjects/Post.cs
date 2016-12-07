using System;
using System.Linq;
using HtmlAgilityPack;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.ForumObjects
{
    public class Post
    {
        public User Author { get; private set; }

        public DateTime CreatedPostDate { get; private set; }

        public Prediction Prediction { get; private set; }

        public string Content { get; set; }

        public Post(HtmlNode postNode, DateTime date)
        {
            CreatedPostDate = Utils.GetPostDate(postNode);
            Prediction = Utils.GetPredictionFromPost(postNode, date);
            Author = new User(postNode);
            Content = Utils.GetPostContent(postNode);
        }
    }
}