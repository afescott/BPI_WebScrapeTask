using BPI_WebScrapeTask.ClassObjects;
using System.Collections.Generic;
using System.IO;

namespace BPI_WebScrapeTask.Classes
{
    /// <summary>
    /// This class Creates and populates a CSV file with the grid data
    /// </summary>
    public class Csv
    {
        public void SaveData(List<GoogleResult> results)
        {
            var file = @".\MusicianSearchData.csv";

            using (var stream = File.CreateText(file))
            {
                stream.WriteLine(string.Format("{0},{1},{2},{3}", "Rank", "Song title", "Link title", "URL"));
                foreach (var element in results)
                {
                    var csvRow = string.Format("{0},{1},{2},{3}", element.Rank, element.SearchPhrase, element.Title,
                        element.URL);

                    stream.WriteLine(csvRow);
                }
                stream.WriteLine();
                stream.Close();
            }
        }
    }
}
