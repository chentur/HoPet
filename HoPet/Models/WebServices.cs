using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HoPet.Models
{
    public class WebServices
    {
        public String GetWikiValue(string title)
        {
            title = title.Replace(" ", "%20");
            string url = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext&exintro&titles=" + title + "&redirects=true";
            var client = new WebClient();
            return client.DownloadString(url);
        }
    }
}