using BPI_WebScrapeTask.Classes;
using BPI_WebScrapeTask.ClassObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BPI_WebScrapeTask.Controller
{
    /// <summary>
    /// Class for routing between the views and models. Also performs exception handling.
    /// </summary>
    public class MainViewModel
    {
        private List<GoogleResult> MergedResults;

        public void ImportIntoCsv()
        {

            if (MergedResults != null)
            {
                var csv = new Csv();

                csv.SaveData(MergedResults);

                MessageBox.Show("Saved to CSV in Bin/Debug folder");


            }
            else
            {
                MessageBox.Show("Data is yet to be loaded");
            }
        }




        public List<GoogleResult> GatherSearchResults(bool googleApiChoice)
        {
            var api = new GoogleSearch();

            var eilishResults = new List<GoogleResult>();
            var grandeResults = new List<GoogleResult>();
            var mendesResults = new List<GoogleResult>();

            if (googleApiChoice)
            {
                eilishResults = api.GetSearchResults("Billie Eilish - Therefore I am");
                if (eilishResults.Count > 0) //Prevent multiple dialog boxes from appearing
                {
                    grandeResults = api.GetSearchResults("Ariana Grande - Positions");
                    mendesResults = api.GetSearchResults("Shawn Mendes - Monster");
                }
            }
            else
            {
                eilishResults = api.GetGoogleApiResults("Billie Eilish - Therefore I am");
                grandeResults = api.GetGoogleApiResults("Ariana Grande - Positions");
                mendesResults = api.GetGoogleApiResults("Shawn Mendes - Monster");

            }

            MergedResults = eilishResults.Union(grandeResults).Union(mendesResults).ToList();

            return MergedResults;
        }
    }
}
