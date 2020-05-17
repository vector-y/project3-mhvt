using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cecs475.BoardGames.WpfApp
{
   
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        String path = "../../../../src/Cecs475.BoardGames.WpfApp/bin/Debug/games/";

        public LoadingWindow()
        {
            InitializeComponent();
        }
        
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://cecs475-boardamges.herokuapp.com");
            var request = new RestRequest("/api/games", Method.GET);

            var response = client.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Failed to Connect");
            }
            else
            {
                dynamic b = JsonConvert.DeserializeObject(response.Content);
                await DownloadFileTaskAsync(b);
            }
            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
            this.Close();
            var gamewindow = new GameChoiceWindow();

        }
        private async Task DownloadFileTaskAsync(dynamic b)
        {
            foreach (var game in b)
            {
                using (WebClient wc = new WebClient())
                {
                    foreach (var file in game["Files"])
                    {
                        String url = file["Url"];
                        SetFolderPermission(path);
                        wc.DownloadFile(new System.Uri(url), path+file["FileName"]);
                    }
                }
            }   
        }
        public static void SetFolderPermission(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            var directorySecurity = directoryInfo.GetAccessControl();
            var currentUserIdentity = WindowsIdentity.GetCurrent();
            var fileSystemRule = new FileSystemAccessRule(currentUserIdentity.Name,
                                                          FileSystemRights.Read,
                                                          InheritanceFlags.ObjectInherit |
                                                          InheritanceFlags.ContainerInherit,
                                                          PropagationFlags.None,
                                                          AccessControlType.Allow);
            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }
    }

}
