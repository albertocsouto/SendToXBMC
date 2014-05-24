using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using SendToXBMC.Client.Requests;
using SendToXBMC.View.Settings;
using SendToXBMC.Util;


namespace SendToXBMC
{
    public sealed partial class MainPage : Page
    {
        ShareOperation shareOperation;

        private Uri sharedWebLink;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.InfoTextBox.Text = ResourcesManager.LocalizedString("SettingsMessage");

            // It is recommended to only retrieve the ShareOperation object in the activation handler, return as
            // quickly as possible, and retrieve all data from the share target asynchronously.
            if (e.Parameter.GetType().Equals(typeof(ShareOperation)))
            {
                this.shareOperation = (ShareOperation)e.Parameter;
                if (this.shareOperation.Data.Contains(StandardDataFormats.WebLink))
                {
                    this.sharedWebLink = await this.shareOperation.Data.GetWebLinkAsync();
                    this.ProgressBar.Visibility = Visibility.Visible;
                    this.InfoTextBox.Text = String.Format(ResourcesManager.LocalizedString("TryingToSend"), this.sharedWebLink.AbsoluteUri);
                    Boolean playResult = await PlaylistRequests.playVideo(this.sharedWebLink.AbsoluteUri);
                    if (playResult)
                    {
                        this.InfoTextBox.Text = ResourcesManager.LocalizedString("VideoSentOk");
                    }
                    else
                    {
                        this.InfoTextBox.Text = ResourcesManager.LocalizedString("VideoSentKo");
                    }
                    this.ProgressBar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            XBMCSettingsFlyout settings = new XBMCSettingsFlyout();
            settings.ShowIndependent();
        }
    }
}
