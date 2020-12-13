using BPI_WebScrapeTask.Controller;
using System.Windows;
using System.Windows.Controls;

namespace BPI_WebScrapeTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
        }

        private void ButtonSaveToCsvGrid(object sender, RoutedEventArgs e)
        {
            ViewModel.ImportIntoCsv();
        }

        private void LoadSearchGrid(object sender, RoutedEventArgs e)
        {

            bool googleApiChoice = MessageBox.Show("Would you like to use the google API method? Otherwise HtmlAgilityPack is used? This method only returns 10 results",
                                       "Method choice?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No;

                var results = ViewModel.GatherSearchResults(googleApiChoice);

                foreach (var item in results)
                {
                    ScrapeResultsGrid.Items.Add(item);
                }
            
        

        }

        private void ScrapeResultsGrid_OnBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //Exception handling incase the user interacts with grid
            e.Cancel = true;
        }
    }
}
