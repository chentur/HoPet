using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Tweetinvi;

namespace HoPet.Models
{
    public class WebServices
    {
        public const string CONSUMER_KEY = "mve2gLEBf1uXhMBRG5R7inWy0";
        public const string CONSUMER_SECRET = "Xjs4mgfEy9tbP6VuP7SUBgkxlSlSUjI1mtzKIepqNY6X0Qjg4Q";
        public const string ACCESS_TOKEN = "48067925-AGOwqbGLbZRzTqz0tKuJnyAYFWC2Q3Vy0Fe5XPnVe";
        public const string ACCESS_TOKEN_SECRET = "In8mlQWQGuapepxbpYZ1mTyVmWm9c4a8gLWCYzugLU8a9";

        private Tweetinvi.Models.IAuthenticatedUser user;
    
        public WebServices()
        {
            user = GetTwitterUser();
        }

        public String GetWikiValue(string title)
        {
            title = title.Replace(" ", "%20");
            string url = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext&exintro&titles=" + title + "&redirects=true";
            var client = new WebClient();
            return client.DownloadString(url);
        }

        public void PostTweet(string content)
        {
            Tweet.PublishTweet(content);
        }

        public Object GetTweets()
        {
            return Timeline.GetUserTimeline(user);
        }

        private Tweetinvi.Models.IAuthenticatedUser GetTwitterUser()
        {
            Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
            return Tweetinvi.User.GetAuthenticatedUser();
        }
    }
}