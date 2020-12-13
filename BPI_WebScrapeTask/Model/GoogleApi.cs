using BPI_WebScrapeTask.ClassObjects;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;

namespace BPI_WebScrapeTask
{
    /// <summary>
    /// This class communicates with the google search engine using HtmlAgilityPack to retrieve the top 20 search results or 10 search results
    /// with the google API method
    /// </summary>
    public class GoogleSearch
    {
        public List<GoogleResult> GetGoogleApiResults(string searchPhrase)
        {
            var cx = "003444117244519549193:t6c4vtktcj3";
            var apiKey = "AIzaSyCbxU8sqWKCN3A3L0oFyHgF0dNDYyr0Z7g";

            var rankCounter = 0;

            var results = new List<GoogleResult>();

            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" +
                                            cx + "&q=" + searchPhrase);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseString = reader.ReadToEnd();

            dynamic jsonData = JsonConvert.DeserializeObject(responseString);

            foreach (var item in jsonData.items)
            {
                rankCounter += 1;

                var resultGoogle = new GoogleResult();

                results.Add(new GoogleResult
                {
                    Title = item.title,
                    URL = item.link,
                    SearchPhrase = searchPhrase,
                    Rank = rankCounter
                });

                if (rankCounter == 20)
                    return results;
            }
            return results;
        }

        public List<GoogleResult> GetSearchResults(string searchPhrase)
        {
            var googleResults = new List<GoogleResult>();

            var rankCounter = 0;

            var stringSearch = "http://www.google.com/search?num=100&q=" + searchPhrase;

            var docResult = new HtmlWeb().Load(stringSearch);

            var googleNodes = docResult.DocumentNode.SelectNodes("//html//body//div[@class='g']");
            if (googleNodes == null)
            {
                MessageBox.Show(
                    "There seems to be an issue in retrieving the google results. This started happening after " +
                    "prolonged periods of testing. Please try again later");
                return new List<GoogleResult>();
            }
            foreach (var node in googleNodes)
            {
                rankCounter += 1;

                var hrefValue = node
                    .Descendants("div")
                    .Where(x => x.Attributes["class"].Value.Contains("yuRUbf"))
                    .Select(x => x.Element("a").Attributes["href"].Value)
                    .FirstOrDefault();

                googleResults.Add(new GoogleResult
                {
                    Title = node.InnerText,
                    URL = hrefValue,
                    SearchPhrase = searchPhrase,
                    Rank = rankCounter
                });

                if (rankCounter == 20)
                    return googleResults;
            }
            return googleResults;
        }
    }
}
