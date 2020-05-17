<<<<<<< HEAD
﻿using Newtonsoft.Json;
=======
using Newtonsoft.Json;
>>>>>>> ca6cba22fccc29600cc9327471b0fc40b4768d58
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
<<<<<<< HEAD
            this.Close();
=======
            int milliseconds = 2000;
            
>>>>>>> ca6cba22fccc29600cc9327471b0fc40b4768d58
            var gamewindow = new GameChoiceWindow();
            gamewindow.Show();

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
