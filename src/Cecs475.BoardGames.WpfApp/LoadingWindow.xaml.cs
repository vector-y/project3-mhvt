using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Cecs475.BoardGames.WpfApp
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public event EventHandler Load;
        public LoadingWindow()
        {
            InitializeComponent();
        }
        private void loadEvent(Object sender, RoutedEventArgs e)
        {
            var cilent = new RestClient("https://cecs475-boardamges.herokuapp.com");
            var request = new RestRequest("/api/games", Method.GET);
            var response = cilent.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Nothing to download");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Connected");
                JObject obj = JObject.Parse(response.Content);
                string result = JsonConvert.DeserializeObject<string>(response.Content);
                //webClient.DownloadString
                MessageBox.Show(result);
            }
        }
    }
}
